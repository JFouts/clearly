using Xunit;

namespace DomainModeling.Core.Unit.Core
{
    public class CommandTests
    {
        [Fact]
        public void ItExists()
        {
            Command c = new DummyCommand();
        }

        private class DummyCommand : Command { }
    }
}
