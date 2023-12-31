using System;

namespace Application.Exceptions
{
    public class UserIdNotExistException : Exception
    {
        public UserIdNotExistException(Guid userId)
            : base($"User not found with username: {userId}")
        {
        }
    }
}