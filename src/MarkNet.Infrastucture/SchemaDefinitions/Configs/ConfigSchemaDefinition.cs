using MarkNet.Core.Entities.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkNet.Infrastructure.SchemaDefinitions.Configs
{
    public class ConfigSchemaDefinition<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IConfigEntity, new()
    {
        private readonly TEntity _entity;

        public ConfigSchemaDefinition()
        {
            _entity = new TEntity()
            {
                Id = 1
            };
        }

        public ConfigSchemaDefinition(TEntity entity)
        {
            _entity = entity;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(col => col.Id);
            builder.HasData(_entity);
        }
    }
}
