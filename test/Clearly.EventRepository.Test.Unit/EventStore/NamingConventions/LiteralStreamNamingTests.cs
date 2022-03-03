using Clearly.EventRepository.EventStore.NamingConvention;
using Xunit;

namespace Clearly.EventRepository.Test.Unit.EventStore.NamingConventions
{
    public class LiteralStreamNamingTests
    {
        private readonly IStreamNamingConvention _convention = new LiteralStreamNamingConvention();

        [Fact]
        public void ItReturnsTheLiteralTypeNameWhenMapping()
        {
            var streamName = _convention.GetStreamName(typeof(object));
            Assert.Equal("Object", streamName);
        }

        [Fact]
        public void ItReturnsTheLiteralTypeNameWhenMappingGeneric()
        {
            var streamName = _convention.GetStreamName<object>();
            Assert.Equal("Object", streamName);
        }
    }
}
