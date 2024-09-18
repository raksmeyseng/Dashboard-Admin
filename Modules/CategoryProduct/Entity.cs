
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.CategoryProduct;

public class CategoryProduct : AuditableEntity
{
	public string Name { get; set; } = null!;
	public ICollection<Product.Product> Products { get; set; } = null!;
}

public class CategoryProductConfig : IEntityTypeConfiguration<CategoryProduct>
{
	public void Configure(EntityTypeBuilder<CategoryProduct> builder)
	{
		builder.Property(m => m.Name)
			   .HasMaxLength(50);
		builder.HasMany(m => m.Products)
				.WithOne(o => o.CategoryProduct)
				.HasForeignKey(k => k.CategoryProductId);
	}
}
