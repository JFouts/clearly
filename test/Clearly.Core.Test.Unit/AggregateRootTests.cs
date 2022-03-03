using System;
using Xunit;

namespace Clearly.Core.Unit
{
    public class AggregateRootTests
    {
        private readonly AggregateRoot _ar;

        public AggregateRootTests()
        {
            _ar = new MockAggregateRoot();
        }

        [Fact]
        public void ItHasId()
        {
            // Act
            var id = _ar.Id;

            // Assert
            Assert.IsType<Guid>(id);
        }
    }

    public record MockAggregateRoot : AggregateRoot { }
}
