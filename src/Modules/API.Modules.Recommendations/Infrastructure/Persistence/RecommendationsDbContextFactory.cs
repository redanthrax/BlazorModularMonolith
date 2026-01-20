using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Modules.Recommendations.Infrastructure.Persistence;

public class RecommendationsDbContextFactory : IDesignTimeDbContextFactory<RecommendationsDbContext> {
    public RecommendationsDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<RecommendationsDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=blazormodularmonolith;Username=postgres;Password=postgres");
        return new RecommendationsDbContext(optionsBuilder.Options);
    }
}
