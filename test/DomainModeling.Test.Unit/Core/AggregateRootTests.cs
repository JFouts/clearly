using System;
using Xunit;

namespace DomainModeling.Core.Unit.Core
{
    public class AggregateRootTests
    {
        private readonly AggregateRoot _aggregateRoot;

        public AggregateRootTests()
        {
            _aggregateRoot = new DummyAggregateRoot();
        }

        [Fact]
        public void ItHasId()
        {
            Assert.IsType<Guid>(_aggregateRoot.Id);
        }
    }

    public class DummyAggregateRoot : AggregateRoot { }
}
