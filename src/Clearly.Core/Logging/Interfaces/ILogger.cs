// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Clearly.Core.Logging;

// TODO: Remove this and just use MSFT ILogger
public interface ILogger<T>
{
    void LogWarning(string message);
    void LogError(Exception exception, string message);
    void LogInformation(string message);
}
