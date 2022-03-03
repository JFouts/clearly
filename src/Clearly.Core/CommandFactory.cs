// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Clearly.Core.Interfaces;

namespace Clearly.Core;

public class CommandFactory : ICommandFactory
{
    public virtual T Create<T>(Func<T> instantiationFunction)
        where T : Command
    {
        var command = instantiationFunction();
        return command;
    }
}
