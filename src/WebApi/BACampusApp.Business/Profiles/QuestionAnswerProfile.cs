using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class QuestionAnswerProfile : Profile
    {
        public QuestionAnswerProfile()
        {
            CreateMap<QuestionAnswer, QuestionAnswerDto>().ReverseMap();
            CreateMap<QuestionAnswer, QuestionAnswerForStudentDto>().ReverseMap();
            CreateMap<QuestionAnswerCreateDto, QuestionAnswer>();
            CreateMap<QuestionAnswerUpdateDto, QuestionAnswer>();
            CreateMap<QuestionAnswerUpdateDto, QuestionAnswerCreateDto>();
        }
    }
}
