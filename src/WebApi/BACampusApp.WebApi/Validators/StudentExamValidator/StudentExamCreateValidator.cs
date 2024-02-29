using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Exam;
using BACampusApp.Dtos.StudentExam;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.StudentExamValidator
{
    public class StudentExamCreateValidator : AbstractValidator<StudentExamCreateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public StudentExamCreateValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(t0 => t0.StudentId).Must((rootObject, list) => rootObject.StudentId != Guid.Empty).WithMessage($"{_stringLocalizer[Messages.StudentChoose]}");
        }
    }
}
