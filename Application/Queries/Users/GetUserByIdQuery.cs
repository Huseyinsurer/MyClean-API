using MediatR;
using Domain.Models.User;
using System;

namespace Application.Queries.Users.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserModel>
    {
        public GetUserByIdQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
