using Xunit;

namespace DomainModeling.Core.Unit
{
    public class CommandFactoryTests
    {
        private readonly CommandFactory _factory;
        private readonly Command _commmand;

        public CommandFactoryTests()
        {
            _factory = new CommandFactory();
            _commmand = new MockCommand();
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

        private class MockCommand : Command { }
    }
}
