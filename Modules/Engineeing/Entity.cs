
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Engineeing;

public class Engineeing : AuditableEntity
{
        public Guid CategoryId { get; set; }
        public Category.Category Category { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project.Project Project { get; set; } = null!;
}

public class ArchitectureConfig : IEntityTypeConfiguration<Engineeing>
{
        public void Configure(EntityTypeBuilder<Engineeing> builder)
        {
                builder.HasOne(o => o.Category)
                        .WithMany(m => m.Engineeings)
                        .HasForeignKey(k => k.CategoryId);
                builder.HasOne(o => o.Project)
                        .WithMany(m => m.Engineeings)
                        .HasForeignKey(k => k.ProjectId);

        }
}
