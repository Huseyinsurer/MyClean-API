using System;

namespace Application.Exceptions
{
    public class DogNotExistException : Exception
    {
        public DogNotExistException(Guid dogId)
            : base($"Dog not Exist with ID: {dogId}")
        {
        }
    }
}