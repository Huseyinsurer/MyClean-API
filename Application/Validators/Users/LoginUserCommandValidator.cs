using Application.Commands.Users.Login;
using Application.Dtos;
using FluentValidation;

namespace Application.Validators.Users
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(command => command.LoginUser)
                .NotNull().WithMessage("User information is required.");

            RuleFor(command => command.LoginUser.Username)
                .NotEmpty().WithMessage("Username cannot be empty.")
                .NotNull().WithMessage("Username cannot be null");

            RuleFor(command => command.LoginUser.Userpassword)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .NotNull().WithMessage("Password cannot be null");
        }
    }
}
