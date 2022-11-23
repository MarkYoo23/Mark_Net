using System;
using System.Threading;
using System.Threading.Tasks;

namespace MarkNet.Core.Engines
{
    public abstract class Engine
    {
        public virtual Task InitalizeAsync()
        {
            return Task.CompletedTask;
        }

        public async Task RunAsync(CancellationToken stoppingToken)
        {
            try
            {
                await InitalizeAsync();

                await StartAsync();

                while (stoppingToken.IsCancellationRequested == false)
                {
                    await RunOneCycleAsync();
                }
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                await StopAsync();
            }
        }

        public virtual Task StartAsync()
        {
            return Task.CompletedTask;
        }

        public abstract Task RunOneCycleAsync();


        public virtual Task StopAsync()
        {
            return Task.CompletedTask;
        }
    }
}
