namespace ArchtistStudio.Modules.Project;

public class ViewListProjectResponse
{
	public Guid Id { get; set; }
	public string ProjectType { get; set; } = null!;
	public string ProjectName { get; set; } = null!;
	public string Client { get; set; } = null!;
	public string Size { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string Location { get; set; } = null!;
	public List<Image.DatailImageResponse> Images { get; set; } = [];
	public int ImageCount { get; set; }
	public int ImageShowCount { get; set; }
	public bool? InActive { get; set; }
}

public class ListProjectResponse
{
	public Guid Id { get; set; }
	public string ProjectType { get; set; } = null!;
	public string ProjectName { get; set; } = null!;
	public string Client { get; set; } = null!;
	public string Size { get; set; } = null!;
	public string Status { get; set; } = null!;
	public string Location { get; set; } = null!;
	public List<Image.DatailImageResponse> Images { get; set; } = [];
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
	public bool? InActive { get; set; }
}
