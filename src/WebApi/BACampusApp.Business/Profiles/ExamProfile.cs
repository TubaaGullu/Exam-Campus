using BACampusApp.Dtos.Exam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Profiles
{
    public class ExamProfile : Profile
    {
        public ExamProfile()
        {
            CreateMap<Exam, ExamDto>()
                         .ForMember(dest => dest.StudentQuestions, opt => opt.MapFrom(src => src.StudentExams.SelectMany(se => se.StudentQuestions)))
                         .ForMember(dest => dest.StudentExams, opt => opt.MapFrom(src => src.StudentExams));
            CreateMap<Exam, SelectedExamDto>()
                         .ForMember(dest => dest.StudentQuestions, opt => opt.MapFrom(src => src.StudentExams.SelectMany(se => se.StudentQuestions)))
                         .ForMember(dest => dest.StudentExams, opt => opt.MapFrom(src => src.StudentExams));

            CreateMap<Exam, ExamDetailDto>()
                         .ForMember(dest => dest.StudentQuestions, opt => opt.MapFrom(src => src.StudentExams.SelectMany(se => se.StudentQuestions.Select(sq=>sq.Question))))
                         .ForMember(dest => dest.StudentExams, opt => opt.MapFrom(src => src.StudentExams));

            CreateMap<Exam, ExamListDto>();
            CreateMap<ExamCreateDto, Exam>()
                        .AfterMap((src, dest) =>
                        {    foreach (var se in dest.StudentExams)
                            {
                                se.StudentQuestions = src.StudentQuestions.Select(dto => new StudentQuestion
                                {
                                    Score = dto.Score,
                                    QuestionOrder = dto.QuestionOrder,
                                    QuestionId=dto.QuestionId,
                                }).ToList();
                            }
                        });
            CreateMap<ExamUpdateDto, Exam>()
                .AfterMap((src, dest) =>
                {
                    foreach (var se in dest.StudentExams)
                    {
                        se.StudentQuestions = src.StudentQuestions.Select(dto => new StudentQuestion
                        {
                            Score = dto.Score,
                            QuestionOrder = dto.QuestionOrder,
                            QuestionId = dto.QuestionId,
                        }).ToList();
                    }
                });
            CreateMap<ExamUpdateDto, Exam>();
            CreateMap<ExamUpdateDto, Exam>();        }
    }
}
