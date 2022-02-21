using System;
using DomainModeling.EventRepository.EventStore.NamingConvention;
using Xunit;

namespace DomainModeling.EventRepository.Test.Unit.EventStore.NamingConventions
{
    public class CustomEventNamingTests
    {
        private readonly CustomEventNamingConvention _convention = new CustomEventNamingConvention();

        [Fact]
        public void ItReturnsEventNameFromMapping()
        {
            const string expected = "CustomObjectEventType";
            _convention.AddMap(typeof(object), expected);

            var eventName = _convention.GetEventName(typeof(object));

            Assert.Equal(expected, eventName);
        }

        [Fact]
        public void ItThrowsUnmappedTypeExceptionWhenMappingFromAnUnmappedType()
        {
            void Act() => _convention.GetEventName(typeof(object));

            Assert.Throws<UnmappedTypeException>((Action)Act);
        }

        [Fact]
        public void ItReturnsEventNameFromMappingGeneric()
        {
            const string expected = "CustomObjectEventType";
            _convention.AddMap(typeof(object), expected);

            var eventName = _convention.GetEventName<object>();

            Assert.Equal(expected, eventName);
        }

        [Fact]
        public void ItThrowsUnmappedTypeExceptionWhenMappingFromAnUnmappedTypeGeneric()
        {
            void Act() => _convention.GetEventName<object>();

            Assert.Throws<UnmappedTypeException>((Action)Act);
        }

        [Fact]
        public void ItReturnsEventTypeFromMapping()
        {
            var expected = typeof(object);
            _convention.AddMap(expected, "CustomObjectEventType");

            var eventType = _convention.GetEventType("CustomObjectEventType");

            Assert.Equal(expected, eventType);
        }

        [Fact]
        public void ItThrowsUnmappedTypeExceptionWhenMappingFromAnUnmappedName()
        {
            void Act() => _convention.GetEventType("CustomObjectEventType");

            Assert.Throws<UnmappedTypeException>((Action)Act);
        }
    }
}
