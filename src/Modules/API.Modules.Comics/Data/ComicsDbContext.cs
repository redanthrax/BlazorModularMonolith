using API.Modules.Comics.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Comics.Data;

public class ComicsDbContext : DbContext {
    public ComicsDbContext(DbContextOptions<ComicsDbContext> options) : base(options) {
    }

    public DbSet<Comic> Comics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Comic>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Publisher).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });
    }
}
