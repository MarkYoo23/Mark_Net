using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace MarkNet.Core.Engines
{
    public abstract class EngineWorker : BackgroundService
    {
        private readonly Engine _engine;

        public EngineWorker(Engine engine)
        {
            _engine = engine;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (stoppingToken.IsCancellationRequested == false)
            {
                await _engine.RunAsync(stoppingToken);
                await Task.Delay(GetDelay());
            }
        }

        public virtual int GetDelay() => 2000;
    }
}
