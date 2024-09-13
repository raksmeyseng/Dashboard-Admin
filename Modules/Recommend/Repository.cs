using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Recommend;

public interface IRecommendRepository : IRepository<Recommend>
{

}

public class RecommendRepository : Repository<Recommend>, IRecommendRepository
{
	public RecommendRepository(MyDbContext context) : base(context)
	{
	}

}