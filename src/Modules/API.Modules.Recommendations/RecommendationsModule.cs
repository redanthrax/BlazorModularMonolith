using API.Modules.Recommendations.Infrastructure.Persistence;
using Common.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;

namespace API.Modules.Recommendations;

public class RecommendationsModule : IModuleInitializer {
    public void RegisterServices(IServiceCollection services) {
        var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        var connectionString = configuration.GetConnectionString("Postgres");

        services.AddDbContext<RecommendationsDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public void ConfigureWolverine(WolverineOptions options) {
        options.Discovery.IncludeAssembly(typeof(RecommendationsModule).Assembly);
        options.Discovery.IncludeAssembly(typeof(IntegrationEvents.Comics.ComicCreatedIntegrationEvent).Assembly);
        options.Discovery.IncludeType<Features.Handlers.ComicCreatedEventHandler>();
    }
}
