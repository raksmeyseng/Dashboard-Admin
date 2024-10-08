
namespace ArchtistStudio.Modules.About;

public class ListAboutResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public string ImagePathWe { get; set; } = null!;
	public string Version { get; set; } = null!;
	public string ImagePathVersion { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string ImagePathService { get; set; } = null!;
	public string Process { get; set; } = null!;
	public string ImagePathProcess { get; set; } = null!;


}
public class DetailAboutResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public string ImagePathWe { get; set; } = null!;
	public string Version { get; set; } = null!;
	public string ImagePathVersion { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string ImagePathService { get; set; } = null!;
	public string Process { get; set; } = null!;
	public string ImagePathProcess { get; set; } = null!;


}

public class InsertAboutRequest
{
	public IFormFile ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public IFormFile ImagePathWe { get; set; } = null!;
	public string Version { get; set; } = null!;
	public IFormFile ImagePathVersion { get; set; } = null!;
	public string Service { get; set; } = null!;
	public IFormFile ImagePathService { get; set; } = null!;
	public string Process { get; set; } = null!;
	public IFormFile ImagePathProcess { get; set; } = null!;
}


public class UpdateAboutRequest
{
	public IFormFile? ImagePath { get; set; } = null!;
	public string? Since { get; set; } = null!;
	public string? We { get; set; } = null!;
	public IFormFile? ImagePathWe { get; set; } = null!;
	public string? Version { get; set; } = null!;
	public IFormFile? ImagePathVersion { get; set; } = null!;
	public string? Service { get; set; } = null!;
	public IFormFile? ImagePathService { get; set; } = null!;
	public string? Process { get; set; } = null!;
	public IFormFile? ImagePathProcess { get; set; } = null!;


}
