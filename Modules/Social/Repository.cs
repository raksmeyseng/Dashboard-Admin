using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Social;

public interface ISocialRepository : IRepository<Social>
{

}

public class SocialRepository : Repository<Social>, ISocialRepository
{
	public SocialRepository(MyDbContext context) : base(context)
	{
	}

}