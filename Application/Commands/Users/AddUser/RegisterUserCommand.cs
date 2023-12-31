using MediatR;
using Domain.Models.User;
using Application.Dtos;

namespace Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommand : IRequest<UserModel>
    {
        public UserDto NewUser { get; }

        public RegisterUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }
    }
}
