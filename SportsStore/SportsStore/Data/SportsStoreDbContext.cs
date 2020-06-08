using Microsoft.EntityFrameworkCore;
using SportsStore.Data.Configurations;
using SportsStore.Models;

namespace SportsStore.Data
{
	public class SportsStoreDbContext : DbContext
	{

		public SportsStoreDbContext(DbContextOptions<SportsStoreDbContext> opts)
			: base(opts)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new ProductConfiguration());
			modelBuilder.ApplyConfiguration(new SupplierConfiguration());
			modelBuilder.ApplyConfiguration(new RatingConfiguration());

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Supplier> Suppliers { get; set; }
		public DbSet<Rating> Ratings { get; set; }
	}
}
