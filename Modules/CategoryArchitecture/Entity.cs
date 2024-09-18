
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.CategoryArchitecture;

public class CategoryArchitecture : AuditableEntity
{
	public string Name { get; set; } = null!;
	public ICollection<Architecture.Architecture> Architectures { get; set; } = null!;
}

public class CategoryArchitectureConfig : IEntityTypeConfiguration<CategoryArchitecture>
{
	public void Configure(EntityTypeBuilder<CategoryArchitecture> builder)
	{
		builder.Property(m => m.Name)
			   .HasMaxLength(50);
		builder.HasMany(m => m.Architectures)
				.WithOne(o => o.CategoryArchitecture)
				.HasForeignKey(k => k.CategoryArchitectureId);
	}
}
