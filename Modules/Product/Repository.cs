using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Product;

public interface IProductRepository : IRepository<Product>
{
}

public class ProductRepository : Repository<Product>, IProductRepository
{
	public ProductRepository(MyDbContext context) : base(context)
	{
	}

}