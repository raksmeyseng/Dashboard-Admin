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
										 "Server=157.245.148.27:5432;Database=databaseTSENC;User Id=userTSENC;Password=TSENCP@ssw0rd";
	public static string SpaceName => Environment.GetEnvironmentVariable("SPACE_NAME") ?? "tsenc";
	public static string Region => Environment.GetEnvironmentVariable("REGION") ?? "sgp1";
	public static string Endpoint => Environment.GetEnvironmentVariable("END_POINT") ?? "https://sgp1.digitaloceanspaces.com";
	public static string AccessKey => Environment.GetEnvironmentVariable("ACCESS_KEY") ?? "DO00HHHYE99V9UUADCAD";
	public static string Secretkey => Environment.GetEnvironmentVariable("SECRET_KEY") ?? "rKx3YGNlzxENmblZlp9YzC/AKfyaThZsaOceQUlbZZg";
}