using System;

namespace DomainModeling.Core.Exceptions
{
    public class InvalidTypeException : Exception
    {
        public InvalidTypeException(string message)
            :base(message)
        {
        }
    }
}