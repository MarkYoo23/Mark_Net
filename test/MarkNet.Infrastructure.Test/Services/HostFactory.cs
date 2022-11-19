using MarkNet.Core.Services.Cashings;
using MarkNet.Test.Contexts;
using MarkNet.Test.Models;
using MarkNet.Test.Repositories.Merges;
using MarkNet.Test.Services.Configs;
using MarkNet.Test.Services.SystemLogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarkNet.Test.Services
{
    internal static class HostFactory
    {
        public static IHost Create()
        {
            var inMemoryDatabaseRoot = new InMemoryDatabaseRoot();
            var databaseName = $"test_{Guid.NewGuid()}";

            var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddDbContext<TestContext>(options =>
                        {
                            options.UseInMemoryDatabase(databaseName, inMemoryDatabaseRoot);
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
