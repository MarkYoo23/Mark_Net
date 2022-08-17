using MarkNet.Test.Repositories.Merges;
using Microsoft.Extensions.DependencyInjection;

namespace MarkNet.Test.Extensions
{
    internal static class RepositoryExtension
    {
        public static IServiceCollection AddTestRepository(this IServiceCollection services) 
        {
            services.AddScoped<ITestMergedRepository, TestMergedRepository>();

            return services;
        }
    }
}
