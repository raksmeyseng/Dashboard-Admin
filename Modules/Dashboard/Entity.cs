
using ArchtistStudio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArchtistStudio.Modules.Dashboard;

public class Dashboard : AuditableEntity
{

}

public class DashboardConfig : IEntityTypeConfiguration<Dashboard>
{
       public void Configure(EntityTypeBuilder<Dashboard> builder)
       {

       }
}
