// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Clearly.Core.Exceptions;

public class InvalidModelStateException : Exception
{
    public IEnumerable<ValidationResult> ValidationErrors { get; }

    public InvalidModelStateException(IEnumerable<ValidationResult> validationErrors)
    {
        ValidationErrors = validationErrors;
    }
}
