﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Cookiemonster.Models;

namespace Cookiemonster.Repositories
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
            modelBuilder.Entity<Recipe>().HasKey(r => r.RecipeId).HasName("Recipe");
            modelBuilder.Entity<Recipe>().Property(r => r.Description).HasColumnName("Description");
            modelBuilder.Entity<Recipe>().HasMany(r => r.Votes).WithOne(v => v.Recipe).HasForeignKey(r => r.RecipeId);
            modelBuilder.Entity<Recipe>().HasOne(r => r.User).WithMany(u => u.Recipes).HasForeignKey(r => r.UserId);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Images).WithOne(i => i.Recipe).HasForeignKey(r => r.ImageId);
            modelBuilder.Entity<Recipe>().HasMany(r => r.Todos).WithOne(t => t.Recipe).HasForeignKey(r => r.RecipeId);

            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId).HasName("Category");
            modelBuilder.Entity<Category>().HasMany(c => c.Recipes).WithOne(r => r.Category).HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Image>().HasKey(i => i.ImageId).HasName("Image");
            modelBuilder.Entity<Image>().HasOne(i => i.Recipe).WithMany(r => r.Images).HasForeignKey(i => i.RecipeId);

            modelBuilder.Entity<User>().HasKey(u => u.UserId).HasName("User");
            modelBuilder.Entity<User>().HasMany(u => u.Recipes).WithOne(r => r.User).HasForeignKey(u => u.UserId);
            modelBuilder.Entity<User>().HasMany(u => u.Votes).WithOne(v => v.User).HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Todo>().HasKey(t => new { t.RecipeId, t.UserId }).HasName("Todo");
            modelBuilder.Entity<Todo>().HasOne(t => t.Recipe).WithMany(r => r.Todos).HasForeignKey(t => t.RecipeId);
            modelBuilder.Entity<Todo>().HasOne(t => t.User).WithMany(u => u.Todos).HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Vote>().HasKey(v => new { v.RecipeId, v.UserId }).HasName("Vote");
            modelBuilder.Entity<Vote>().HasOne(v => v.User).WithMany(u => u.Votes).HasForeignKey(v => v.UserId);
            modelBuilder.Entity<Vote>().HasOne(v => v.Recipe).WithMany(r => r.Votes).HasForeignKey(v => v.RecipeId);
        }

    }
}
