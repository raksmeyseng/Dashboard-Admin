using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Engineeing;

public interface IEngineeingRepository : IRepository<Engineeing>
{
}

public class EngineeingRepository : Repository<Engineeing>, IEngineeingRepository
{
	public EngineeingRepository(MyDbContext context) : base(context)
	{
	}

}