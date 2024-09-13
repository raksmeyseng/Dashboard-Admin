
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Architecture;

public class Architecture : AuditableEntity
{

        public Guid CategoryId { get; set; }
        public Category.Category Category { get; set; } = null!;
        public Guid ProjectId { get; set; }
        public Project.Project Project { get; set; } = null!;
}

public class ArchitectureConfig : IEntityTypeConfiguration<Architecture>
{
        public void Configure(EntityTypeBuilder<Architecture> builder)
        {
                builder.HasOne(o => o.Category)
                        .WithMany(m => m.Architectures)
                        .HasForeignKey(k => k.CategoryId);
                builder.HasOne(o => o.Project)
                        .WithMany(m => m.Architectures)
                        .HasForeignKey(k => k.ProjectId);
               
        }
}
