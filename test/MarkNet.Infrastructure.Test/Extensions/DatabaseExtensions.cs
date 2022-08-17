using MarkNet.Test.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarkNet.Test.Extensions
{
    internal static class DatabaseExtensions
    {
        internal static IServiceCollection AddTestDatabase(this IServiceCollection services) 
        {
            services.AddSingleton((builder) =>
            {
                var databaseName = $"test_{Guid.NewGuid()}";
                var options = new DbContextOptionsBuilder<TestContext>()
                    .UseInMemoryDatabase(databaseName)
                    .Options;

                return new TestContext(options);
            });

            return services;
        }

    }
}
