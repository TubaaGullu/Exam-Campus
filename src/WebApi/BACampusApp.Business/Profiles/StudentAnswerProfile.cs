using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class StudentAnswerProfile : Profile
    {
        public StudentAnswerProfile()
        {
            CreateMap<StudentAnswer, StudentAnswerDto>();
            CreateMap<StudentAnswerCreateDto, StudentAnswer>();
        }
    }
}
