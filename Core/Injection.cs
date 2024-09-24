using System.Reflection;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace ArchtistStudio.Core;

public static class DependencyInjection
{
    public static void AddMiddleWare(this IApplicationBuilder app)
    {
        app.UseCors();
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();
        app.AddError();
        var env = MyEnvironment.GetName();
        if (env == "prod") return;
        app.MySwagger();
    }
    public static void AddInjection(this IServiceCollection service)
    {
        _assemblyInjection(service, "Service");
        _assemblyInjection(service, "SingletonService");
        _assemblyInjection(service, "Repository");
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
        service.AddMyCors();
        service.AddMySwagger();
        service.AddControllers();
        service.AddRazorPages();
        service.AddAWSService<IAmazonS3>();
        service.AddTransient<IFileUploadService, FileUploadService>();

        service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
          {
              options.ExpireTimeSpan = TimeSpan.FromMinutes(60 * 1);
              options.LoginPath = "/Auth/Login";
              options.AccessDeniedPath = "/Auth/Login";
          });
        service.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    private static void _assemblyInjection(IServiceCollection service, string subFix)
    {
        Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(a => a.Name.EndsWith(subFix) && a is { IsAbstract: false, IsInterface: false })
            .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
            .ToList()
            .ForEach(typesToRegister =>
            {
                if (subFix.Contains("Singleton"))
                {
                    typesToRegister.serviceTypes.ForEach(typeToRegister =>
                        service.AddSingleton(typeToRegister, typesToRegister.assignedType));
                }
                else
                {
                    typesToRegister.serviceTypes.ForEach(typeToRegister =>
                        service.AddScoped(typeToRegister, typesToRegister.assignedType));
                }
            });
    }
}