
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Project;

public class Project : AuditableEntity
{
	public string ProjectType { get; set; } = null!;
	public string ProjectName { get; set; } = null!;
	public string Client { get; set; } = null!;
	public string Size { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string Location { get; set; } = null!;
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		builder.Property(m => m.ProjectType)
			.HasMaxLength(50);
		builder.Property(m => m.ProjectName)
			.HasMaxLength(100);
		builder.Property(m => m.Location)
			.HasMaxLength(100);
		builder.Property(m => m.Client)
			.HasMaxLength(100);
		builder.Property(m => m.Size)
			.HasMaxLength(50);
		builder.Property(m => m.Status)
			.HasMaxLength(100);

	}
}
