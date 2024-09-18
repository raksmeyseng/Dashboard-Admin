
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Product;

public class Product : AuditableEntity
{
        public Guid CategoryProductId { get; set; }
        public CategoryProduct.CategoryProduct CategoryProduct { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project.Project Project { get; set; } = null!;
}

public class ArchitectureConfig : IEntityTypeConfiguration<Product>
{
        public void Configure(EntityTypeBuilder<Product> builder)
        {
                builder.HasOne(o => o.CategoryProduct)
                        .WithMany(m => m.Products)
                        .HasForeignKey(k => k.CategoryProductId);
                builder.HasOne(o => o.Project)
                        .WithMany(m => m.Products)
                        .HasForeignKey(k => k.ProjectId);

        }
}
