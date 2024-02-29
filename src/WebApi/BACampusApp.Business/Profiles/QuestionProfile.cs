using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionAndAnswersDto>();
            CreateMap<Question, QuestionDto>();
            CreateMap<Question, QuestionAndAnswerListDto>()
                      .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.QuestionTopic.TopicName));

            CreateMap<QuestionCreateDto, Question>();
            CreateMap<QuestionUpdateDto, Question>()
                     .ForMember(dest => dest.Id, opt => opt.Ignore())
                     .ForMember(dest => dest.QuestionAnswers, opt => opt.MapFrom(src => src.QuestionAnswers))
                     .ForMember(dest => dest.CreatedBy, opt => opt.NullSubstitute("defaultValue")); 


            CreateMap<QuestionUpdateDto, QuestionAndAnswersDto>();
            CreateMap<QuestionAndAnswersDto, Question>();
            CreateMap<Question, QuestionListDto>()
                    .ForMember(dest => dest.TopicName, opt => opt.MapFrom(src => src.QuestionTopic.TopicName));

        }
    }
}
