﻿using System.ComponentModel.DataAnnotations;
using Clearly.Core.Exceptions;
using Xunit;

namespace Clearly.Core.Unit
{
    public class ValidatedCommandFactoryTests
    {
        private readonly ValidatedCommandFactory _factory;
        private readonly Command _command;
        private readonly Command _commandWithInvalidModelState;

        public ValidatedCommandFactoryTests()
        {
            _factory = new ValidatedCommandFactory();
            _command = new MockCommand();
            _commandWithInvalidModelState = GetInvalidCommand();
        }

        [Fact]
        // Given an instantiation method that returns a command
        // When a factory creates a new instance with the instantiation method
        // Then the created instance should be the same command
        public void ItReturnsCommandFromInstantiationMethod()
        {
            Command InstantiationMethod() => _command;

            var createdInstance = _factory.Create(InstantiationMethod);

            Assert.Same(_command, createdInstance);
        }

        [Fact]
        // Given an instantiation method that returns a command with an invalid model state
        // When a factory creates a new instance with the instantiation method
        // Then an Invalid Model State error should be thrown
        public void ItThrowsExceptionWhenModelStateIsInvalid()
        {
            Command InstantiationMethod() => _commandWithInvalidModelState;

            void Act() => _factory.Create(InstantiationMethod);

            Assert.Throws<InvalidModelStateException>((Action) Act);
        }

        private static Command GetInvalidCommand()
        {
            var c = new MockCommand();
            c.SetInvalidModelState();

            return c;
        }

        private class MockCommand : Command
        {
            [Required] public object Required { get; set; } = new { };

            public void SetInvalidModelState()
            {
                Required = null!;
            }
        }
    }
}
