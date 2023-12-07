using Microsoft.EntityFrameworkCore;
using Cookiemonster.Infrastructure.EFRepository.Models;

namespace Cookiemonster.Infrastructure.EFRepository.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.RecipeId)
                .HasName("Recipe");
            modelBuilder.Entity<Recipe>()
                .Property(r => r.Description)
                .HasColumnName("Description");
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Votes)
                .WithOne(v => v.Recipe)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Recipe>()
                .HasOne(r => r.User)
                .WithMany(u => u.Recipes)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Images)
                .WithOne(i => i.Recipe)
                .HasForeignKey(r => r.ImageId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Todos)
                .WithOne(t => t.Recipe)
                .HasForeignKey(r => r.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId)
                .HasName("Category");
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Recipes)
                .WithOne(r => r.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Image>()
                .HasKey(i => i.ImageId)
                .HasName("Image");
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Recipe)
                .WithMany(r => r.Images)
                .HasForeignKey(i => i.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId)
                .HasName("User");
            modelBuilder.Entity<User>()
                .HasMany(u => u.Recipes)
                .WithOne(r => r.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Votes)
                .WithOne(v => v.User)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Todo>()
                .HasKey(t => new { t.RecipeId, t.UserId })
                .HasName("Todo");
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.Recipe)
                .WithMany(r => r.Todos)
                .HasForeignKey(t => t.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.User)
                .WithMany(u => u.Todos)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasKey(v => new { v.RecipeId, v.UserId })
                .HasName("Vote");
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.User)
                .WithMany(u => u.Votes)
                .HasForeignKey(v => v.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Recipe)
                .WithMany(r => r.Votes)
                .HasForeignKey(v => v.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

