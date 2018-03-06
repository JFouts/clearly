using System;
using DomainModeling.Core;
using DomainModeling.Core.Interfaces;
using DomainModeling.Core.Utilities.Interfaces;
using Moq;

namespace Questionable.Questions.Test.Unit.Commands
{
    public class CommandTestBase<THandler, TCommand> : TestBase where THandler : ICommandHandler<TCommand> where TCommand : Command
    {
        protected THandler Handler;
        protected readonly DateTime EventTime = new DateTime(2000, 5, 10, 4, 20, 34);
        protected readonly DateTime CurrentTime = new DateTime(2000, 6, 10, 4, 20, 34);

        protected IDate CreateMockDate()
        {
            var d = new Mock<IDate>();
            d.Setup(x => x.CurrentDateUtc()).Returns(CurrentTime);
            return d.Object;
        }
    }
}