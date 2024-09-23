namespace ArchtistStudio.Core;

internal static class CorsExtension
{
    public static void AddMyCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigins", policy =>
                policy.WithOrigins("https://architecture.ts-enc.com")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
            );
        });
    }
}
