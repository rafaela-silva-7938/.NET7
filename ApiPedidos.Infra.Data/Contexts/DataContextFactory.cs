using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ApiPedidos.Infra.Data.Contexts
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Configura o caminho para localizar o appsettings.json no projeto principal
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Obtém a Connection String do appsettings.json
            var connectionString = configuration.GetConnectionString("Conexao");

            // Configura o DbContextOptions com a Connection String
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);

            // Retorna uma instância do DataContext
            return new DataContext(optionsBuilder.Options);
        }
    }
}
