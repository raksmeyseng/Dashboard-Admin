
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.About;

public class About : AuditableEntity
{
       public string ImagePath { get; set; } = null!;
       public string Since { get; set; } = null!;
       public string We { get; set; } = null!;
       public string Version { get; set; } = null!;
       public string Service { get; set; } = null!;
       public string Process { get; set; } = null!;
}

public class AboutConfig : IEntityTypeConfiguration<About>
{
       public void Configure(EntityTypeBuilder<About> builder)
       {
       }
}
