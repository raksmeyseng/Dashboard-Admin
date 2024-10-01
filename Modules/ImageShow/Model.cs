
namespace ArchtistStudio.Modules.ImageShow;

public class ListImageShowResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}
public class DatailImageShowResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class InsertImageShowRequest
{
	public Guid ImageId { get; set; }
	public IFormFile ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class UpdateImageShowRequest
{
	public Guid ImageId { get; set; }
	public IFormFile? ImagePath { get; set; }
	public string? Description { get; set; }

}
