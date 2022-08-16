using MarkNet.Core.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarkNet.Infrastructure.SchemaDefinitions.Commons
{
    public class EntitySchemaDefinition<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(col => col.Id);
        }
    }
}
