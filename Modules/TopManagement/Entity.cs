
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.TopManagement;

public class TopManagement : AuditableEntity
{
	public string Name { get; set; } = null!;
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;

}

public class TopManagementConfig : IEntityTypeConfiguration<TopManagement>
{
	public void Configure(EntityTypeBuilder<TopManagement> builder)
	{

	}
}
