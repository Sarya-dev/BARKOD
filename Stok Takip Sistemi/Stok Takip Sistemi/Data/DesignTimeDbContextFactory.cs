using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StokTakipSistemi.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        optionsBuilder.UseSqlite(ConnectionString.GetConnectionString());
        return new DatabaseContext(optionsBuilder.Options);
    }
}
