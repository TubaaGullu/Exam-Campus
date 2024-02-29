using BACampusApp.Dtos.StudentAnswer;
using BACampusApp.Entities.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class StudentAnswerManager :IStudentAnswerService
    {
        private readonly IStudentAnswerRepository _studentAnswerRepository;
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public StudentAnswerManager(IStudentAnswerRepository studentAnswerRepository, IMapper mapper, IQuestionAnswerRepository questionAnswerRepository, IStringLocalizer<Resource> stringLocalizer)
        {
            _studentAnswerRepository = studentAnswerRepository;
            _mapper = mapper;
            _questionAnswerRepository = questionAnswerRepository;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// Öğrencinin vermiş olduğu cevapları liste olarak kaydetmek için kullanılır.
        /// </summary>
        /// <param name="studentAnswerCreateDtos">List<AnswerOfStudentDto></param>
        /// <returns>DataResult tipinde öğrencinin cevapları.</returns>
        public async Task<IDataResult<List<StudentAnswerDto>>> AddRangeAsync(List<StudentAnswerCreateDto> studentAnswerCreateDtos)
        {
            var studentAnswers = new List<StudentAnswer>();
            foreach (var studentAnswerCreateDto in studentAnswerCreateDtos)
            {
                var hasStudentAnswer = await _studentAnswerRepository.AnyAsync(x => x.QuestionAnswerId == studentAnswerCreateDto.QuestionAnswerId && x.StudentQuestionId == studentAnswerCreateDto.StudentQuestionId);
                if (hasStudentAnswer)
                {
                    return new ErrorDataResult<List<StudentAnswerDto>>(_stringLocalizer[Messages.StudentAnswerNotSaved]);
                }
                var studentAnswer = _mapper.Map<StudentAnswer>(studentAnswerCreateDto);
                await _studentAnswerRepository.AddAsync(studentAnswer);
                studentAnswers.Add(studentAnswer);
            }
            await _studentAnswerRepository.SaveChangesAsync();
            return new SuccessDataResult<List<StudentAnswerDto>>(_mapper.Map<List<StudentAnswerDto>>(studentAnswers), _stringLocalizer[Messages.StudentAnswersSaved]);
        }



        /// <summary>
        /// Verilen öğrenci cevaplarının doğruluk durumunu belirler.
        /// </summary>
        /// <param name="studentAnswerDtos">Doğruluk durumu ayarlanacak öğrenci cevapları.</param>
        /// <returns>Doğruluk durumu ayarlanmış öğrenci cevapları.</returns>
        private async Task<IDataResult<List<StudentAnswerDto>>> SetIsCorrectProperty(List<StudentAnswerDto> studentAnswerDtos)
        {
            var questionAnswerIds= studentAnswerDtos.Select(x=>x.QuestionAnswerId).ToList();
         
                var questionAnswers = await _questionAnswerRepository.GetAllAsync(x => questionAnswerIds.Contains(x.Id));
                foreach (var studentAnswerDto in studentAnswerDtos)
                {
                    if (studentAnswerDto.IsSelected)
                    {
                        var questionAnswer = questionAnswers.FirstOrDefault(qa => qa.Id == studentAnswerDto.QuestionAnswerId);

                        if (questionAnswer != null)
                        {
                            studentAnswerDto.IsCorrect = questionAnswer.IsRightAnswer;
                        }
                    }
                }
                return new SuccessDataResult<List<StudentAnswerDto>>(studentAnswerDtos, _stringLocalizer[Messages.IsCorrectAssigned]);
       
        }

        /// <summary>
        /// Belirli öğrenci soru kimliklerine göre öğrenci cevaplarını getirir ve cevapların doğruluk durumunu ayarlar.
        /// </summary>
        /// <param name="studentQuestionIds">Öğrenci soru kimlikleri.</param>
        /// <returns>Öğrenci cevaplarını içeren bir veri sonucu.</returns>
        public async Task<IDataResult<List<StudentAnswerDto>>> GetByStudentQuestionIds(List<Guid> studentQuestionIds)
        {
            var studentAnswers = await _studentAnswerRepository.GetAllAsync(x => studentQuestionIds.Contains(x.StudentQuestionId));
            if (studentAnswers.Any())
            {
                var studentAnswerDtos = _mapper.Map<List<StudentAnswerDto>>(studentAnswers);

                var isCorrectDto = await SetIsCorrectProperty(studentAnswerDtos);

                if (!isCorrectDto.IsSuccess)
                {
                    return new ErrorDataResult<List<StudentAnswerDto>>(_stringLocalizer[isCorrectDto.Message]);
                }

                return new SuccessDataResult<List<StudentAnswerDto>>(isCorrectDto.Data, _stringLocalizer[Messages.AddSuccess]);
            }
            return new ErrorDataResult<List<StudentAnswerDto>>(_stringLocalizer[Messages.StudentAnswerNotFound]);

        }
    }
}
