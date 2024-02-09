
using MediatR;

namespace Application.Commands.Users.Delete
{
    public class DeleteUserResult : IRequest
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
