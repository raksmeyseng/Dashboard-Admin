using ArchtistStudio.Core;

namespace ArchtistStudio.Modules.ImageSlide;

public interface IImageSlideRepository : IRepository<ImageSlide>
{

}

public class ImageSlideRepository : Repository<ImageSlide>, IImageSlideRepository
{
	public ImageSlideRepository(MyDbContext context) : base(context)
	{
	}

}