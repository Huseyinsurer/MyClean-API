using System;

namespace Application.Exceptions
{
    public class CatNotExistException : Exception
    {
        public CatNotExistException(Guid catId)
            : base($"Cat not Exist with ID: {catId}")
        {
        }
    }
}