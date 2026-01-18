using API.Modules.Authentication.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Authentication.Infrastructure.Persistence;

public class AuthDbContext : DbContext {
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
        });
    }
}
