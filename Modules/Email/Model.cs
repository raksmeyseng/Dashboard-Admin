
namespace ArchtistStudio.Modules.Email;

public class ListEmailResponse
{
	public Guid Id { get; set; }
	public string Address { get; set; } = null!;
}
public class DetailEmailResponse
{
	public Guid Id { get; set; }
	public string Address { get; set; } = null!;
}

public class InsertEmailRequest
{
	public string Address { get; set; } = null!;
}


public class UpdateEmailRequest
{
	public string? Address { get; set; }

}
