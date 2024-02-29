using BACampusApp.Business.Abstracts;
using BACampusApp.Dtos.StudentQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class StudentQuestionManager : IStudentQuestionService
    {
        private readonly IStudentQuestionRepository _studentQuestionRepository;
        private readonly IMapper _mapper;
        private readonly IStudentExamService _studentExamService;
        private readonly IExamService _examService;
        private readonly IQuestionService _questionService;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public StudentQuestionManager(IStudentQuestionRepository studentQuestionRepository, IMapper mapper, IStudentExamService studentExamService, IExamService examService, IQuestionService questionService, IStringLocalizer<Resource> stringLocalizer)
        {
            _studentQuestionRepository = studentQuestionRepository;
            _mapper = mapper;
            _studentExamService = studentExamService;
            _examService = examService;
            _questionService = questionService;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// id sine göre studentquestion döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<StudentQuestionDto>> GetByIdAsync(Guid id)
        {
            var studentQuestion = await _studentQuestionRepository.GetByIdAsync(id);
            if (studentQuestion == null)
            {
                return new ErrorDataResult<StudentQuestionDto>(_stringLocalizer[Messages.StudentQuestionNotFound]);
            }
            return new SuccessDataResult<StudentQuestionDto>(_mapper.Map<StudentQuestionDto>(studentQuestion), _stringLocalizer[Messages.StudentQuestionFoundSuccess]);
        }

        /// <summary>
        /// studentexamIdye göre öğrencilerin sorularını döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IDataResult<List<StudentQuestionListDto>>> GetByStudentExamIdAsync(Guid id)
        {
            var studentQuestions = await _studentQuestionRepository.GetAllAsync(x => x.StudentExamId == id);

            if (studentQuestions.Count()<0)
            {
                return new ErrorDataResult<List<StudentQuestionListDto>>(_stringLocalizer[Messages.StudentQuestionNotFound]);
            }

            return new SuccessDataResult<List<StudentQuestionListDto>>(_mapper.Map<List<StudentQuestionListDto>>(studentQuestions), _stringLocalizer[Messages.StudentQuestionFoundSuccess]);
        }

        


    }
}
