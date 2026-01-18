using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Modules.Comics.Infrastructure.Persistence;

public class ComicsDbContextFactory : IDesignTimeDbContextFactory<ComicsDbContext> {
    public ComicsDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<ComicsDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=blazormodularmonolith;Username=postgres;Password=postgres");
        return new ComicsDbContext(optionsBuilder.Options);
    }
}
