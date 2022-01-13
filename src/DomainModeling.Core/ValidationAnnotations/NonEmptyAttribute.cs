using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using DomainModeling.Core.Exceptions;

namespace DomainModeling.Core.ValidationAnnotations
{
    public class NonEmptyAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            const ValidationResult? success = null;
            var error = new ValidationResult($"{validationContext?.DisplayName} must be non-empty.");

            switch (value)
            {
                case string s:
                    return string.IsNullOrWhiteSpace(s) ? error : success;
                case IEnumerable l:
                    return l.GetEnumerator().MoveNext() ? success : error;
                case null:
                    return error;
                default:
                    throw new InvalidTypeException($"{nameof(NonEmptyAttribute)} can only be applied to {nameof(String)} or {nameof(IEnumerable)} but was applied to {value.GetType().Name}");
            }
        }

    }
}
