using System;
using Clearly.EventRepository.EventStore.NamingConvention;
using Xunit;

namespace Clearly.EventRepository.Test.Unit.EventStore.NamingConventions
{
    public class LiteralEventNamingTests
    {
        private readonly IEventNamingConvention _convention;

        public LiteralEventNamingTests()
        {
            var convention = new LiteralEventNamingConvention();

            convention.AddEventType<object>();
            _convention = convention;
        }

        [Fact]
        public void ItReturnsLiteralTypeNameWhenMapping()
        {
            var eventName = _convention.GetEventName(typeof(object));
            Assert.Equal("Object", eventName);
        }

        [Fact]
        public void ItReturnsLiteralTypeNameWhenMappingGeneric()
        {
            var eventName = _convention.GetEventName<object>();
            Assert.Equal("Object", eventName);
        }

        [Fact]
        public void ItThrowsWhenMappingToAnUnregisteredType()
        {
            void Act() => _convention.GetEventType("UnregisteredEventName");

            var ex = Assert.Throws<UnmappedEventNameException>((Action)Act);
            Assert.Equal("UnregisteredEventName", ex.EventName);
        }

        [Fact]
        public void ItReturnsMappedTypeWhenMappingToRegisteredType()
        {
            var eventType = _convention.GetEventType("Object");

            Assert.Equal(typeof(object), eventType);
        }
    }
}
