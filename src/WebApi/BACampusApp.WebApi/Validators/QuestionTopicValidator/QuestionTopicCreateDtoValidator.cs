using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.QuestionTopic;
using FluentValidation;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.QuestionTopicValidator
{
    public class QuestionTopicCreateDtoValidator :AbstractValidator<QuestionTopicCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public QuestionTopicCreateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.TopicName).NotEmpty().WithMessage($"{ _stringLocalizer[Messages.QuestionTopicNameNotEmpty]}")
                                .MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.QuestionTopicNameLength]}");
        }
    }
}
