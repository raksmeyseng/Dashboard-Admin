namespace ArchtistStudio.Core
{
    internal static class CorsExtension
    {
        public static void AddMyCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://architecture.ts-enc.com") // Replace with your specific allowed origin
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); // Allows credentials (cookies, authorization headers, etc.) if needed
                });
            });
        }
    }
}
