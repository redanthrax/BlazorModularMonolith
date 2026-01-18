using API.Modules.Boxes.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Boxes.Infrastructure.Persistence;

public class BoxesDbContext : DbContext {
    public BoxesDbContext(DbContextOptions<BoxesDbContext> options) : base(options) {
    }

    public DbSet<Box> Boxes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Box>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Location).HasMaxLength(500);
        });
    }
}
