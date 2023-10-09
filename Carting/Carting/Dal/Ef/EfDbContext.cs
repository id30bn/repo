using Carting.Core.Models.Cart;
using Microsoft.EntityFrameworkCore;

namespace Carting.Dal.Ef
{
	public class EfDbContext : DbContext
    {
        public DbSet<Cart> Carts { get; set; }

        public EfDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //	optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=appdb;Trusted_Connection=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>().Property(x => x.Id).ValueGeneratedNever();

            modelBuilder.Entity<Item>().OwnsOne(x => x.Image);
        }
    }
}
