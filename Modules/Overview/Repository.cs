using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Overview;

public interface IOverviewRepository : IRepository<Overview>
{

}

public class OverviewRepository : Repository<Overview>, IOverviewRepository
{
	public OverviewRepository(MyDbContext context) : base(context)
	{
	}

}