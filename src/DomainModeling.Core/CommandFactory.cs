using System;
using DomainModeling.Core.Interfaces;

namespace DomainModeling.Core
{
    public class CommandFactory : ICommandFactory
    {
        public virtual T Create<T>(Func<T> instantiationFunction) where T : Command
        {
            var command = instantiationFunction();
            return command;
        }
    }
}