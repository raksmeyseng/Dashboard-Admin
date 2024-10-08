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


	public static Uri GetServiceUrl(string path)
	{
		var domainName = Environment.GetEnvironmentVariable("SERVICE_DOMAIN") ?? "-psekvgalha-as.a.run.app";
		var env = GetName();
		return new Uri($"https://{path}-{env}{domainName}");
	}
	public static string Bucket => Environment.GetEnvironmentVariable("BUCKET") ?? "wedding-hub-dev";

	public static string Folder => Environment.GetEnvironmentVariable("FOLDER") ?? "Sourcing/";

	public static string CredentialJson => Environment.GetEnvironmentVariable("CREDENTIAL_JSON_KEY") ?? "{}";

	public static Guid ServiceId =>
		Guid.Parse(Environment.GetEnvironmentVariable("SERVICE_ID") ?? "1e0fe6e6-8941-4b52-bb2d-d792af08e058");

	public static string DbConnection => Environment.GetEnvironmentVariable("DB_CONNECTION") ??
										 "Server=47.129.175.170:5432;Database=ArchitectureDB;User Id=ArchitectureUser;Password=ArchitectureP@ssw0rd;";

	public static string JwtIssuer => Environment.GetEnvironmentVariable("JWT_ISSUER") ?? "https://dev.eventhub.one";

	public static string JwtAudience => Environment.GetEnvironmentVariable("JWT_AUDIENCE") ??
										"https://dev.eventhub.one";

	public static string JwtKey => Environment.GetEnvironmentVariable("JWT_KEY") ??
								   "ra8FXsc1Xv6FjN8cuxMDYcKeP4aQ4XRmKZyGnyhLRhuJ";

	public static string BucketName => Environment.GetEnvironmentVariable("BACKET_NAME") ?? "arhcitecture-image";
	public static string AWSKey => Environment.GetEnvironmentVariable("ACCESS_KEY") ?? "AKIAX2DZEVEVTPSTVY23";
	public static string AWSSecretkey => Environment.GetEnvironmentVariable("SECRET_KEY") ?? "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";
}