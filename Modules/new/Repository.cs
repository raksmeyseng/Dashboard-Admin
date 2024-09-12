using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.New;

public interface INewRepository : IRepository<New>
{

}

public class NewRepository : Repository<New>, INewRepository
{
	public NewRepository(MyDbContext context) : base(context)
	{
	}

}