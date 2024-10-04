
namespace ArchtistStudio.Modules.Contact;

public class ListContactResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Location { get; set; } = null!;
}
public class DetailContactResponse
{
	public Guid Id { get; set; }
	public string ImagePath { get; set; } = null!;
	public string Location { get; set; } = null!;
}

public class InsertContactRequest
{
	public IFormFile ImagePath { get; set; } = null!;
	public string Location { get; set; } = null!;
}

public class UpdateContactRequest
{
	public IFormFile? ImagePath { get; set; }
	public string? Location { get; set; } 
}
