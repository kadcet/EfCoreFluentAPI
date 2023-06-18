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

			#region Product Configurations
			modelBuilder.Entity<Product>().Ignore(p => p.IncludeTax);
			modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(9,2)").IsRequired();
			modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(128).IsRequired();
			modelBuilder.Entity<Product>().Property(p => p.CategoryId).IsRequired(false);

			modelBuilder.Entity<Product>()
				.HasOne<Category>(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId)
				.OnDelete(DeleteBehavior.SetNull);
			#endregion

			#region One to One
			modelBuilder.Entity<Customer>()
					.HasOne<CustomerAddress>(c=>c.Address)
					.WithOne(ca => ca.Customer)
					.HasForeignKey<CustomerAddress>(ad => ad.AddressOfCustomerId)
					.OnDelete(DeleteBehavior.Cascade);
			#endregion

			modelBuilder.Entity<Post>()
				.HasMany(p => p.Tags)
				.WithMany(t => t.Posts);

		}

		public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<CustomerAddress> CustomerAddresses { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Tag> Tags { get; set; }



    }
}