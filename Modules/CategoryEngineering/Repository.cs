using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.CategoryEngineering;

public interface ICategoryEngineeringRepository : IRepository<CategoryEngineering>
{

}

public class CategoryEngineeringRepository : Repository<CategoryEngineering>, ICategoryEngineeringRepository
{
	public CategoryEngineeringRepository(MyDbContext context) : base(context)
	{
	}
}