using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Students;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace BACampusApp.WebApi.Validators.StudentValidator
{
    public class StudentCurrentUserUpdateDtoValidator : AbstractValidator<StudentCurrentUserUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentCurrentUserUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;

            RuleFor(x => x.PhoneNumber)
               .Matches(@"^+?\d[\d\s]*$").WithMessage($"{_stringLocalizer[Messages.StudentPhoneNumberMatches]}")
               .MinimumLength(7).WithMessage($"{_stringLocalizer[Messages.StudentPhoneNumberLength]}");

            RuleFor(x => x.CountryCode)
                .MaximumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentCountryLength]}")
                .Must(IsValidCountry).WithMessage($"{_stringLocalizer[Messages.StudentCountryControl]}");

            RuleFor(x => x.Address)
                           .NotEmpty().WithMessage($"{_stringLocalizer[Messages.StudentAddressNotNull]}")
                           .NotNull().WithMessage($"{_stringLocalizer[Messages.StudentAddressNotNull]}")
                           .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.StudentAddressLength]}")
                           .Must(address => !Regex.IsMatch(address, @"[*?)(*+@<>₺&%+^^'!#]")).WithMessage($"{_stringLocalizer[Messages.StudentAddressNoSpecialChars]}");
            RuleFor(dto => dto.Image)
                .Must(HaveValidFileExtension).WithMessage($"{_stringLocalizer[Messages.StudentImageExtension]}")
                .Must((dto, image) => {
                    if (image != null)
                    {
                        long fileSizeInBytes = image.Length;
                        long fileSizeInMB = fileSizeInBytes / (1024 * 1024);
                        return fileSizeInMB <= 10;
                    }
                    return true;
                }).WithMessage($"{_stringLocalizer[Messages.StudentImageFileSize]}");
        }
        private static bool IsValidCountry(string Country)
        {
            if (Country == null || Country == "")
            {
                return true;
            }
            else if (Country.Length < 2)
            {
                return false;
            }
            return true;

        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;

            if (dateOfBirth > today.AddYears(-age))
                age--;

            return age;
        }
        private bool HaveValidFileExtension(IFormFile file)
        {
            if (file == null)
            {
                return true;
            }

            string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };
            string extension = Path.GetExtension(file.FileName)?.ToLower();

            return !string.IsNullOrEmpty(extension) && allowedExtensions.Contains(extension);
        }
    }
}
