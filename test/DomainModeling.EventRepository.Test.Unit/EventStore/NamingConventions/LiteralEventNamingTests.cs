using System;
using DomainModeling.EventRepository.EventStore.NamingConention;
using Xunit;

namespace DomainModeling.EventRepository.Test.Unit.EventStore.NamingConventions
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

        [Fact]
        public void ItReturnsTrueWhenEventNameIsRegistered()
        {
            var result = _convention.IsKnownEventName("Object");

            Assert.True(result);
        }

        [Fact]
        public void ItReturnsFalseWhenEventNameIsUnregistered()
        {
            var result = _convention.IsKnownEventName("UnregisteredEventName");

            Assert.False(result);
        }
    }
}
