using System.Threading;
using System.Threading.Tasks;

namespace MarkNet.Core.Services.Commons
{
    public class AsyncLockManager
    {
        protected const int _millisecondsWriteTimeout = 1000;
        protected const int _millisecondsReadDelay = 1;

        protected readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        protected async Task WaitCanReadAsync()
        {
            while (!IsReadable())
            {
                await Task.Delay(_millisecondsReadDelay);
            }
        }

        protected bool IsReadable()
        {
            return _semaphoreSlim.CurrentCount > 0;
        }
    }
}
