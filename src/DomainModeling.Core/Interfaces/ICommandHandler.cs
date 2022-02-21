// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace DomainModeling.Core.Interfaces;

public interface ICommandHandler<in T>
    where T : Command
{
    Task ExecuteAsync(T command);
}
