
namespace ArchtistStudio.Modules.Overview;

public class ListOverviewResponse
{
    public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
    public string Description { get; set; } = null!;

}
 
public class InsertOverviewRequest
{
	public IFormFile ImagePath { get; set; } = null!;
    public string Description { get; set; } = null!;

}

public class UpdateOverviewRequest
{
	public IFormFile? ImagePath { get; set; }
    public string? Description { get; set; } 

}
