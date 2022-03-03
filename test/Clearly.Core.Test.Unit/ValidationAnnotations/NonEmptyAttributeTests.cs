using System;
using System.ComponentModel.DataAnnotations;
using Clearly.Core.Exceptions;
using Clearly.Core.ValidationAnnotations;
using Xunit;

namespace Clearly.Core.Unit.ValidationAnnotations
{
    public class NonEmptyAttributeTests
    {
        private readonly NonEmptyAttribute _a;

        public NonEmptyAttributeTests()
        {
            _a = new NonEmptyAttribute();
        }

        [Fact]
        public void ItIsValidationAttribute()
        {
            // Assert
            Assert.IsAssignableFrom<ValidationAttribute>(_a);
        }

        [Fact]
        public void NullIsInvalid()
        {
            // Act
            var result = _a.IsValid(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmptyStringIsInvalid()
        {
            // Act
            var result = _a.IsValid(string.Empty);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void WhitespaceIsInvalid()
        {
            // Act
            var result = _a.IsValid("   ");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void TextIsValid()
        {
            // Act
            var result = _a.IsValid("asdkfnjkn erfnakj 21");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EmptyListIsInvalid()
        {
            // Act
            var result = _a.IsValid(new object[0]);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void PopulatedListIsValid()
        {
            // Act
            var result = _a.IsValid(new object[1]);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InvalidObjectThrowException()
        {
            // Act
            void Act() { _a.IsValid(new object()); }

            // Assert
            Assert.Throws<InvalidTypeException>((Action)Act);
        }
    }
}
