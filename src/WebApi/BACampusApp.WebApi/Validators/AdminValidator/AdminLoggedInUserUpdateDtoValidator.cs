using System.Text.RegularExpressions;
using BACampusApp.Business;
using BACampusApp.Business.Constants;
using BACampusApp.Dtos.Admin;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace BACampusApp.WebApi.Validators.AdminValidator
{
    public class AdminLoggedInUserUpdateDtoValidator : AbstractValidator<AdminLoggedInUserUpdateDto>
    {
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public AdminLoggedInUserUpdateDtoValidator(IStringLocalizer<Resource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            RuleFor(x => x.FirstName)
               .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminFirstNameNotNull]}")
               .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminFirstNameNotNull]}")
               .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminFirstNameLength]}")
               .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.AdminFirstNameMatches]}");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminLastNameNotNull]}")
                .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminLastNameNotNull]}")
                .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminLastNameLength]}")
                .Matches("^[a-zA-ZğüşıöçĞÜŞİÖÇ ]+$").WithMessage($"{_stringLocalizer[Messages.AdminLastNameMatches]}");

            RuleFor(x => x.PhoneNumber)
               .Matches(@"^+?\d[\d\s]*$").WithMessage($"{_stringLocalizer[Messages.AdminPhoneNumberMatches]}")
               .MinimumLength(7).WithMessage($"{_stringLocalizer[Messages.AdminPhoneNumberLength]}");

            RuleFor(x => x.CountryCode)
                .MaximumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminCountryCodeLength]}")
                .Must(IsValidCountry).WithMessage($"{_stringLocalizer[Messages.AdminCountryCodeControl]}");

            RuleFor(x => x.DateOfBirth)
                .Must(dateOfBirth => CalculateAge(dateOfBirth) >= 18)
                .WithMessage($"{_stringLocalizer[Messages.AdminDateOfBirthLessThan]}")
                .Must(dateOfBirth => CalculateAge(dateOfBirth) <= 65)
                .WithMessage($"{_stringLocalizer[Messages.AdminDateOfBirthGreaterThan]}");
            RuleFor(x => x.Address)
                           .NotEmpty().WithMessage($"{_stringLocalizer[Messages.AdminAddressNotNull]}")
                           .NotNull().WithMessage($"{_stringLocalizer[Messages.AdminAddressNotNull]}")
                           .MaximumLength(256).MinimumLength(2).WithMessage($"{_stringLocalizer[Messages.AdminAddressLength]}")
                           .Must(address => !Regex.IsMatch(address, @"[*?)(*+@<>₺&%+^^'!#]")).WithMessage($"{_stringLocalizer[Messages.AdminAddressNoSpecialChars]}");
            RuleFor(dto => dto.Image)
                .Must(HaveValidFileExtension).WithMessage($"{_stringLocalizer[Messages.AdminImageExtension]}")
                .Must((dto, image) => {
                    if (image != null)
                    {
                        long fileSizeInBytes = image.Length;
                        long fileSizeInMB = fileSizeInBytes / (1024 * 1024);
                        return fileSizeInMB <= 10;
                    }
                    return true;
                }).WithMessage($"{_stringLocalizer[Messages.AdminImageFileSize]}");
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
