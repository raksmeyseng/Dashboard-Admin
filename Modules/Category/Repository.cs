using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Category;

public interface ICategoryRepository : IRepository<Category>
{

}

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
	public CategoryRepository(MyDbContext context) : base(context)
	{
	}
}