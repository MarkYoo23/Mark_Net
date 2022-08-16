using MarkNet.Test.Services.Configs;
using MarkNet.Test.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MarkNet.Test.Services.SystemLogs;
using MarkNet.Test.Entities;
using Newtonsoft.Json.Linq;

namespace MarkNet.Test.UnitTests.SystemLogs
{
    public class SystemLogTest
    {
        private readonly IHost _host;

        public SystemLogTest()
        {
            _host = HostFactory.Create();
        }

        [Fact]
        public async Task FakeSystemLogService_PutShould()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var exception = await Record.ExceptionAsync(async () =>
            {
                var service = serviceProvider.GetRequiredService<FakeSystemLogService>();
                await service.PutAsync(new FakeSystemLogEntity() 
                {
                    Value = 1,
                });
            });

            Assert.Null(exception);
        }
    }
}
