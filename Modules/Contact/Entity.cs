
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Contact;

public class Contact : AuditableEntity
{
       public string ImagePath { get; set; } = null!;
       public string Name { get; set; } = null!;
       public string PhoneNumber { get; set; } = null!;
       public string Email { get; set; } = null!;
       public string Purpose { get; set; } = null!;
       public string Message { get; set; } = null!;
}

public class ContactConfig : IEntityTypeConfiguration<Contact>
{
       public void Configure(EntityTypeBuilder<Contact> builder)
       {
              builder.Property(m => m.Name)
                     .HasMaxLength(50);
              builder.Property(m => m.PhoneNumber)
                     .HasMaxLength(50);
              builder.Property(m => m.Email)
                     .HasMaxLength(50);
              builder.Property(m => m.Purpose)
                     .HasMaxLength(50);
              builder.Property(m => m.Message)
                    .HasMaxLength(255);
       }
}
