using System;

namespace DomainModeling.Core.Interfaces
{
    public interface ICommandFactory
    {
        T Create<T>(Func<T> instantiationFunction) where T : Command;
    }
}