using Exam.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam.DBContexts
{
    public class MyDBContext : DbContext
    {
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables  
            modelBuilder.Entity<Category>().ToTable("Categorys");
            modelBuilder.Entity<Product>().ToTable("Products");

            // Configure Primary Keys  
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID).HasName("CategoryID");
            modelBuilder.Entity<Product>().HasKey(u => u.ProductID).HasName("ProductID");

            // Configure columns  
            modelBuilder.Entity<Category>().Property(c => c.CategoryID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.CategoryName).HasColumnType("nvarchar(100)").IsRequired();
            modelBuilder.Entity<Category>().Property(c => c.Description).HasColumnType("nvarchar(100)").IsRequired(false);
            modelBuilder.Entity<Category>().Property(c => c.Picture).HasColumnType("nvarchar(100)").IsRequired(false);

            modelBuilder.Entity<Product>().Property(p => p.ProductID).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ProductName).HasColumnType("nvarchar(50)").IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.SubplierID).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.CategoryID).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.QuantityPerUnit).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.UnitsInStock).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.ReorderLevel).HasColumnType("int").IsRequired(false);
            modelBuilder.Entity<Product>().Property(p => p.Discontinued).HasColumnType("int").IsRequired(true).HasDefaultValue(0);

            // Configure relationships  
            modelBuilder.Entity<Product>().HasOne<Category>().WithMany().HasPrincipalKey(c => c.CategoryID).HasForeignKey(p => p.CategoryID).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Products_Categorys");
        }
    }
}