using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentQuestion;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentQuestion
{
    public class StudentQuestionUpdateValidator :AbstractValidator<StudentQuestionUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public StudentQuestionUpdateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(t0 => t0.QuestionId).Must((rootObject, list) => rootObject.QuestionId != Guid.Empty).WithMessage($"{_stringLocalizer[Messages.QuestionChoose]}");
            RuleFor(x => x.QuestionOrder).NotEmpty().WithMessage($"{_stringLocalizer[Messages.QuestionOrderNotEmpty]}")
            .GreaterThan(0).WithMessage($"{_stringLocalizer[Messages.QuestionOrderGreatherThanZero]}");

        }
    }
}
