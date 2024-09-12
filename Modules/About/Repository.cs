using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.About;

public interface IAboutRepository : IRepository<About>
{

}

public class AboutRepository : Repository<About>, IAboutRepository
{
	public AboutRepository(MyDbContext context) : base(context)
	{
	}

}