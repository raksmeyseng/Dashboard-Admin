
namespace ArchtistStudio.Modules.About;

public class ListAboutResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public string Version { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string Process { get; set; } = null!;

}
public class DetailAboutResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public string Version { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string Process { get; set; } = null!;

}

public class InsertAboutRequest
{
	public IFormFile ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public string Version { get; set; } = null!;
	public string Service { get; set; } = null!;
	public string Process { get; set; } = null!;

}


public class UpdateAboutRequest
{
	public IFormFile? ImagePath { get; set; }
	public string? Since { get; set; }
	public string? We { get; set; }
	public string? Version { get; set; }
	public string? Service { get; set; }
	public string? Process { get; set; }

}
