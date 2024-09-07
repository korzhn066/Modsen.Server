using FluentValidation;
using Modsen.Server.Authentication.Application.Features.ApplicationUser.Commands;
using Modsen.Server.Authentication.Application.Models.Authentication;

namespace Modsen.Server.Authentication.Application.Validators.Authentication
{
    public class RegisterApplicationUserValidator : AbstractValidator<RegisterApplicationUser>
    {
        public RegisterApplicationUserValidator()
        {
            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.UserName)
                .NotEmpty()
                .WithMessage("User name is required");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.PhoneNumber)
                .Matches(@"^375\(\d{2}\)\d{3}-\d{2}-\d{2}")
                .WithMessage("Invalid phone number");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.Password)
                .NotEmpty()
                .WithMessage("Password is required");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.Password)
                .Matches(@"\d")
                .WithMessage("Password must contain digit");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.Password)
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain upper case letter");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.Password)
                .Matches(@"[a-z]")
                .WithMessage("Password must contain lower case letter");

            RuleFor(registerApplicationUser => registerApplicationUser.RegisterModel.Password)
                .MinimumLength(6)
                .WithMessage("Minimum password length is 6");
        }
    }
}
