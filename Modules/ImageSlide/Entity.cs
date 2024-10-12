
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.ImageSlide;

public class ImageSlide : AuditableEntity
{
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Guid ProjectId { get; set; }
	public Project.Project Project { get; set; } = null!;
}

public class ImageSlideConfig : IEntityTypeConfiguration<ImageSlide>
{
	public void Configure(EntityTypeBuilder<ImageSlide> builder)
	{
		builder.HasOne(o => o.Project)
				.WithMany(m => m.ImageSlides)
				.HasForeignKey(k => k.ProjectId);
	}
}
