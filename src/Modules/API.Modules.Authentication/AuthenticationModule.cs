using API.Modules.Authentication.Infrastructure.Persistence;
using API.Modules.Authentication.Infrastructure.Services;
using Common.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;

namespace API.Modules.Authentication;

public class AuthenticationModule : IModuleInitializer {
    public void RegisterServices(IServiceCollection services) {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<TokenService>();
    }

    public void ConfigureWolverine(WolverineOptions options) {
        options.Discovery.IncludeAssembly(typeof(AuthenticationModule).Assembly);
    }
}
