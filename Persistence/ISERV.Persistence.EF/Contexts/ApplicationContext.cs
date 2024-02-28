using ISERV.Persistence.EF.Entities;
using ISERV.Persistence.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext, IMigration
{
    public DbSet<University> Universities { get; set; } = null!;
    private readonly DatabaseSettings _databaseSettings;

    public ApplicationContext(DatabaseSettings settings)
    {
        _databaseSettings = settings;
    }

    public void ReCreateDatabase()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseSettings.ConnectionString);
    }
}