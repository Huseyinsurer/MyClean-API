using System;

namespace Application.Exceptions
{
    public class DogCreationException : Exception
    {
        public DogCreationException(string message)
            : base($"Dog creation failed: {message}")
        {
        }
    }
}
