using API.Modules.Boxes.Infrastructure.Persistence;
using Common.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;

namespace API.Modules.Boxes;

public class BoxesModule : IModuleInitializer {
    public void RegisterServices(IServiceCollection services) {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<BoxesDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public void ConfigureWolverine(WolverineOptions options) {
        options.Discovery.IncludeAssembly(typeof(BoxesModule).Assembly);
    }
}
