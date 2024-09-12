
namespace ArchtistStudio.Modules.Contact;

public class ListContactResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Purpose { get; set; } = null!;
	public string Message { get; set; } = null!;
}
public class DetailContactResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Purpose { get; set; } = null!;
	public string Message { get; set; } = null!;
}

public class InsertContactRequest
{
	public IFormFile? ImagePath { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string PhoneNumber { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Purpose { get; set; } = null!;
	public string Message { get; set; } = null!;
}


public class UpdateContactRequest
{
	public IFormFile? ImagePath { get; set; }
	public string? Name { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Email { get; set; }
	public string? Purpose { get; set; }

	public string? Message { get; set; }
}
