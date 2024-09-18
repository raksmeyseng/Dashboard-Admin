
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Architecture;

public class Architecture : AuditableEntity
{

        public Guid CategoryArchitectureId { get; set; }
        public CategoryArchitecture.CategoryArchitecture CategoryArchitecture { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project.Project Project { get; set; } = null!;
}

public class ArchitectureConfig : IEntityTypeConfiguration<Architecture>
{
        public void Configure(EntityTypeBuilder<Architecture> builder)
        {
                builder.HasOne(o => o.CategoryArchitecture)
                        .WithMany(m => m.Architectures)
                        .HasForeignKey(k => k.CategoryArchitectureId);
                builder.HasOne(o => o.Project)
                        .WithMany(m => m.Architectures)
                        .HasForeignKey(k => k.ProjectId);
               
        }
}
