using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.CategoryArchitecture;

public interface ICategoryArchitectureRepository : IRepository<CategoryArchitecture>
{

}

public class CategoryArchitectureRepository : Repository<CategoryArchitecture>, ICategoryArchitectureRepository
{
	public CategoryArchitectureRepository(MyDbContext context) : base(context)
	{
	}
}