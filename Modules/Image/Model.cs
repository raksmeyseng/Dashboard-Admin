﻿namespace ArchtistStudio.Modules.Image;

public class ListImageResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
	public Project.Project Project { get; set; } = null!;
}
public class DatailImageResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
	public List<ImageShow.DatailImageShowResponse> ImageShows { get; set; } = [];
}

public class InsertImageRequest
{
	public Guid ProjectId { get; set; }
	public IFormFile ImagePath { get; set; } = null!;
	public string Description { get; set; } = null!;
}

public class UpdateImageRequest
{
	public Guid ProjectId { get; set; }
	public IFormFile? ImagePath { get; set; }
	public string? Description { get; set; }

}
