using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Architecture;

public interface IArchitectureRepository : IRepository<Architecture>
{
}

public class ArchitectureRepository : Repository<Architecture>, IArchitectureRepository
{
	public ArchitectureRepository(MyDbContext context) : base(context)
	{
	}

}