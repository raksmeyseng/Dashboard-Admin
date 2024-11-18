namespace ArchtistStudio.Core;

public static class MyEnvironment
{
	public static string GetName()
	{
		var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
		return env switch
		{
			"Production" => "prod",
			"Uat" => "uat",
			_ => "dev"
		};
	}

	public static string DbConnection => Environment.GetEnvironmentVariable("DB_CONNECTION") ??
										 "Server=157.245.146.248:5432;Database=databaseTSENC;User Id=userTSENC;Password=TSENCP@ssw0rd";

	public static string SpaceName => Environment.GetEnvironmentVariable("SPACE_NAME") ?? "ts-enc";
	public static string Region => Environment.GetEnvironmentVariable("REGION") ?? "sgp1";
	public static string Endpoint => Environment.GetEnvironmentVariable("END_POINT") ?? "https://ts-enc.sgp1.digitaloceanspaces.com";
	public static string AccessKey => Environment.GetEnvironmentVariable("ACCESS_KEY") ?? "DO00JMU67D2ZGENZH6Y6";
	public static string Secretkey => Environment.GetEnvironmentVariable("SECRET_KEY") ?? "3+XNkUayT15U2uYiRlNwuqvZ1WmE8d08Z++EneCGOAU";
}