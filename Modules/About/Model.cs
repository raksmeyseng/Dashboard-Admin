
namespace ArchtistStudio.Modules.About;

public class ListAboutResponse
{
	public Guid Id { get; set; }
	  public string ImagePath { get; set; } = null!;
       public string Since { get; set; } = null!;
       public string We { get; set; } = null!;
       public string ImagePathWe { get; set; } = null!;
       public string Vision { get; set; } = null!;
       public string ImagePathVision { get; set; } = null!;
       public string Service { get; set; } = null!;
       public string ImagePathService { get; set; } = null!;
       public string Process { get; set; } = null!;
       public string ImagePathProcess { get; set; } = null!;
       public string Planning { get; set; } = null!;
       public string ImagePathPlanning { get; set; } = null!;

}
public class DetailAboutResponse
{
	public Guid Id { get; set; }
	  public string ImagePath { get; set; } = null!;
       public string Since { get; set; } = null!;
       public string We { get; set; } = null!;
       public string ImagePathWe { get; set; } = null!;
       public string Vision { get; set; } = null!;
       public string ImagePathVision { get; set; } = null!;
       public string Service { get; set; } = null!;
       public string ImagePathService { get; set; } = null!;
       public string Process { get; set; } = null!;
       public string ImagePathProcess { get; set; } = null!;
       public string Planning { get; set; } = null!;
       public string ImagePathPlanning { get; set; } = null!;


}

public class InsertAboutRequest
{
	public IFormFile ImagePath { get; set; } = null!;
	public string Since { get; set; } = null!;
	public string We { get; set; } = null!;
	public IFormFile ImagePathWe { get; set; } = null!;
	public string Vision { get; set; } = null!;
	public IFormFile ImagePathVision { get; set; } = null!;
	public string Service { get; set; } = null!;
	public IFormFile ImagePathService { get; set; } = null!;
	public string Process { get; set; } = null!;
	public IFormFile ImagePathProcess { get; set; } = null!;

	public string Planning { get; set; } = null!;
	public IFormFile ImagePathPlanning { get; set; } = null!;
}


public class UpdateAboutRequest
{
	public IFormFile? ImagePath { get; set; }
	public string? Since { get; set; }
	public string? We { get; set; }
	public IFormFile? ImagePathWe { get; set; }
	public string? Vision { get; set; }
	public IFormFile? ImagePathVision { get; set; }
	public string? Service { get; set; }
	public IFormFile? ImagePathService { get; set; }
	public string? Process { get; set; }
	public IFormFile? ImagePathProcess { get; set; }
	public string? Planning { get; set; }
	public IFormFile? ImagePathPlanning { get; set; }


}
