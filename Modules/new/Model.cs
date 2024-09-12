
namespace ArchtistStudio.Modules.New;

public class ListNewResponse
{
    public Guid Id { get; set; }
  	public string ImagePath { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Time { get; set; } = null!;
}
public class DetailNewResponse
{
  	public string ImagePath { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public string Time { get; set; } = null!;
}
 
public class InsertNewRequest
{
  	public IFormFile ImagePath { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public DateTime Time { get; set; }
}


public class UpdateNewRequest
{
  	public IFormFile ImagePath { get; set; } = null!;
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public DateTime Time { get; set; } 
}
