using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Question;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.QuestionValidator
{
    public class QuestionUpdateDtoValidator :AbstractValidator<QuestionUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public QuestionUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(dto => dto.QuestionText).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.QuestionTextNotEmpty]}").
               MaximumLength(1000).MinimumLength(10).WithMessage($"{_stringLocalizer[Messages.QuestionTextLength]}");

            RuleFor(t0 => t0.QuestionTopicId).Must((rootObject, list) => rootObject.QuestionTopicId != Guid.Empty).WithMessage($"{_stringLocalizer[Messages.QuestionTopicChoose]}");

        }
    }
}
