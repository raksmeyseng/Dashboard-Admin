
namespace ArchtistStudio.Modules.About;

public class ListAboutResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Expert { get; set; } = null!;
	public string Construction { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string ChooseUs { get; set; } = null!;

}
public class DetailAboutResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Expert { get; set; } = null!;
	public string Construction { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string ChooseUs { get; set; } = null!;

}

public class InsertAboutRequest
{
	public IFormFile ImagePath { get; set; }  = null!;
	public string Expert { get; set; } = null!;
	public string Construction { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string ChooseUs { get; set; } = null!;

}


public class UpdateAboutRequest
{
	public IFormFile?  ImagePath { get; set; } 
	public string? Expert { get; set; }
	public string? Construction { get; set; }
	public string? Service { get; set; }
	public string? ChooseUs { get; set; }

}
