using API.Modules.Recommendations.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Modules.Recommendations.Infrastructure.Persistence;

public class RecommendationsDbContext : DbContext {
    public RecommendationsDbContext(DbContextOptions<RecommendationsDbContext> options) : base(options) {
    }

    public DbSet<Recommendation> Recommendations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Recommendation>(entity => {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Reason).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Score).HasPrecision(5, 2);
            entity.HasIndex(e => e.SourceComicId);
            entity.HasIndex(e => e.RecommendedComicId);
        });
    }
}
