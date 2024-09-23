namespace ArchtistStudio.Core;

internal static class CorsExtension
{
    public static void AddMyCors(this IServiceCollection service)
    {
        service.AddCors(options =>
     {
         options.AddPolicy("AllowFlutterApp",
             builder =>
             {
                 builder.WithOrigins("https://architecture.ts-enc.com")  
                        .AllowAnyHeader()
                        .AllowAnyMethod();
             });
     });

    }
}