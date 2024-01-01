using MediatR;
using Domain.Models.User;
using Application.Dtos;

namespace Application.Commands.Users.RegisterUser
{
    public class CreateUserCommand : IRequest<UserModel>
    {
        public UserDto NewUser { get; }

        public CreateUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }
    }
}
