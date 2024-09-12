
namespace ArchtistStudio.Modules.Project;

public class ListProjectResponse
{
    public Guid Id { get; set; }
   public string ProjectType { get; set; } = null!;
	public string ProjectName { get; set; } = null!;
	public string Client { get; set; } = null!;
	public string Size { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string Location { get; set; } = null!;
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
    public bool? InActive { get; set; }
}
public class DetailProjectResponse
{
  public string ProjectType { get; set; } = null!;
	public string ProjectName { get; set; } = null!;
	public string Client { get; set; } = null!;
	public string Size { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string Location { get; set; } = null!;
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
    public bool? InActive { get; set; }
}

public class InsertProjectRequest
{
   public string ProjectType { get; set; } = null!;
	public string ProjectName { get; set; } = null!;
	public string Client { get; set; } = null!;
	public string Size { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string Location { get; set; } = null!;
	public IFormFile ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;   
     public bool? InActive { get; set; }
}


public class UpdateProjectRequest
{
  public string? ProjectType { get; set; }
	public string? ProjectName { get; set; }
	public string? Client { get; set; }
	public string? Size { get; set; }
	public string? Status { get; set; }
	public string? Location { get; set; }
	public IFormFile? ImagePath { get; set; }
	public string? Description { get; set; }
    public bool? InActive { get; set; }
}
