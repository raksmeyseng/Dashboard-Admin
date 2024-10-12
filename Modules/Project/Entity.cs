
using ArchtistStudio.Core;
using ArchtistStudio.Modules.Architecture;
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
	public ICollection<Image.Image> Images { get; set; } = null!;
	public ICollection<ImageSlide.ImageSlide> ImageSlides { get; set; } = null!;
	public ICollection<Architecture.Architecture> Architectures { get; set; } = null!;
	public ICollection<Engineeing.Engineeing> Engineerings { get; set; } = null!;
	public ICollection<Product.Product> Products { get; set; } = null!;

}

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
	public void Configure(EntityTypeBuilder<Project> builder)
	{
		builder.HasMany(m => m.Images)
				.WithOne(o => o.Project);
		builder.HasMany(m => m.Architectures)
				.WithOne(o => o.Project)
				.HasForeignKey(k => k.CategoryArchitectureId);
		builder.HasMany(m => m.Engineerings)
	  			.WithOne(o => o.Project)
	  			.HasForeignKey(k => k.CategoryEngineeringId);
		builder.HasMany(m => m.Products)
			   .WithOne(o => o.Project)
			   .HasForeignKey(k => k.CategoryProductId);
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
