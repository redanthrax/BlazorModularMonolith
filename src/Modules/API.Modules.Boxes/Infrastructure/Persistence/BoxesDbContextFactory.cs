using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Modules.Boxes.Infrastructure.Persistence;

public class BoxesDbContextFactory : IDesignTimeDbContextFactory<BoxesDbContext> {
    public BoxesDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<BoxesDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=blazormodularmonolith;Username=postgres;Password=postgres");
        return new BoxesDbContext(optionsBuilder.Options);
    }
}
