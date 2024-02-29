using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.QuestionAnswer;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.QuestionAnswerValidator
{
    public class QuestionAnswerUpdateDtoValidator :AbstractValidator<QuestionAnswerUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public QuestionAnswerUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(dto => dto.Answer).Cascade(CascadeMode.StopOnFirstFailure).
               NotEmpty().WithMessage($"{_stringLocalizer[Messages.AnswerNotEmpty]}").
               MaximumLength(1000).MinimumLength(1).WithMessage($"{_stringLocalizer[Messages.AnswerLength]}");
        }
    }
}
