using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.StudentExam;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentExamValidator
{
    public class StudentExamUpdateValidator :AbstractValidator<StudentExamUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public StudentExamUpdateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(t0 => t0.StudentId).Must((rootObject, list) => rootObject.StudentId != Guid.Empty).WithMessage($"{_stringLocalizer[Messages.StudentChoose]}");
        }
    }
}
