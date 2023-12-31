using System;

namespace Application.Exceptions
{
    public class UserNotExistException : Exception
    {
        public UserNotExistException(string username)
            : base($"User not found with username: {username}")
        {
        }
    }
}