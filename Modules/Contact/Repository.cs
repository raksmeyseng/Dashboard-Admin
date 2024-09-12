using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Contact;

public interface IContactRepository : IRepository<Contact>
{
}

public class ContactRepository : Repository<Contact>, IContactRepository
{
	public ContactRepository(MyDbContext context) : base(context)
	{
	}

}