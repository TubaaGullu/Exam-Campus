using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Exam;
using BACampusApp.WebApi.Validators.StudentExamValidator;
using BACampusApp.WebApi.Validators.StudentQuestion;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.ExamValidator
{
    public class ExamUpdateDtoValidator:AbstractValidator<ExamUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ExamUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(dto => dto.Name).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.ExamNameNotEmpty]}").
               MaximumLength(500).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.ExamNameLength]}");
            RuleFor(x => x.ExamDuration).NotEmpty().WithMessage($"{_stringLocalizer[Messages.ExamDurationNotEmpty]}");
            RuleFor(x => x.ExamDateTime).NotEmpty().WithMessage($"{_stringLocalizer[Messages.ExamDateTimeNotEmpty]}")
            .GreaterThan(DateTime.Now).WithMessage($"{_stringLocalizer[Messages.ExamDateTimeNotInPast]}");
            RuleFor(x => x.StudentExams)
            .Must(students => students != null && students.Any())
            .WithMessage($"{_stringLocalizer[Messages.StudentChoose]}");
            RuleFor(x => x.StudentQuestions)
            .Must(questions => questions != null && questions.Any())
            .WithMessage($"{_stringLocalizer[Messages.QuestionChoose]}");
        }
    }
}
