using Domain.CategoryAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public DbSet<Category> Category { get; set; }

		public DbSet<Item> Item { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{
			//Database.EnsureDeleted();
			//Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ItemEntityConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
