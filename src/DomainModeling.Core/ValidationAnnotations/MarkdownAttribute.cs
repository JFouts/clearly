using System;
using System.ComponentModel.DataAnnotations;
using DomainModeling.Core.Exceptions;

namespace DomainModeling.Core.ValidationAnnotations
{
    public class MarkdownAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            switch (value)
            {
                case null:
                case string _:
                    return null;
                default:
                    throw new InvalidTypeException($"{nameof(MarkdownAttribute)} can only be applied to {nameof(String)} but was applied to {value.GetType().Name}");
            }
        }
    }
}
