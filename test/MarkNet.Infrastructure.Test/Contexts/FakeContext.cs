using MarkNet.Infrastructure.SchemaDefinitions.Commons;
using MarkNet.Infrastructure.SchemaDefinitions.Configs;
using MarkNet.Infrastructure.SchemaDefinitions.SystemLogs;
using MarkNet.Test.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarkNet.Test.Contexts
{
    public class TestContext : DbContext
    {
        public DbSet<FakeOneEntity> FakeOnes { get; set; } = null!;
        public DbSet<FakeTwoEntity> FakeTwos { get; set; } = null!;
        public DbSet<FakeCollectionConfigEntity> FakeCollectionConfigs { get; set; } = null!;
        public DbSet<FakeConfigEntity> FakeConfigs { get; set; } = null!;
        public DbSet<FakeSystemLogEntity> FakeSystemLogs { get; set; } = null!;
        public DbSet<FakeGenericEntity> FakeGenerics { get; set; } = null!;

        public TestContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntitySchemaDefinition<FakeOneEntity>());
            modelBuilder.ApplyConfiguration(new EntitySchemaDefinition<FakeTwoEntity>());

            modelBuilder.ApplyConfiguration(
                new ConfigCollectionSchemaDefinition<FakeCollectionConfigEntity>(
                    new FakeCollectionConfigEntity[] { }));

            modelBuilder.ApplyConfiguration(
                new ConfigSchemaDefinition<FakeConfigEntity>(
                    new FakeConfigEntity()
                    {
                        Id = 1,
                        Value = 1,
                    })
                );

            modelBuilder.ApplyConfiguration(
                new SystemLogSchemaDefinition<FakeSystemLogEntity>());

            modelBuilder.ApplyConfiguration(
                new EntitySchemaDefinition<FakeGenericEntity>());
        }
    }
}
