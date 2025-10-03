using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore; // thêm cái này để dùng [Precision]

namespace Lab4_ClaimsDemo.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Precision(18, 2)] // decimal(18,2) trong SQL
        public decimal Price { get; set; }   // chỉ dùng 1 giá


        [StringLength(1000)]
        public string? Description { get; set; }

        public string? MainImage { get; set; }

        // 👇 Cho phép null để không bắt buộc khi seed
        public string? CreatedBy { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    }
}
