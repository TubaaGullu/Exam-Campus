using AutoMapper;
using BACampusApp.Business.Abstracts;
using BACampusApp.Core.Enums;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Dtos.Exam;
using BACampusApp.Dtos.Question;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class ExamManager : IExamService
    {
        private readonly IExamRepository _examRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IStudentQuestionRepository _studentQuestionRepository;
        private IMapper _mapper;
        public ExamManager(IExamRepository examRepository, IMapper mapper, ITrainerRepository trainerRepository, IStringLocalizer<Resource> stringLocalizer, IStudentExamRepository studentExamRepository, IStudentQuestionRepository studentQuestionRepository)
        {
            _examRepository = examRepository;

            _mapper = mapper;
            _trainerRepository = trainerRepository;
            _stringLocalizer = stringLocalizer;
            _studentExamRepository = studentExamRepository;
            _studentQuestionRepository = studentQuestionRepository;
        }


        /// <summary>
        /// sınav, sınavın öğrencilerini ve sorularını ekler.
        /// </summary>
        /// <param name="examCreateDto">ExamCreateDto</param>
        /// <returns>DataResult<ExamDto></returns>
        public async Task<IDataResult<ExamDto>> AddAsync(ExamCreateDto examCreateDto)
        {

            var exam = _mapper.Map<Exam>(examCreateDto);
            await _examRepository.AddAsync(exam);
            await _examRepository.SaveChangesAsync();

            return new SuccessDataResult<ExamDto>(_mapper.Map<ExamDto>(exam), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        /// sınavı, sınavın öğrencilerini ve sorularını siler.
        /// </summary>
        /// <returns>IResult döndürür</returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);

            if (exam is null)
            {
                return new ErrorResult(_stringLocalizer[Messages.ExamNotFound]);
            }

            var studentExams = await _studentExamRepository.GetAllAsync(x => x.ExamId == exam.Id, false);
            if (studentExams.Count()<=0)
            {
                return new ErrorResult(_stringLocalizer[Messages.StudentExamNotFound]);
            }
            exam.StudentExams = studentExams.ToList();

            foreach (var studentExam in exam.StudentExams)
            {
                var studentQuestions = await _studentQuestionRepository.GetAllAsync(x => x.StudentExamId == studentExam.Id,false);
                if (studentQuestions.Count() <= 0)
                {
                    return new ErrorResult(_stringLocalizer[Messages.StudentQuestionNotFound]);
                }
                studentExam.StudentQuestions = studentQuestions.ToList();
            }

            await _examRepository.DeleteAsync(exam);
            await _examRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }


        /// <summary>
        /// Mevcut bütün sınavları liste olarak döner.
        /// <returns>ExamListDto</returns>
        public async Task<IDataResult<List<ExamListDto>>> GetAllAsync()
        {
            var exams = await _examRepository.GetAllAsync();
            if (exams.Count() <= 0)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);

            }
            return new SuccessDataResult<List<ExamListDto>>(_mapper.Map<List<ExamListDto>>(exams), _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// examınIdsine göre examı ve içindeki seçili studentları ve seçili soruları döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<SelectedExamDto>> GetByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);

            if (exam != null)
            {
                var activeStudentExams = exam.StudentExams.Where(se => se.Status!=Status.Deleted).ToList();
                var activeStudentQuestions = exam.StudentExams.ToList()[0].StudentQuestions.Where(se => se.Status != Status.Deleted).ToList();
                var activeExam = _mapper.Map<SelectedExamDto>(exam);
                activeExam.StudentExams = _mapper.Map<List<SelectedStudentExamDto>>(activeStudentExams);
                activeExam.StudentQuestions = _mapper.Map<List<SelectedStudentQuestionDto>>(activeStudentQuestions);
                return new SuccessDataResult<SelectedExamDto>(activeExam, _stringLocalizer[Messages.ExamFoundSuccess]);
            }
            return new ErrorDataResult<SelectedExamDto>(_stringLocalizer[Messages.ExamNotFound]);
        }


        /// <summary>
        /// sınavın, sınavın öğrencilerinin, sınavın öğrencilerinin sorularının güncellenmesi.
        /// </summary>
        /// <param name="examUpdateDto"></param>
        /// <returns></returns>
        public async Task<IDataResult<ExamDto>> UpdateAsync(ExamUpdateDto examUpdateDto)
        {
            var exam = await _examRepository.GetByIdAsync(examUpdateDto.Id);
            if (exam is null)
                return new ErrorDataResult<ExamDto>(_stringLocalizer[Messages.ExamNotFound]);

            var existingStudentExams = exam.StudentExams;
            var updatedStudentExams = examUpdateDto.StudentExams;
            var allExistingStudentQuestions = existingStudentExams
                            .SelectMany(exam => exam.StudentQuestions)
                            .ToList();
            var updatedStudentQuestions = examUpdateDto.StudentQuestions;
            var StudentExams = MergeStudentCombine(existingStudentExams.ToList(), updatedStudentExams, allExistingStudentQuestions, updatedStudentQuestions.ToList());
            var updatedExam = _mapper.Map(examUpdateDto, exam);
            updatedExam.StudentExams = StudentExams;
         
            await _examRepository.UpdateAsync(updatedExam);
            await _examRepository.SaveChangesAsync();

            return new SuccessDataResult<ExamDto>(_mapper.Map<ExamDto>(updatedExam), _stringLocalizer[Messages.UpdateSuccess]);
        }



        /// <summary>
        /// studentexam ve studentquestionların eklenme silinme ve güncellenmeleri durumlarını dikkate alarak Güncellenecek studentexam onun içinde koleksiyon olan studentquestionları döndürür. 
        /// </summary>
        /// <param name="existingStudentExamList">DBdeki studentexams</param>
        /// <param name="updatedStudentExamList">Guncellenecek studentexams</param>
        /// <param name="ExamId"></param>
        /// <param name="existingStudentQuestionList">DBdeki studentquestions</param>
        /// <param name="updatedStudentQuestionList">Guncellenecek studentQuestions</param>
        /// <returns></returns>
        private List<StudentExam> MergeStudentCombine(List<StudentExam> existingStudentExamList, List<StudentExamUpdateDto> updatedStudentExamList,List<StudentQuestion> existingStudentQuestionList,List<StudentQuestionUpdateDto> updatedStudentQuestionList)
        {
            var mergedList = new List<StudentExam>();
            mergedList.AddRange(existingStudentExamList);

            foreach (var updatedItem in updatedStudentExamList)
            {
                var existingItem = existingStudentExamList.FirstOrDefault(e => e.StudentId == updatedItem.StudentId);

                if (existingItem != null)
                {
                    // existingItem üzerinde güncelleme yapılabilir
                    _mapper.Map(updatedItem, existingItem);

                    foreach (var updatedquestItem in updatedStudentQuestionList)
                    {
                        var existingQuestItem = existingStudentQuestionList.FirstOrDefault(e => e.QuestionId == updatedquestItem.QuestionId);

                        if (existingQuestItem != null)
                        {
                            // existingItem.StudentQuestions üzerinde güncelleme yapılabilir
                            _mapper.Map(updatedquestItem, existingQuestItem);
                        }
                        else
                        {
                            var studentQuestion = _mapper.Map<StudentQuestion>(updatedquestItem);
                            existingItem.StudentQuestions.Add(studentQuestion);
                        }
                    }

                    // Güncellenmiş existingItem'ı mergedList'e ekleyin
                    mergedList.Add(existingItem);

                    var questionsToRemove = existingItem.StudentQuestions
                        .Where(e => !updatedStudentQuestionList.Any(u => u.QuestionId == e.QuestionId))
                        .ToList();

                    foreach (var studentToRemove in questionsToRemove)
                    {
                        existingItem.StudentQuestions.Remove(studentToRemove);
                    }
                }
                else
                {
                    var studentExam = _mapper.Map<StudentExam>(updatedItem);
                    var studentQuestions = _mapper.Map<List<StudentQuestion>>(updatedStudentQuestionList);
                    studentExam.StudentQuestions = studentQuestions;
                    mergedList.Add(studentExam);
                }
            }

            var studentsToRemove = existingStudentExamList
                .Where(e => !updatedStudentExamList.Any(u => u.StudentId == e.StudentId))
                .ToList();

            foreach (var studentToRemove in studentsToRemove)
            {
                mergedList.Remove(studentToRemove);
            }

            return mergedList;
        }


        /// <summary>
        /// TrainerIdentityId ye göre eğitmenin eklediği tüm gelecek sınavları döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<ExamListDto>>> GetAllFutureExamByTrainerIdentityIdAsync(string trainerId)
        {
            var trainer = _trainerRepository.GetAsync(x => x.IdentityId == trainerId).Result;
            if (trainer == null)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.TrainerNotFound]);
            }
            var Now = DateTime.Now;
            var trainerExams = await _examRepository.GetAllAsync(s => s.CreatedBy == trainer.IdentityId);
            if (trainerExams.Count() <= 0)
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);
            var trainerExamFutures = trainerExams.Where(s => s.ExamDateTime > Now);
            if (trainerExamFutures.Count() <= 0)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);
            }

            var trainerExamsDto = _mapper.Map<List<ExamListDto>>(trainerExamFutures);

            return new SuccessDataResult<List<ExamListDto>>(trainerExamsDto, _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// TrainerIdentityId ye göre eğitmenin eklediği anlık sınavları döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<ExamListDto>>> GetAllCurrentExamByTrainerIdentityIdAsync(string trainerId)
        {
            var trainer = _trainerRepository.GetAsync(x => x.IdentityId == trainerId).Result;
            if (trainer == null)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.TrainerNotFound]);
            }

            var Now = DateTime.Now;
            var trainerExams = await _examRepository.GetAllAsync(s => s.CreatedBy == trainer.IdentityId);
            if (trainerExams.Count() <= 0)
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);
            var trainerExamCurrents = trainerExams.Where(s => s.ExamDateTime <= Now && s.ExamDateTime + s.ExamDuration > Now);
            if (trainerExamCurrents.Count() <= 0)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);
            }

            var trainerExamsDto = _mapper.Map<List<ExamListDto>>(trainerExamCurrents);

            return new SuccessDataResult<List<ExamListDto>>(trainerExamsDto, _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// TrainerIdentityId ye göre eğitmenin eklediği tüm eski sınavları döner.
        /// </summary>
        /// <returns></returns>
        public async Task<IDataResult<List<ExamListDto>>> GetAllOldExamByTrainerIdentityIdAsync(string trainerId)
        {
            var trainer = _trainerRepository.GetAsync(x => x.IdentityId == trainerId).Result;
            if (trainer == null)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.TrainerNotFound]);
            }
            var Now = DateTime.Now;
            var trainerExams = await _examRepository.GetAllAsync(s => s.CreatedBy == trainer.IdentityId);
            if(trainerExams.Count()<=0)
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);
            var trainerExamOlds = trainerExams.Where(s => (s.ExamDateTime + s.ExamDuration) < Now);
            if (trainerExamOlds.Count() <= 0)
            {
                return new ErrorDataResult<List<ExamListDto>>(_stringLocalizer[Messages.ExamNotFound]);
            }

            var trainerExamsDto = _mapper.Map<List<ExamListDto>>(trainerExamOlds);

            return new SuccessDataResult<List<ExamListDto>>(trainerExamsDto, _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// examınIdsine göre examı detaylı olarak döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<ExamDetailDto>> GetExamDetailByIdAsync(Guid id)
        {
            var exam = await _examRepository.GetByIdAsync(id);

            if (exam != null)
            {
                var activeStudentExams = exam.StudentExams.Where(se => se.Status != Status.Deleted).ToList();
                var activeStudentQuestions = exam.StudentExams.ToList()[0].StudentQuestions.Where(se => se.Status != Status.Deleted).ToList();
                var questions = activeStudentQuestions.Select(x=>x.Question).ToList();
                var activeExam = _mapper.Map<ExamDetailDto>(exam);
                activeExam.StudentExams = _mapper.Map<List<StudentExamsDetailsDto>>(activeStudentExams);
                activeExam.StudentQuestions = _mapper.Map<List<QuestionAndAnswerListDto>>(questions);
                return new SuccessDataResult<ExamDetailDto>(activeExam, _stringLocalizer[Messages.ExamFoundSuccess]);
            }
            return new ErrorDataResult<ExamDetailDto>(_stringLocalizer[Messages.ExamNotFound]);
        }


      

      
    }
    
}