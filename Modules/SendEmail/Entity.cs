
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.SendEmail;

public class SendEmail : AuditableEntity
{
       public string Name { get; set; } = null!;
       public string Email { get; set; } = null!;
       public string Company { get; set; } = null!;
       public string Country { get; set; } = null!;
       public string Phone { get; set; } = null!;
       public string Expertise { get; set; } = null!;
       public string Message { get; set; } = null!;

}

public class SendEmailConfig : IEntityTypeConfiguration<SendEmail>
{
       public void Configure(EntityTypeBuilder<SendEmail> builder)
       {
       }
}
