using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.CategoryProduct;

public interface ICategoryProductRepository : IRepository<CategoryProduct>
{

}

public class CategoryProductRepository : Repository<CategoryProduct>, ICategoryProductRepository
{
	public CategoryProductRepository(MyDbContext context) : base(context)
	{
	}
}