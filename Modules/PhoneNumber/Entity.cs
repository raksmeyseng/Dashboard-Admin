
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.PhoneNumber;

public class PhoneNumber : AuditableEntity
{
       public string Phone { get; set; } = null!;
}

public class PhoneNumberConfig : IEntityTypeConfiguration<PhoneNumber>
{
       public void Configure(EntityTypeBuilder<PhoneNumber> builder)
       {
              builder.Property(m => m.Phone)
                     .HasMaxLength(50);
       }
}
