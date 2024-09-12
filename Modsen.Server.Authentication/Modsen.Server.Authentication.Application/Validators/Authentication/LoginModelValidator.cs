using FluentValidation;
using Modsen.Server.Authentication.Application.Models.Authentication;

namespace Modsen.Server.Authentication.Application.Validators.Authentication
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(loginModel => loginModel.UserName)
                .NotEmpty()
                .WithMessage("User name is required");

            RuleFor(loginModel => loginModel.Password)
                .NotEmpty()
                .WithMessage("Password is required");
        }
    }
}
