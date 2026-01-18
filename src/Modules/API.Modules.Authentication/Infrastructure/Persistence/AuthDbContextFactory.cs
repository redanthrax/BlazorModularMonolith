using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Modules.Authentication.Infrastructure.Persistence;

public class AuthDbContextFactory : IDesignTimeDbContextFactory<AuthDbContext> {
    public AuthDbContext CreateDbContext(string[] args) {
        var optionsBuilder = new DbContextOptionsBuilder<AuthDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Database=blazormodularmonolith;Username=postgres;Password=postgres");
        return new AuthDbContext(optionsBuilder.Options);
    }
}
