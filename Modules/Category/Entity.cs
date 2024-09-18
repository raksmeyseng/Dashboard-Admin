
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Category;

public class Category : AuditableEntity
{
	public string Name { get; set; } = null!;
}

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.Property(m => m.Name)
			   .HasMaxLength(50);
	}
}
