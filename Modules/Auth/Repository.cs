using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Auth;

public interface IAuthRepository : IRepository<Auth>
{
}

public class AuthRepository : Repository<Auth>, IAuthRepository
{
	public AuthRepository(MyDbContext context) : base(context)
	{
	}

}