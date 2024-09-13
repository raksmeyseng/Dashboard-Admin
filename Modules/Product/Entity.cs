
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Product;

public class Product : AuditableEntity
{
        public Guid CategoryId { get; set; }
        public Category.Category Category { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project.Project Project { get; set; } = null!;
}

public class ArchitectureConfig : IEntityTypeConfiguration<Product>
{
        public void Configure(EntityTypeBuilder<Product> builder)
        {
                builder.HasOne(o => o.Category)
                        .WithMany(m => m.Products)
                        .HasForeignKey(k => k.CategoryId);
                builder.HasOne(o => o.Project)
                        .WithMany(m => m.Products)
                        .HasForeignKey(k => k.ProjectId);

        }
}
