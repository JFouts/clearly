using System;

namespace DomainModeling.Core.Exceptions {
    public class NotFoundException : Exception {
        public Guid Id { get; }

        public NotFoundException(Guid id): base($"Entity with id {id} was not found.") { 
            Id = id;
        }
    }
}