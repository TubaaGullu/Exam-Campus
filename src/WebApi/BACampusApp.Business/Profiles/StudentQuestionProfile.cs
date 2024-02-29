using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class StudentQuestionProfile : Profile
    {
        public StudentQuestionProfile()
        {
            CreateMap<StudentQuestion, StudentQuestionDto>();
            CreateMap<StudentQuestion, SelectedStudentQuestionDto>();
            CreateMap<StudentQuestion, StudentQuestionListDto>();
            CreateMap<StudentQuestion, StudentQuestionDetailsDto>()
                .ForMember(dest => dest.QuestionText, opt => opt.MapFrom(src => src.Question.QuestionText))               
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Question.Image))
                .ForMember(dest => dest.QuestionAnswers, opt => opt.MapFrom(src => src.Question.QuestionAnswers))
                .ForMember(dest => dest.ExamDuration, opt => opt.MapFrom(src => src.StudentExam.Exam.ExamDuration))
                .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.StudentExam.Exam.ExamDateTime));

            CreateMap<StudentQuestionCreateDto, StudentQuestion>();
            CreateMap<StudentQuestionUpdateDto, StudentQuestion>();
        }
    }
}
