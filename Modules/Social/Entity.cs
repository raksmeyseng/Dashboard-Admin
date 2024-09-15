
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Social;

public class Social : AuditableEntity
{
	public string Platform { get; set; }  = null!;
	public string URL { get; set; } = null!;
	public string DisplayText { get; set; } = null!;
}

public class SocialConfig : IEntityTypeConfiguration<Social>
{
	public void Configure(EntityTypeBuilder<Social> builder)
	{
		builder.Property(m => m.Platform)
				.HasMaxLength(100);
		builder.Property(m => m.URL)
				.HasMaxLength(100);
		builder.Property(m => m.DisplayText)
				.HasMaxLength(100);
	}
}
