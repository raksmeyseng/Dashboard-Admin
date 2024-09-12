
namespace ArchtistStudio.Modules.Social;

public class ListSocialResponse
{
    public Guid Id { get; set; }
   public ESocial Platform { get; set; } 
    public string DisplayText { get; set; } = null!;
    public string URL { get; set; } = null!;
}
public class DetailSocialResponse
{
     public Guid Id { get; set; }
	public string Platform { get; set; } = null!;
	public string DisplayText { get; set; } = null!;
	public string URL { get; set; } = null!;
}

public class InsertSocialRequest
{
  public string Platform { get; set; } = null!;
	public string DisplayText { get; set; } = null!;
	public string URL { get; set; } = null!;
}


public class UpdateSocialRequest
{
    public string? Platform { get; set; } 
    public string? DisplayText { get; set; } 
    public string? URL { get; set; } 
}
