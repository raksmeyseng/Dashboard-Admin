
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.History;

public class History : AuditableEntity
{
	public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

}

public class HistoryConfig : IEntityTypeConfiguration<History>
{
	public void Configure(EntityTypeBuilder<History> builder)
	{
		
	}
}
