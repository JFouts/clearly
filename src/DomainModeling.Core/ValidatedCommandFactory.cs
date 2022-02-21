// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;
using DomainModeling.Core.Exceptions;

namespace DomainModeling.Core;

public class ValidatedCommandFactory : CommandFactory
{
    public override T Create<T>(Func<T> instantiationFunction)
    {
        var command = base.Create(instantiationFunction);
        AssertModelValid(command);
        return command;
    }

    private static void AssertModelValid(object model)
    {
        var validationContext = new ValidationContext(model);
        var results = new List<ValidationResult>();

        // TODO: Validator is a hidden dependency let's pull that out and inject it
        if (!Validator.TryValidateObject(model, validationContext, results))
        {
            throw new InvalidModelStateException(results);
        }
    }
}
