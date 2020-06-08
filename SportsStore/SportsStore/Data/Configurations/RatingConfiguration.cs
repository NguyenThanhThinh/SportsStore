using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportsStore.Models;

namespace SportsStore.Data.Configurations
{
	public class RatingConfiguration : IEntityTypeConfiguration<Rating>
	{
		public void Configure(EntityTypeBuilder<Rating> builder)
		{
			builder.ToTable("Ratings");

		}

	}
}
