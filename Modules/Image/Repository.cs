using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.Image;

public interface IImageRepository : IRepository<Image>
{

}

public class ImageRepository : Repository<Image>, IImageRepository
{
	public ImageRepository(MyDbContext context) : base(context)
	{
	}

}