
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Recommend;

public class Recommend : AuditableEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Description { get; set; } = null!;

}

public class RecommendConfig : IEntityTypeConfiguration<Recommend>
{
    public void Configure(EntityTypeBuilder<Recommend> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(50);
        builder.Property(p => p.Email)
            .HasMaxLength(100);
    }
}
