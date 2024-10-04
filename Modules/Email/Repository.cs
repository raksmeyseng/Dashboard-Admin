using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Email;

public interface IEmailRepository : IRepository<Email>
{
}

public class EmailRepository : Repository<Email>, IEmailRepository
{
	public EmailRepository(MyDbContext context) : base(context)
	{
	}

}