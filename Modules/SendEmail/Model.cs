
namespace ArchtistStudio.Modules.SendEmail;

public class InsertSendEmailRequest
{
	public string Name { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string Company { get; set; } = null!;
	public string Country { get; set; } = null!;
	public string Phone { get; set; } = null!;
	public string Expertise { get; set; } = null!;
	public string Message { get; set; } = null!;
}

public static class MyAccount
{
    public static string OwnerGmail { get; } = "defurteam@gmail.com";  
    public static string OwnerPassword { get; } = "nccq qxue ewqx ipvg";    
}
