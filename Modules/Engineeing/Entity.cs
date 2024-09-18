
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Engineeing;

public class Engineeing : AuditableEntity
{
        public Guid CategoryEngineeringId { get; set; }
        public CategoryEngineering.CategoryEngineering CategoryEngineering { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project.Project Project { get; set; } = null!;
}

public class ArchitectureConfig : IEntityTypeConfiguration<Engineeing>
{
        public void Configure(EntityTypeBuilder<Engineeing> builder)
        {
                builder.HasOne(o => o.CategoryEngineering)
                        .WithMany(m => m.Engineerings)
                        .HasForeignKey(k => k.CategoryEngineeringId);
                builder.HasOne(o => o.Project)
                        .WithMany(m => m.Engineerings)
                        .HasForeignKey(k => k.ProjectId);

        }
}
