
using ArchtistStudio.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Auth;

public class Auth : AuditableEntity
{
       public string Name { get; set; } = null!;
       public string Email { get; internal set; } = null!;
       public string Password { get; set; } = null!;
}

public class AuthConfig : IEntityTypeConfiguration<Auth>
{
       public void Configure(EntityTypeBuilder<Auth> builder)
       {
              builder.Property(p => p.Name)
                     .HasMaxLength(50);

       }
}
