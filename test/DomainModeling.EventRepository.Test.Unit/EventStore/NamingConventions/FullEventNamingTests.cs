using DomainModeling.EventRepository.EventStore.NamingConention;
using Xunit;

namespace DomainModeling.EventRepository.Test.Unit.EventStore.NamingConventions
{
    public class FullEventNamingTests
    {
        private readonly IEventNamingConvention _convention = new FullEventNamingConvention();

        [Fact]
        public void ItReturnsFullyQualifiedNameWhenMapping()
        {
            var eventName = _convention.GetEventName(typeof(object));
            Assert.Equal("System.Object", eventName);
        }

        [Fact]
        public void ItReturnsFullyQualifiedNameWhenMappingGeneric()
        {
            var eventName = _convention.GetEventName<object>();
            Assert.Equal("System.Object", eventName);
        }

        [Fact]
        public void ItReturnsDotNetTypeFromEventName()
        {
            var type = _convention.GetEventType("System.Object");
            Assert.Equal(typeof(object), type);
        }

        [Fact]
        public void ItReturnsTrueWhenEventNameIsInTheAssembly()
        {
            var result = _convention.IsKnownEventName("System.Object");
            Assert.True(result);
        }

        [Fact]
        public void ItReturnsFalseWhenEventNameIsNotInTheAssembly()
        {
            var result = _convention.IsKnownEventName("System.ThisClassDoesNotExist");
            Assert.False(result);
        }
    }
}
