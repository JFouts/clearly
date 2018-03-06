using System;
using System.ComponentModel.DataAnnotations;
using DomainModeling.Core.Exceptions;
using DomainModeling.Core.ValidationAnnotations;
using Xunit;

namespace DomainModeling.Core.Unit.ValidationAnnotations
{
    public class AlphanumericAttributeTests
    {
        private readonly AlphanumericAttribute _a;

        public AlphanumericAttributeTests()
        {
            _a = new AlphanumericAttribute();
        }

        [Fact]
        public void ItIsValidationAttribute()
        {
            // Assert
            Assert.IsAssignableFrom<ValidationAttribute>(_a);
        }

        [Fact]
        public void NullIsValid()
        {
            // Act
            var result = _a.IsValid(null);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void EmptyStringIsValid()
        {
            // Act
            var result = _a.IsValid(string.Empty);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void PeriodIsInvalid()
        {
            // Act
            var result = _a.IsValid(".");

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
        public void AlphanumericIsValid()
        {
            // Act
            var result = _a.IsValid("asdfas123412ASDFEBTE");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ListWithOneInvalidStringIsInvalid()
        {
            // Act
            var result = _a.IsValid(new[] { "asd", "123", "452", "4.5" });

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void EmptyListIsValid()
        {
            // Act
            var result = _a.IsValid(new string[0]);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ListWithAlphanumericStringsIsValid()
        {
            // Act
            var result = _a.IsValid(new[] { "asd", "123", "452", "4d5" });

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void InvalidObjectThrowException()
        {
            // Act
            void Act () { _a.IsValid(new object()); }

            // Assert
            Assert.Throws<InvalidTypeException>((Action)Act);
        }
    }
}
