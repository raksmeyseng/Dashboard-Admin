using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.NewDescription;

public interface INewDescriptionRepository : IRepository<NewDescription>
{

}

public class NewDescriptionRepository : Repository<NewDescription>, INewDescriptionRepository
{
	public NewDescriptionRepository(MyDbContext context) : base(context)
	{
	}

}