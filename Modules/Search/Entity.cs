
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Search;

public class Search : AuditableEntity
{
        public string? Name { get; set; } 
}

public class SearchConfig : IEntityTypeConfiguration<Search>
{
        public void Configure(EntityTypeBuilder<Search> builder)
        {
        }
}
