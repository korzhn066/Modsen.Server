using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Authentication.Test.Extensions
{
    internal class AsyncEnumeratorWrapper<T>(IEnumerator<T> source) : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> Source = source;

        public T Current => Source.Current;

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(Source.MoveNext());
        }
    }
}
