
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Category;

public class Category : AuditableEntity
{
	public string Name { get; set; } = null!;
	public ICollection<Architecture.Architecture> Architectures { get; set; } = null!;
	public ICollection<Engineeing.Engineeing> Engineeings { get; set; } = null!;
	public ICollection<Product.Product> Products { get; set; } = null!;

}

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.Property(m => m.Name)
			   .HasMaxLength(50);
		builder.HasMany(m => m.Architectures)
				.WithOne(o => o.Category)
				.HasForeignKey(k => k.CategoryId);
		builder.HasMany(m => m.Engineeings)
				.WithOne(o => o.Category)
				.HasForeignKey(k => k.CategoryId);
		builder.HasMany(m => m.Products)
				.WithOne(o => o.Category)
				.HasForeignKey(k => k.CategoryId);
	}
}
