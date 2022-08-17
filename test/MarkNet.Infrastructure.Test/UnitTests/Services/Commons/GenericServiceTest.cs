using MarkNet.Test.Extensions;
using MarkNet.Test.Models;
using MarkNet.Test.Services.Commons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MarkNet.Test.UnitTests.Services.Commons
{
    public class GenericServiceTest
    {
        private readonly IHost _host;

        public GenericServiceTest()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTestDatabase();
                    services.AddTestRepository();
                    services.AddScoped<FakeGenericService>();
                })
                .Build();
        }

        [Fact]
        public async Task Add() 
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var service = serviceProvider.GetRequiredService<FakeGenericService>();

            var model = new FakeGenericModel()
            {
                Value = 1
            };

            var isSuccessAdd = await service.AddAsync(model);
            Assert.True(isSuccessAdd);
        }

        [Fact]
        public async Task Update()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var service = serviceProvider.GetRequiredService<FakeGenericService>();

            var model = new FakeGenericModel()
            {
                Value = 1
            };

            var isSuccessAdd = await service.AddAsync(model);
            Assert.True(isSuccessAdd);

            var entities = await service.GetAllAsync();
            var entity = entities.First();

            model.Value = 2;

            var isSuccessUpdate = await service.UpdateAsync(entity.Id, model);
            Assert.True(isSuccessUpdate);

            entities = await service.GetAllAsync();
            entity = entities.First();
            Assert.Equal(model.Value, entity.Value);
        }

        [Fact]
        public async Task Delete()
        {
            using var scope = _host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;

            var service = serviceProvider.GetRequiredService<FakeGenericService>();

            var model = new FakeGenericModel()
            {
                Value = 1
            };

            var isSuccessAdd = await service.AddAsync(model);
            Assert.True(isSuccessAdd);

            var entities = await service.GetAllAsync();
            var entity = entities.First();

            var isSuccessUpdate = await service.RemoveAsync(entity.Id);
            Assert.True(isSuccessUpdate);

            entities = await service.GetAllAsync();
            Assert.Empty(entities);
        }
    }
}
