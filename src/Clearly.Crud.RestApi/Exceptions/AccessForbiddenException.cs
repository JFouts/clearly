// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Crud.RestApi;

/// <summary>
/// Represents and error when a user is authenticated but is explicitly forbidden from taking
/// the requested action.
/// </summary>
public class AccessForbiddenException : Exception
{
    /// <summary>
    /// UserName of the account that requested access, but was forbidden.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Exception when a user is authenticated but is explicitly forbidden from taking.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="userName">UserName of the account that requested access, but was forbidden.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public AccessForbiddenException(string? message = null, string? userName = null, Exception? innerException = null)
        : base(GetMessage(message, userName), innerException)
    {
    }

    private static string GetMessage(string? message = null, string? userName = null)
    {
        return message ??
            (userName != null ? $"Access was forbidden to user {userName}!" : "Access was forbidden to user!");
    }
}
