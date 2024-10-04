
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Email;

public class Email : AuditableEntity
{
       public string Address { get; set; } = null!;
}

public class EmailConfig : IEntityTypeConfiguration<Email>
{
       public void Configure(EntityTypeBuilder<Email> builder)
       {
              builder.Property(m => m.Address)
                     .HasMaxLength(50);
       }
}
