using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Lab4_ClaimsDemo.Models;
using System.Collections.Generic; // Cần để dùng List<T>

namespace Lab4_ClaimsDemo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- Cấu hình quan hệ ---
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // --- Seed Data (Tổ chức lại cho gọn gàng) ---

            // 1. Tạo danh sách dữ liệu cho Product
            var productsToSeed = new List<Product>
            {
                new Product { Id = 101, Name = "Áo thun basic unisex", Price = 250000, MainImage = "https://images.unsplash.com/photo-1618354691438-25e9c1c7c1b6?w=800&q=80" },
                new Product { Id = 102, Name = "Áo sơ mi công sở", Price = 450000, MainImage = "https://images.unsplash.com/photo-1523381294911-8d3cead13475?w=800&q=80" },
                new Product { Id = 103, Name = "Váy liền nữ", Price = 690000, MainImage = "https://images.unsplash.com/photo-1526170375885-4d8ecf77b99f?w=800&q=80" },
                new Product { Id = 104, Name = "Quần jeans thời trang", Price = 520000, MainImage = "https://images.unsplash.com/photo-1582552956871-1d57c8dd63d1?w=800&q=80" }
            };

            // 2. Tạo danh sách dữ liệu cho ProductImage
            var productImagesToSeed = new List<ProductImage>
            {
                new ProductImage { Id = 201, ProductId = 101, ImageUrl = "https://images.unsplash.com/photo-1602810318383-e72903fdda7a?w=800&q=80" },
                new ProductImage { Id = 202, ProductId = 102, ImageUrl = "https://images.unsplash.com/photo-1539990121801-0471a6c5aa53?w=800&q=80" },
                new ProductImage { Id = 203, ProductId = 103, ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?w=800&q=80" },
                new ProductImage { Id = 204, ProductId = 104, ImageUrl = "https://images.unsplash.com/photo-1600185365316-76e98b3bc19d?w=800&q=80" }
            };

            // 3. Gọi HasData một lần cho mỗi bảng
            modelBuilder.Entity<Product>().HasData(productsToSeed);
            modelBuilder.Entity<ProductImage>().HasData(productImagesToSeed);
        }
    }
}
