using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SportsStore.Data
{
	public class SportsStoreDbContextFactory : IDesignTimeDbContextFactory<SportsStoreDbContext>
	{


		SportsStoreDbContext IDesignTimeDbContextFactory<SportsStoreDbContext>.CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = configuration.GetConnectionString("SportsStoreDbContext");

			var optionsBuilder = new DbContextOptionsBuilder<SportsStoreDbContext>();
			optionsBuilder.UseSqlServer(connectionString);

			return new SportsStoreDbContext(optionsBuilder.Options);
		}
	}
}
