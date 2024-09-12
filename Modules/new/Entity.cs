
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.New;

public class New : AuditableEntity
{
	public string ImagePath { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public DateTime? Time { get; set; }
}

public class NewConfig : IEntityTypeConfiguration<New>
{
	public void Configure(EntityTypeBuilder<New> builder)
	{
		
	}
}
