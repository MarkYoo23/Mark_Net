using MarkNet.Core.Repositories.Commons;
using MarkNet.Test.Entities;
using MarkNet.Test.Repositories.Merges;
using MarkNet.Test.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarkNet.Test.UnitTests.Repositories
{
    public class MergedRepositoryTest
    {
        private readonly IHost _host;

        public MergedRepositoryTest()
        {
            _host = HostFactory.Create();
        }

        [Fact]
        public void MergedRepository_GetRepositoryShould()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var exception = Record.Exception(() =>
            {
                var mergedRepository = serviceProvider.GetRequiredService<ITestMergedRepository>();
                mergedRepository.GetRepository<IGenericRepository<FakeOneEntity>>();
                mergedRepository.GetRepository<IGenericRepository<FakeTwoEntity>>();
            });

            Assert.Null(exception);
        }
    }
}
