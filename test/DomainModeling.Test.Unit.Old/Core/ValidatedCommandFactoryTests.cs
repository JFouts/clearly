using System;
using System.ComponentModel.DataAnnotations;
using DomainModeling.Core.Exceptions;
using Xunit;

namespace DomainModeling.Core.Unit.Core
{
    public class ValidatedCommandFactoryTests
    {
        private readonly ValidatedCommandFactory _factory;
        private readonly Command _commmand;
        private readonly Command _commandWithInvalidModelState;

        public ValidatedCommandFactoryTests()
        {
            _factory = new ValidatedCommandFactory();
            _commmand = new MockCommand();
            _commandWithInvalidModelState = GetInvalidCommand();
        }

        [Fact]
        // Given an instantiation method that returns a command
        // When a factory creates a new instance with the instantiation method
        // Then the created instance should be the same command
        public void ItRetrunsCommandFromInstantiationMethod()
        {
            Command InstantiationMethod() => _commmand;

            var createdInstance = _factory.Create(InstantiationMethod);

            Assert.Same(_commmand, createdInstance);
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
                Required = null;
            }

        }
    }
}
