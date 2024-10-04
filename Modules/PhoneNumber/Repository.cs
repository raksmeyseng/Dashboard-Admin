using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.PhoneNumber;

public interface IPhoneNumberRepository : IRepository<PhoneNumber>
{
}

public class PhoneNumberRepository : Repository<PhoneNumber>, IPhoneNumberRepository
{
	public PhoneNumberRepository(MyDbContext context) : base(context)
	{
	}

}