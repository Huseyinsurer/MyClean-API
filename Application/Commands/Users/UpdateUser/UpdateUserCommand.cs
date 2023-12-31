using Application.Dtos;
using Domain.Models.User;
using MediatR;
using System;

public class UpdateUserCommand : IRequest<UserModel>
{
    public Guid UserId { get; }
    public UserDto UpdatedUser { get; }

    public UpdateUserCommand(Guid userId, UserDto updatedUser)
    {
        UserId = userId;
        UpdatedUser = updatedUser;
    }
}
