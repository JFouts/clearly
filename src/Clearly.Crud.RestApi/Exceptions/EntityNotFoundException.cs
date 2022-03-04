// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.RestApi;

/// <summary>
/// Represents and error when a required Entity is not found
/// </summary>
public class EntityNotFoundException : NotFoundException
{
    /// <summary>
    /// Id of the Entity that was not found
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Exception when a required Entity is not found
    /// </summary>
    /// <param name="type">Type of the object that was not found</param>
    /// <param name="id">Id of the object that was not found</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public EntityNotFoundException(Type? type = null, Guid? id = null, string? message = null, Exception? innerException = null)
        : base(type, GetMessage(message, type, id), innerException)
    {
        Type = type;
        Id = id;
    }
    
    private static string GetMessage(string? message = null, Type? type = null, Guid? id = null)
    {
        if (message != null)
        {
            return message;
        }

        if (type != null && id != null)
        {
            return $"A required Entity of type {type.Name} with id {id} was not found!";
        }

        if (type != null)
        {
            return $"A required Entity of type {type.Name} was not found!";
        }

        if (type != null)
        {
            return $"A required Entity with id {id} was not found!";
        }

        return "A required Entity was not found!";
    }
}
