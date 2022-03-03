using Xunit;

namespace Clearly.Core.Unit
{
    public class CommandTests
    {
        [Fact]
        public void ItExists()
        {
            Command c = new MockCommand();
        }

        private class MockCommand : Command { }
    }
}
