using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ArchtistStudio.Core;

namespace ArchtistStudio;

public class MyDbContext(DbContextOptions<MyDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var models = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys());
        foreach (var relationship in models)
        {
            relationship.DeleteBehavior = DeleteBehavior.NoAction;
        }

        modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetProperties())
            .ToList()
            .ForEach(f =>
            {
                switch (f.Name)
                {
                    case "CreatedAt":
                        f.SetDefaultValueSql("now() at time zone 'utc'");
                        break;
                    case "Name":
                        f.SetMaxLength(255);
                        break;
                    case "Color":
                        f.SetMaxLength(20);
                        break;
                }
            });
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

public static class DatabaseInjection
{
    public static void AddDatabase(this IServiceCollection service)
    {

        service.AddDbContext<MyDbContext>(options => { options.UseNpgsql(MyEnvironment.DbConnection); });
    }
}