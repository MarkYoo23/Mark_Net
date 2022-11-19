using MarkNet.Core.Repositories.Commons;
using MarkNet.Core.Services.Cashings;
using MarkNet.Test.Contexts;
using MarkNet.Test.Models;
using MarkNet.Test.Repositories.Merges;
using MarkNet.Test.Services.Configs;
using MarkNet.Test.Services.SystemLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;

namespace MarkNet.Test.Services
{
    internal static class HostFactory
    {
        public static IHost Create()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton((builder) =>
                    {
                        var databaseName = $"test_{Guid.NewGuid()}";
                        var options = new DbContextOptionsBuilder<TestContext>()
                            .UseInMemoryDatabase(databaseName)
                            .Options;

                        return new TestContext(options);
                    });

                    services.AddScoped<ITestMergedRepository, TestMergedRepository>();

                    services.AddSingleton<CollectionCashManager<FakeCollectionConfig>>();
                    services.AddScoped<FakeCollectionConfigService>();

                    services.AddSingleton<CashManager<FakeConfig>>(); 
                    services.AddScoped<FakeConfigService>();

                    services.AddScoped<FakeSystemLogService>();
                })
                .Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<TestContext>();
                context.Database.EnsureCreated();
            }

            return host;
        }
    }
}
