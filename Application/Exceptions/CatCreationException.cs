using System;

namespace Application.Exceptions
{
    public class CatCreationException : Exception
    {
        public CatCreationException(string message)
            : base($"Cat creation failed: {message}")
        {
        }
    }
}
