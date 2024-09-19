using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.ImageShow;

public interface IImageShowRepository : IRepository<ImageShow>
{

}

public class ImageShowRepository : Repository<ImageShow>, IImageShowRepository
{
	public ImageShowRepository(MyDbContext context) : base(context)
	{
	}

}