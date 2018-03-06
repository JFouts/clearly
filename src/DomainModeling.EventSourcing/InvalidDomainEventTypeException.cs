using System;
using System.Runtime.Serialization;

namespace DomainModeling.EventSourcing
{
    [Serializable]
    internal class InvalidDomainEventTypeException : Exception
    {
        public InvalidDomainEventTypeException()
        {
        }

        public InvalidDomainEventTypeException(string message) : base(message)
        {
        }

        public InvalidDomainEventTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidDomainEventTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}