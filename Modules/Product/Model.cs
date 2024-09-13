﻿
using ArchtistStudio.Modules.Project;
namespace ArchtistStudio.Modules.Product;

public class GetCategoryByProductResponse
{
	public Guid ProjectId { get; set; }
	public ListProjectResponse Project { get; set; } = null!;
	public bool Checked { get; set; }
}
public class ChangeProductRequest
{
	public Guid ProjectId { get; set; }
	public Guid CategoryId { get; set; }
	public bool Checked { get; set; }
}