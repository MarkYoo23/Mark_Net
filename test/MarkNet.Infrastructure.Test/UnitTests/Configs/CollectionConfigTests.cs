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
    }
}
