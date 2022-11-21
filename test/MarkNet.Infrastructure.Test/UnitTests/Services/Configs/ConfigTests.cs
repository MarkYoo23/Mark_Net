using MarkNet.Test.Services.Configs;
using MarkNet.Test.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MarkNet.Test.Models;

namespace MarkNet.Test.UnitTests.Configs
{
    public class ConfigTests
    {
        private readonly IHost _host;

        public ConfigTests()
        {
            _host = HostFactory.Create();
        }

        [Fact]
        public async Task Initialize_Config_Success()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var exception = await Record.ExceptionAsync(async () =>
            {
                var service = serviceProvider.GetRequiredService<FakeConfigService>();
                await service.InitializeAsync();
            });

            Assert.Null(exception);
        }

        [Fact]
        public async Task SetAndGet_Config_Success()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var service = serviceProvider.GetRequiredService<FakeConfigService>();

            await service.InitializeAsync();

            var seq1Model = await service.GetAsync();
            Assert.Equal(1, seq1Model.Value);

            var changeModel = new FakeConfig() { Value = 2 };
            await service.SetAsync(changeModel);

            var seq2Model = await service.GetAsync();
            Assert.Equal(changeModel.Value, seq2Model.Value);
        }

        [Fact]
        public async Task SetWithGetMany_Service_MeetAsyncPerformanceCriteria()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var service = serviceProvider.GetRequiredService<FakeConfigService>();
                await service.InitializeAsync();
            }
            
            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(new Task(async () =>
                {
                    var scope = _host.Services.CreateScope();
                    var serviceProvider = scope.ServiceProvider;
                    var service = serviceProvider.GetRequiredService<FakeConfigService>();

                    var value = new FakeConfig()
                    {
                        Value = i
                    };

                    var delay = Random.Shared.Next(0, 10);
                    await Task.Delay(delay);
                    await service.SetAsync(value);
                }));
            }

            for (int i = 0; i < 10000; i++)
            {
                var scope = _host.Services.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                var service = serviceProvider.GetRequiredService<FakeConfigService>();

                tasks.Add(new Task(async () =>
                {
                    var delay = Random.Shared.Next(0, 30);
                    await Task.Delay(delay);
                    await service.GetAsync();
                }));
            }

            var startTime = DateTime.Now;

            foreach (var task in tasks)
            {
                task.Start();
            }
            
            Task.WaitAll(tasks.ToArray());

            var endTime = DateTime.Now;

            var diff = endTime - startTime;

            Assert.True(diff.TotalSeconds < 1);
        }

        [Fact]
        public async Task SetWithGetMany_Service_MeetSyncPerformanceCriteria()
        {
            using (var scope = _host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var service = serviceProvider.GetRequiredService<FakeConfigService>();
                await service.InitializeAsync();
            }

            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(new Task(async () =>
                {
                    var scope = _host.Services.CreateScope();
                    var serviceProvider = scope.ServiceProvider;
                    var service = serviceProvider.GetRequiredService<FakeConfigService>();

                    var value = new FakeConfig()
                    {
                        Value = i
                    };

                    var delay = Random.Shared.Next(0, 10);
                    await Task.Delay(delay);
                    await service.SetAsync(value);
                }));
            }

            for (int i = 0; i < 10000; i++)
            {
                var scope = _host.Services.CreateScope();
                var serviceProvider = scope.ServiceProvider;
                var service = serviceProvider.GetRequiredService<FakeConfigService>();

                tasks.Add(new Task(async () =>
                {
                    var delay = Random.Shared.Next(0, 30);
                    await Task.Delay(delay);
                    service.Get();
                }));
            }

            var startTime = DateTime.Now;

            foreach (var task in tasks)
            {
                task.Start();
            }

            Task.WaitAll(tasks.ToArray());

            var endTime = DateTime.Now;

            var diff = endTime - startTime;

            Assert.True(diff.TotalSeconds < 1);
        }
    }
}
