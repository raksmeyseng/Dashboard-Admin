
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Image;

public class Image : AuditableEntity
{
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Guid ProjectId { get; set; }
	public Project.Project Project { get; set; } = null!;
}

public class ImageConfig : IEntityTypeConfiguration<Image>
{
	public void Configure(EntityTypeBuilder<Image> builder)
	{
		builder.HasOne(o => o.Project)
					 .WithMany(m => m.Images)
					 .HasForeignKey(k => k.ProjectId);
	}
}
