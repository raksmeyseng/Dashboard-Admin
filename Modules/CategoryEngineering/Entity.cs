
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.CategoryEngineering;

public class CategoryEngineering : AuditableEntity
{
	public string Name { get; set; } = null!;
	public ICollection<Engineeing.Engineeing> Engineerings { get; set; } = null!;
}

public class CategoryEngineeringConfig : IEntityTypeConfiguration<CategoryEngineering>
{
	public void Configure(EntityTypeBuilder<CategoryEngineering> builder)
	{
		builder.Property(m => m.Name)
			   .HasMaxLength(50);
		builder.HasMany(m => m.Engineerings)
				.WithOne(o => o.CategoryEngineering)
				.HasForeignKey(k => k.CategoryEngineeringId);
	}
}
