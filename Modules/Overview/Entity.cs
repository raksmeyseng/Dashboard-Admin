
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Overview;

public class Overview : AuditableEntity
{
	public string ImagePath { get; set; } = null!;
    public string Description { get; set; } = null!;

}

public class OverviewConfig : IEntityTypeConfiguration<Overview>
{
	public void Configure(EntityTypeBuilder<Overview> builder)
	{
		
	}
}
