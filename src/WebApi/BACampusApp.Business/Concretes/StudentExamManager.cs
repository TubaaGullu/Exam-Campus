using BACampusApp.Dtos.StudentAnswer;
using BACampusApp.Dtos.StudentExam;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class StudentExamManager :IStudentExamService
    {
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly IStudentRepository _studentRepository;
        private IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IStudentQuestionRepository _studentQuestionRepository;
        private readonly IStudentAnswerService _studentAnswerService;

        public StudentExamManager(IStudentExamRepository studentExamRepository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer, IStudentRepository studentRepository, IStudentQuestionRepository studentQuestionRepository, IStudentAnswerService studentAnswerService)
        {
            _studentExamRepository = studentExamRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _studentRepository = studentRepository;
            _studentQuestionRepository = studentQuestionRepository;
            _studentAnswerService = studentAnswerService;
        }
        /// <summary>
        /// Idsine göre StudentExami döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<StudentExamDto>> GetByIdAsync(Guid id)
        {
            var studentExam = await _studentExamRepository.GetByIdAsync(id);
            if (studentExam == null)
            {
                return new ErrorDataResult<StudentExamDto>(_stringLocalizer[Messages.StudentExamNotFound]);
            }

            return new SuccessDataResult<StudentExamDto>(_mapper.Map<StudentExamDto>(studentExam), _stringLocalizer[Messages.StudentExamFoundSuccess]);
        }
        /// <summary>
        /// Öğrenciye ait gelecek sınavların listesini döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<StudentExamListDto>>> GetAllFutureExamByStudentIdentityIdAsync(string id)
        {
            var student = _studentRepository.GetAsync(x => x.IdentityId == id).Result;
            if (student == null)
            {
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentNotFound]);
            }
            var studentExams = await _studentExamRepository.GetAllAsync(x => x.Student.IdentityId == id);
            if (studentExams.Count()<=0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            var futureStudentExams = studentExams.Where(x => x.Exam.ExamDateTime > DateTime.Now);
            if(futureStudentExams.Count()<=0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            return new SuccessDataResult<List<StudentExamListDto>>(_mapper.Map<List<StudentExamListDto>>(futureStudentExams), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Öğrenciye ait geçmiş sınavların listesini döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<StudentExamListDto>>> GetAllOldExamByStudentIdentityIdAsync(string id)
        {
            var student = _studentRepository.GetAsync(x => x.IdentityId == id).Result;
            if (student == null)
            {
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentNotFound]);
            }
            var studentExams = await _studentExamRepository.GetAllAsync(x => x.Student.IdentityId == id);
            if (studentExams.Count() <= 0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            var oldStudentExams = studentExams.Where(x => x.Exam.ExamDateTime + x.Exam.ExamDuration < DateTime.Now || x.IsFinished==true);
            if(oldStudentExams.Count() <= 0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);

            return new SuccessDataResult<List<StudentExamListDto>>(_mapper.Map<List<StudentExamListDto>>(oldStudentExams), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Öğrenciye ait mevcut zamandaki sınavların listesini döner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<StudentExamListDto>>> GetAllCurrentExamByStudentIdentityIdAsync(string id)
        {
            var student = _studentRepository.GetAsync(x => x.IdentityId == id).Result;
            if (student == null)
            {
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentNotFound]);
            }
            var studentExams = await _studentExamRepository.GetAllAsync(x => x.Student.IdentityId == id);
            if (studentExams.Count() <= 0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            var currentStudentExams = studentExams.Where(x => x.Exam.ExamDateTime <= DateTime.Now && x.Exam.ExamDateTime + x.Exam.ExamDuration > DateTime.Now);
            if(currentStudentExams.Count() <= 0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            return new SuccessDataResult<List<StudentExamListDto>>(_mapper.Map<List<StudentExamListDto>>(currentStudentExams), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// ExamınIdsine göre StudentExamlistesi döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<StudentExamListDto>>> GetAllByExamIdAsync(Guid id)
        {
            var studentExams = await _studentExamRepository.GetAllAsync(x => x.ExamId == id);
            if(studentExams.Count() <= 0)
                return new ErrorDataResult<List<StudentExamListDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            return new SuccessDataResult<List<StudentExamListDto>>(_mapper.Map<List<StudentExamListDto>>(studentExams), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Belirtilen öğrenci sınav kimliğine göre öğrenci sınavını başlatır ve ilgili soruları getirir.
        /// </summary>
        /// <param name="studentExamId">Başlatılacak öğrenci sınavının kimliği.</param>
        /// <returns>
        /// Başarı durumuna göre, öğrenci sınavına ait soruları içeren bir veri sonucu döner.
        /// Başarı durumunda, veri soruların listesiyle birlikte gelir.
        /// Başarısız durumda, hata mesajı içeren bir veri sonucu döner.
        /// </returns>
        public async Task<IDataResult<List<StudentQuestionDetailsDto>>> ExamStartStudentExamIdAsync(Guid studentExamId)
        {

            var studentExam = await _studentExamRepository.GetByIdAsync(studentExamId);
            if (studentExam == null)
                return new ErrorDataResult<List<StudentQuestionDetailsDto>>(_stringLocalizer[Messages.StudentExamNotFound]);
            if (studentExam.Exam == null)
                return new ErrorDataResult<List<StudentQuestionDetailsDto>>(_stringLocalizer[Messages.ExamNotFound]);
            var examStatusResult = IsItTimeStartExam(studentExam.Exam.ExamDateTime, studentExam.Exam.ExamDuration);
            if (examStatusResult.Data)
            {
                if (!studentExam.IsFinished)
                {
                    var studentQuestions = await _studentQuestionRepository.GetAllAsync(x => x.StudentExamId == studentExamId);

                    if (studentQuestions.Count() < 0)
                    {
                        return new ErrorDataResult<List<StudentQuestionDetailsDto>>(_stringLocalizer[Messages.QuestionNotFound]);
                    }

                    var orderedStudentQuestions = studentQuestions.OrderBy(x => x.QuestionOrder);

                    return new SuccessDataResult<List<StudentQuestionDetailsDto>>(_mapper.Map<List<StudentQuestionDetailsDto>>(orderedStudentQuestions), _stringLocalizer[Messages.ListedSuccess]);
                }

                return new ErrorDataResult<List<StudentQuestionDetailsDto>>(_stringLocalizer[Messages.AnswersAlreadyBeenSaved]);

            }
            else
            {
                return new ErrorDataResult<List<StudentQuestionDetailsDto>>(examStatusResult.Message);
            }

        }

        /// <summary>
        /// Belirtilen sınavın başlama zamanını ve süresini kontrol eder.
        /// </summary>
        /// <param name="examDateTime">Sınavın başlama zamanı.</param>
        /// <param name="examDuration">Sınavın süresi.</param>
        /// <returns>
        /// Başarı durumuna göre, sınavın başlama durumunu içeren bir veri sonucu döner.
        /// Başarı durumunda, veri `true` ise sınav başlamaya uygun, `false` ise sınav başlamamıştır.
        /// Başarısız durumda, veri `false` ise sınav süresi doldu, aksi takdirde sınav henüz başlamamıştır.
        /// </returns>
        private IDataResult<bool> IsItTimeStartExam(DateTime examDateTime, TimeSpan examDuration)
        {
            DateTime now = DateTime.Now;
            DateTime examEndTime = examDateTime + examDuration;

            if (now >= examDateTime && now < examEndTime)
            {
                return new SuccessDataResult<bool>(true, _stringLocalizer[Messages.ExamStarted]);
            }
            else if (now < examDateTime)
            {
                return new ErrorDataResult<bool>(false, _stringLocalizer[Messages.ExamNotStartedYet]);
            }
            else
            {
                return new ErrorDataResult<bool>(false, _stringLocalizer[Messages.ExamTimeIsUp]);
            }
        }



        /// <summary>
        /// Belirtilen sınavın bitme zamanını ve süresini kontrol eder.
        /// </summary>
        /// <param name="examDateTime">Sınavın başlama zamanı.</param>
        /// <param name="examDuration">Sınavın süresi.</param>
        /// <returns>
        /// Başarı durumuna göre, sınavın başlama durumunu içeren bir veri sonucu döner.
        /// Başarı durumunda, veri `true` ise sınav başlamaya uygun, `false` ise sınav başlamamıştır.
        /// Başarısız durumda, veri `false` ise sınav süresi doldu, aksi takdirde sınav henüz başlamamıştır.
        /// </returns>
        private IDataResult<bool> IsItTimeEndExam(DateTime examDateTime, TimeSpan examDuration)
        {
            DateTime now = DateTime.Now;
            DateTime examEndTime = examDateTime + examDuration;

            if (now >= examDateTime && now < examEndTime)
            {
                return new SuccessDataResult<bool>(true, _stringLocalizer[Messages.ExamFinished]);
            }
            else if (now < examDateTime)
            {
                return new ErrorDataResult<bool>(false, _stringLocalizer[Messages.ExamNotStartedYet]);
            }
            else
            {
                return new ErrorDataResult<bool>(false, _stringLocalizer[Messages.ExamExpired]);
            }
        }

        /// <summary>
        /// Belirtilen öğrenci sınavının bitiş zamanına göre sınavı tamamlar.Öğrenci cevaplarını kaydeder. Öğrencinin sınav sonucunu hesaplar.
        /// </summary>
        /// <param name="studentExamId">Tamamlanacak öğrenci sınavının kimliği.</param>
        /// <returns>
        /// Başarı durumuna göre, öğrenci sınavının tamamlanma durumunu içeren bir veri sonucu döner.
        /// Başarı durumunda, veri sınavın tamamlandığına dair bilgiyi içerir ve öğrenci sınavının sonucunu hesaplar.
        /// Başarısız durumda, hata mesajı içeren bir veri sonucu döner.
        /// </returns>
        
        public async Task<IResult> ExamFnishedStudentExamIdAsync(List<StudentAnswerCreateDto> studentAnswerCreateDtos, Guid studentExamId)
        {

            var studentExam = await _studentExamRepository.GetByIdAsync(studentExamId);
            if (studentExam == null)
                return new ErrorResult(_stringLocalizer[Messages.StudentExamNotFound]);
            if (studentExam.Exam == null)
                return new ErrorResult(_stringLocalizer[Messages.ExamNotFound]);
            if (!studentExam.IsFinished)
            {
                var examStatusResult = IsItTimeEndExam(studentExam.Exam.ExamDateTime, studentExam.Exam.ExamDuration);
                if (examStatusResult.Data)
                {
                    await _studentAnswerService.AddRangeAsync(studentAnswerCreateDtos);
                    studentExam.IsFinished = true;
                    var score = await CalculateStudentExamResult(studentExamId);
                    studentExam.Score = score.Data;
                    await _studentExamRepository.UpdateAsync(studentExam);
                    await _studentExamRepository.SaveChangesAsync();
                    return new SuccessResult(examStatusResult.Message);
                }
                else
                {
                    return new ErrorResult(examStatusResult.Message);
                }
            }
            return new ErrorResult(_stringLocalizer[Messages.AnswersAlreadyBeenSaved]);
        }
        /// <summary>
        /// Belirtilen öğrenci sınavının sonucunu hesaplar.
        /// </summary>
        /// <param name="studentExamId">Sonuçları hesaplanacak öğrenci sınavının kimliği.</param>
        /// <returns>
        /// Başarı durumuna göre, öğrenci sınavının sonucunu içeren bir veri sonucu döner.
        /// Başarı durumunda, veri öğrenci sınavının başarıyla hesaplanmış sonucunu içerir.
        /// Başarısız durumda, hata mesajı içeren bir veri sonucu döner.
        /// </returns>
        public async Task<IDataResult<int>> CalculateStudentExamResult(Guid studentExamId)
        {
            var studentQuestions = await _studentQuestionRepository.GetAllAsync(x => x.StudentExamId == studentExamId);
            var studentQuestionIds = studentQuestions.Select(x => x.Id).ToList();

            var studentAnswersResult = await _studentAnswerService.GetByStudentQuestionIds(studentQuestionIds);

            if (!studentAnswersResult.IsSuccess)
            {
                return new ErrorDataResult<int>(studentAnswersResult.Message);
            }

            var studentAnswers = studentAnswersResult.Data;


            int studentScore = CalculateScore(studentQuestions, studentAnswers);

            return new SuccessDataResult<int>(studentScore, _stringLocalizer[Messages.ExamScoreCalculationSuccess]);
        }



        /// <summary>
        /// Belirtilen öğrenci soruları ve cevapları kullanarak öğrencinin sınav puanını hesaplar.
        /// </summary>
        /// <param name="studentQuestions">Öğrenci soruları listesi.</param>
        /// <param name="studentAnswers">Öğrenci cevapları listesi.</param>
        /// <returns>Öğrencinin sınav puanı.</returns>
        private int CalculateScore(IEnumerable<StudentQuestion> studentQuestions, List<StudentAnswerDto> studentAnswers)
        {
            int studentScore = 0;

            foreach (var question in studentQuestions)
            {
                var studentAnswer = studentAnswers.FirstOrDefault(x => x.StudentQuestionId == question.Id);

                if (studentAnswer != null && studentAnswer.IsSelected)
                {
                    if (studentAnswer.IsCorrect)
                    {
                        studentScore += question.Score;
                    }
                }
            }

            return studentScore;
        }


    }
}
