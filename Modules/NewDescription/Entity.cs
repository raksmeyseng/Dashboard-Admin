
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.NewDescription;

public class NewDescription : AuditableEntity
{
	public string Description { get; set; } = null!;
}

public class NewDescriptionConfig : IEntityTypeConfiguration<NewDescription>
{
	public void Configure(EntityTypeBuilder<NewDescription> builder)
	{
	}
}
