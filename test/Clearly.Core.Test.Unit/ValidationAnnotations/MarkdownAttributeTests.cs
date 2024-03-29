﻿using System;
using System.ComponentModel.DataAnnotations;
using Clearly.Core.Exceptions;
using Clearly.Core.ValidationAnnotations;
using Xunit;

namespace Clearly.Core.Unit.ValidationAnnotations
{
    public class MarkdownAttributeTests
    {
        private readonly MarkdownAttribute _a;

        public MarkdownAttributeTests()
        {
            _a = new MarkdownAttribute();
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
        public void InvalidObjectThrowException()
        {
            // Act
            void Act() { _a.IsValid(new object()); }

            // Assert
            Assert.Throws<InvalidTypeException>((Action)Act);
        }
    }
}
