// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.RestApi;

/// <summary>
/// Represents and error when a required object is not found
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Type of the object that was not found
    /// </summary>
    public Type? Type { get; set; }
    
    /// <summary>
    /// Exception when a required object is not found
    /// </summary>
    /// <param name="type">Type of the object that was not found</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotFoundException(Type? type = null, string? message = null, Exception? innerException = null)
        : base(GetMessage(message, type), innerException)
    {
        Type = type;
    }
    
    private static string GetMessage(string? message = null, Type? type = null)
    {
        return message ??
            (type != null ? $"A required object of type {type.Name} was not found!" : "A required object was not found!");
    }
}
