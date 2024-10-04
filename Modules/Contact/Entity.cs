
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Contact;

public class Contact : AuditableEntity
{
       public string ImagePath { get; set; } = null!;
       public string Location { get; set; } = null!;
}

public class ContactConfig : IEntityTypeConfiguration<Contact>
{
       public void Configure(EntityTypeBuilder<Contact> builder)
       {
       }
}
