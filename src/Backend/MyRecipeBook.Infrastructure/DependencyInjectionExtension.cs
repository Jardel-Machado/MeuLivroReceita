using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContextMySql(services, configuration);
        AddRepositories(services);
    }

    //private static void AddDbContextSqlServer(IServiceCollection services)
    //{
    //    services.AddDbContext<MyRecipeBookDbContext>(options =>
    //    {
    //        var connectionString = "Data Source=JARDELHOME\\SQLEXPRESS;database=meulivrodereceitas;Trusted_connection=true; Encrypt=false; TrustServerCertificate=true";

    //        options.UseSqlServer(connectionString);
    //    });
    //} 
    
    private static void AddDbContextMySql(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MyRecipeBookDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 40));

            options.UseMySql(connectionString, serverVersion);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
