using API.Modules.Comics.Infrastructure.Persistence;
using Common.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;

namespace API.Modules.Comics;

public class ComicsModule : IModuleInitializer {
    public void RegisterServices(IServiceCollection services) {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<ComicsDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public void ConfigureWolverine(WolverineOptions options) {
        options.Discovery.IncludeAssembly(typeof(ComicsModule).Assembly);
    }
}
