namespace ArchtistStudio.Modules.ImageSlide;

public class ListImageSlideResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Project.Project Project { get; set; } = null!;
}
public class DatailImageSlideResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class InsertImageSlideRequest
{
	public Guid ProjectId { get; set; }
	public IFormFile ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class UpdateImageSlideRequest
{
	public Guid ProjectId { get; set; }
	public IFormFile? ImagePath { get; set; }
	public string? Description { get; set; }

}
public class PaginationListImageResponse
{
    public List<ListImageSlideResponse> Items { get; set; } = null!;
    public int PageNumber { get; set; } 
    public int PageSize { get; set; } 
    public int TotalCount { get; set; } 
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}