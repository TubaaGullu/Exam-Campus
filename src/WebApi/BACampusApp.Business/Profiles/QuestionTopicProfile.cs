using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class QuestionTopicProfile :Profile
    {
        public QuestionTopicProfile()
        {
            CreateMap<QuestionTopicCreateDto, QuestionTopic>()
                .ForMember(dest => dest.TopicName,
            config => config.MapFrom(src => src.TopicName.Trim()));

            CreateMap<QuestionTopicUpdateDto, QuestionTopic>()
                .ForMember(dest => dest.TopicName,
            config => config.MapFrom(src => src.TopicName.Trim()));

            CreateMap<QuestionTopic, QuestionTopicDto>();
            CreateMap<QuestionTopic, QuestionTopicListDto>();
            CreateMap<QuestionTopic, QuestionTopicDeletedListDto>();
        }
    }
}
