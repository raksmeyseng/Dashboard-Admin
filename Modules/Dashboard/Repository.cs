using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Dashboard;

public interface IDashboardRepository : IRepository<Dashboard>
{
}

public class DashboardRepository : Repository<Dashboard>, IDashboardRepository
{
	public DashboardRepository(MyDbContext context) : base(context)
	{
	}

}