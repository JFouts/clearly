using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Questionable.Questions.Test.Unit
{
    public class TestBase : IDisposable
    {
        private Exception _latestException;

        public void Dispose()
        {
            RethrowLastestException();
        }

        protected void ThenItProducesAn<T>() where T : Exception
        {
            Assert.ThrowsAny<T>((Action)RethrowLastestException);
        }

        protected static void AssertContains<T1, T2>(IEnumerable<T1> collection, Func<T2, bool> exp) where T2 : T1
        {
            Assert.Contains(collection, x => x.GetType() == typeof(T2) && exp((T2)x));
        }

        protected async Task ExceptionWrappedAsync(Func<Task> act)
        {
            try
            {
                await act();
            }
            catch (Exception e)
            {
                _latestException = e;
            }
        }

        private void RethrowLastestException()
        {
            if (_latestException == null)
                return;

            var e = _latestException;
            _latestException = null;
            throw e;
        }
    }
}