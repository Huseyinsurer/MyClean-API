using MediatR;
using System;

namespace Application.Commands.Users.Delete
{
    public class DeleteUserCommand : IRequest<DeleteUserResult>
    {
        public Guid UserId { get; }

        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
