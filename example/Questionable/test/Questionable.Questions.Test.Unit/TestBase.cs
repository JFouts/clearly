using System.Threading.Tasks;
using Xunit;

namespace Questionable.Questions.Test.Unit;

public class TestBase : IDisposable
{
    private Exception? latestException;

    public void Dispose()
    {
        RethrowLastestException();
    }

    protected void ThenItProducesAn<T>()
        where T : Exception
    {
        Assert.ThrowsAny<T>((Action)RethrowLastestException);
    }

    protected static void AssertContains<T1, T2>(IEnumerable<T1> collection, Func<T2, bool> exp)
        where T2 : T1
    {
        Assert.Contains(collection, x => x is T2 t2 && exp(t2));
    }

    protected async Task ExceptionWrappedAsync(Func<Task> act)
    {
        try
        {
            await act();
        }
        catch (Exception e)
        {
            latestException = e;
        }
    }

    private void RethrowLastestException()
    {
        if (latestException == null)
            return;

        var e = latestException;
        latestException = null;

        throw e;
    }
}
