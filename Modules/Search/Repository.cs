using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Search;

public interface ISearchRepository : IRepository<Search>
{
}

public class SearchRepository : Repository<Search>, ISearchRepository
{
	public SearchRepository(MyDbContext context) : base(context)
	{
	}

}