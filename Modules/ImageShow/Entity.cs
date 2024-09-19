
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.ImageShow;

public class ImageShow : AuditableEntity
{
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Guid ProjectId { get; set; }
	public Project.Project Project { get; set; } = null!;
}

public class ImageShowConfig : IEntityTypeConfiguration<ImageShow>
{
	public void Configure(EntityTypeBuilder<ImageShow> builder)
	{
		builder.HasOne(o => o.Project)
					 .WithMany(m => m.ImageShows)
					 .HasForeignKey(k => k.ProjectId);
	}
}
