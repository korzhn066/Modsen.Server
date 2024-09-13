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
                .Matches(@"\d")
                .Matches(@"[A-Z]")
                .Matches(@"[a-z]")
                .MinimumLength(6)
                .WithMessage("Password is incorrect");
        }
    }
}
