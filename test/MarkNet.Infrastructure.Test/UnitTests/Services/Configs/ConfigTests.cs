using MarkNet.Test.Services.Configs;
using MarkNet.Test.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

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
        public async Task FakeConfigService_InitalizeShould()
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
    }
}
