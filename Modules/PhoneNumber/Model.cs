
namespace ArchtistStudio.Modules.PhoneNumber;

public class ListPhoneNumberResponse
{
	public Guid Id { get; set; }
	public string Phone { get; set; } = null!;
}
public class DetailPhoneNumberResponse
{
	public Guid Id { get; set; }
	public string Phone { get; set; } = null!;
}

public class InsertPhoneNumberRequest
{
	public string Phone { get; set; } = null!;
}


public class UpdatePhoneNumberRequest
{
	public string? Phone { get; set; }
}
