using Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCorePersistLayer
{
	public class FluentAPIDbContext:DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=FluentAPIDb;Integrated Security=true;TrustServerCertificate=true");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>().Ignore(p => p.IncludeTax);
			modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(9,2)").IsRequired();
			modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(128).IsRequired();

			modelBuilder.Entity<Product>()
				.HasOne<Category>(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);
		}

		public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}