using backend_dotnet.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace backend_dotnet.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Case> Cases { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryCategoryPrice> CountryCategoryPrices { get; set; }
        public DbSet<FileMetadata> FileMetadatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>(); // Stores enum as string
            base.OnModelCreating(modelBuilder);

            // Remove unique constraint for UserName (default in Identity)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)  // This will create a non-unique index
                .IsUnique(false);  // By default, Identity adds unique constraint to UserName

            // Ensure that Email is unique
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);  // Enforce unique constraint on Email

        }
    }
}
