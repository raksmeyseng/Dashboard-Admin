using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.SendEmail;

public interface ISendEmailRepository : IRepository<SendEmail>
{
}

public class SendEmailRepository : Repository<SendEmail>, ISendEmailRepository
{
	public SendEmailRepository(MyDbContext context) : base(context)
	{
	}

}