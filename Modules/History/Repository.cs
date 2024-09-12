using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.History;

public interface IHistoryRepository : IRepository<History>
{

}

public class HistoryRepository : Repository<History>, IHistoryRepository
{
	public HistoryRepository(MyDbContext context) : base(context)
	{
	}

}