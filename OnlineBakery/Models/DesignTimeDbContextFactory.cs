using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace OnlineBakery.Models
{
  public class OnlineBakeryContextFactory : IDesignTimeDbContextFactory<OnlineBakeryContext>
  {

    OnlineBakeryContext IDesignTimeDbContextFactory<OnlineBakeryContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<OnlineBakeryContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new OnlineBakeryContext(builder.Options);
    }
  }
}