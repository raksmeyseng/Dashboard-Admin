
namespace ArchtistStudio.Modules.Auth;

public class InsertLoginRequest
{
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
}

public class InsertRegisterRequest
{
	public string Name { get; set; }= null!;
	public string Email { get; set; } = null!;
	public string Password { get; set; } = null!;
}