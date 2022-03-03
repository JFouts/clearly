// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Clearly.Core.Exceptions;

namespace Clearly.Core.ValidationAnnotations;

public class MarkdownAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
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
