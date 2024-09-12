
namespace ArchtistStudio.Modules.TopManagement;

public class ListTopManagementResponse
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}
public class DetailTopManagementResponse
{
	public Guid Id { get; set; }
	public string Name { get; set; } = null!;
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class InsertTopManagementRequest
{
	public string Name { get; set; } = null!;
	public IFormFile ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}


public class UpdateTopManagementRequest
{
	public string? Name { get; set; }
	public IFormFile? ImagePath { get; set; }
	public string? Description { get; set; }
}
