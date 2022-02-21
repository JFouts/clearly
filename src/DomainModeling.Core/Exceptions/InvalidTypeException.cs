// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Core.Exceptions;

public class InvalidTypeException : Exception
{
    public InvalidTypeException(string message)
        : base(message)
    {
    }
}
