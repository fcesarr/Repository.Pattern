using Microsoft.EntityFrameworkCore;

namespace Repository.Pattern.Entities.Contexts;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options)
    {
    }

    public required DbSet<Car> Cars { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasData(new Car
            {
                Name = "Xpto"
            });
    }
}
