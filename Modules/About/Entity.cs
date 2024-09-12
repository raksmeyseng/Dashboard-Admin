
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.About;

public class About : AuditableEntity
{
       public string ImagePath { get; set; } = null!;
       public string Expert { get; set; } = null!;
       public string Construction { get; set; } = null!;
       public string Service { get; set; } = null!;
       public string ChooseUs { get; set; } = null!;
}

public class AboutConfig : IEntityTypeConfiguration<About>
{
       public void Configure(EntityTypeBuilder<About> builder)
       {
       }
}
