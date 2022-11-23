using MarkNet.Core.Entities.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace MarkNet.Infrastructure.SchemaDefinitions.Configs
{
    public class ConfigCollectionSchemaDefinition<TEntity>
        : IEntityTypeConfiguration<TEntity> where TEntity : class, ICollectionConfigEntity, new()
    {
        private readonly TEntity[] _entities;

        public ConfigCollectionSchemaDefinition(TEntity[] entities)
        {
            _entities = entities;
        }

        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(col => col.Id);
            builder.HasIndex(col => col.Number).IsUnique(true);

            for (var i = 0; i < _entities.Count(); i++)
            {
                var entity = _entities.ElementAt(i);
                entity.Id = i + 1;
            }

            builder.HasData(_entities);
        }
    }
}
