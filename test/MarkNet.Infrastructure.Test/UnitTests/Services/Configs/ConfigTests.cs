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

            var seq1Response = await service.GetAsync();
            Assert.True(seq1Response.IsSuccess);

            var seq1Model = seq1Response.Model;
            Assert.Equal(1, seq1Model.Value);

            var changeModel = new FakeConfig() { Value = 2 };
            await service.SetAsync(changeModel);

            var seq2Response = await service.GetAsync();
            Assert.True(seq2Response.IsSuccess);

            var seq2Model = seq2Response.Model;
            Assert.Equal(changeModel.Value, seq2Model.Value);
        }
    }
}
