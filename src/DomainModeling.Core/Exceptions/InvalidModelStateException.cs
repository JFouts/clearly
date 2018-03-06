using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModeling.Core.Exceptions
{
    public class InvalidModelStateException : Exception
    {
        public IEnumerable<ValidationResult> ValidationErrors { get; }

        public InvalidModelStateException(IEnumerable<ValidationResult> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}