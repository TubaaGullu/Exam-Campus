using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class StudentExamProfile : Profile
    {
        public StudentExamProfile()
        {
            CreateMap<StudentExam, StudentExamDto>()
                .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName));

            CreateMap<StudentExam, SelectedStudentExamDto>();
            CreateMap<StudentExam, StudentExamListDto>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
                .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
                .ForMember(dest => dest.ExamDuration, opt => opt.MapFrom(src => src.Exam.ExamDuration))
                .ForMember(dest => dest.ExamId, opt => opt.MapFrom(src => src.Exam.Id))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                .ForMember(dest => dest.MaxScore, opt => opt.MapFrom(src => src.Exam.MaxScore));



            CreateMap<StudentExam, StudentExamsDetailsDto>()
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Exam.Name))
                .ForMember(dest => dest.ExamDateTime, opt => opt.MapFrom(src => src.Exam.ExamDateTime))
                .ForMember(dest => dest.StudentFullName, opt => opt.MapFrom(src => src.Student.FullName));


            CreateMap<StudentExamCreateDto, StudentExam>();
            CreateMap<StudentExamUpdateDto, StudentExam>();
        }
    }
}
