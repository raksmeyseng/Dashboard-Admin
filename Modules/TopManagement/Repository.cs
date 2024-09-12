using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.TopManagement;

public interface ITopManagementRepository : IRepository<TopManagement>
{

}

public class TopManagementRepository : Repository<TopManagement>, ITopManagementRepository
{
	public TopManagementRepository(MyDbContext context) : base(context)
	{
	}

}