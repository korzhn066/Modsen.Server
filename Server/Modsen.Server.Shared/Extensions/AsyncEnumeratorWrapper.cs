using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modsen.Server.Shared.Extensions
{
    internal class AsyncEnumeratorWrapper<T>(IEnumerator<T> source) : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _source = source;

        public T Current => _source.Current;

        public ValueTask DisposeAsync()
        {
            return new ValueTask(Task.CompletedTask);
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_source.MoveNext());
        }
    }
}
