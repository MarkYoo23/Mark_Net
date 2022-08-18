using MarkNet.Test.Entities;
using MarkNet.Test.Services;
using MarkNet.Test.Services.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarkNet.Test.UnitTests.Configs
{
    public class CollectionConfigTests
    {
        private readonly IHost _host;

        public CollectionConfigTests()
        {
            _host = HostFactory.Create();
        }

        [Fact]
        public async Task FakeCollectionConfigService_InitalizeShould()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
           
            var exception = await Record.ExceptionAsync(async () => 
            {
                var service = serviceProvider.GetRequiredService<FakeCollectionConfigService>();
                await service.InitializeAsync();
            });

            Assert.Null(exception);
        }

        [Fact]
        public async Task FakeCollectionConfigService_SetShould()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var exception = await Record.ExceptionAsync(async () =>
            {
                var service = serviceProvider.GetRequiredService<FakeCollectionConfigService>();

                var configs = new FakeCollectionConfigEntity[]
                {
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 1,
                        Value = 1,
                    },
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 2,
                        Value = 2,
                    },
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 3,
                        Value = 3,
                    },
                };

                await service.SetAsync(configs);
            });

            Assert.Null(exception);
        }

        [Fact]
        public async Task FakeCollectionConfigService_SetTwiceShould()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var service = serviceProvider.GetRequiredService<FakeCollectionConfigService>();

            var exception = await Record.ExceptionAsync(async () =>
            {
                var configs = new FakeCollectionConfigEntity[]
                {
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 1,
                        Value = 1,
                    },
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 2,
                        Value = 2,
                    },
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 3,
                        Value = 3,
                    },
                };

                await service.SetAsync(configs);
            });

            Assert.Null(exception);

            exception = await Record.ExceptionAsync(async () =>
            {
                var configs = new FakeCollectionConfigEntity[]
                {
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 1,
                        Value = 1,
                    },
                    new FakeCollectionConfigEntity()
                    {
                        Id = 0,
                        Number = 2,
                        Value = 2,
                    }
                };

                await service.SetAsync(configs);
            });

            Assert.Null(exception);
        }
    }
}
