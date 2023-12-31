using System;

namespace Application.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException(string username)
            : base($"User with username '{username}' already exists.")
        {
        }
    }
}
