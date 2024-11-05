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
										 "Server=13.250.4.12:5432;Database=ArchtustDB;User Id=ArchtustUser;Password=ArchtustP@ssw0rd;";

	public static string BucketName => Environment.GetEnvironmentVariable("BACKET_NAME") ?? "storage-architecture";
	public static string AWSKey => Environment.GetEnvironmentVariable("ACCESS_KEY") ?? "AKIASZ7NBXPCROJNDKGE";
	public static string AWSSecretkey => Environment.GetEnvironmentVariable("SECRET_KEY") ?? "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";
}