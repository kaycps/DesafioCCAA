using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DesafioCCAA.Infrastructure
{
    public class DbContextFactory : IDesignTimeDbContextFactory<DesafioCCAAContext>
    {
        DesafioCCAAContext IDesignTimeDbContextFactory<DesafioCCAAContext>.CreateDbContext(string[] args)
        {
            // Lê o appsettings.json para pegar a connection string
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "DesafioCCAA.API")) // pasta onde está o .csproj
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DesafioCCAAContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new DesafioCCAAContext(optionsBuilder.Options);
        }
    }
}
