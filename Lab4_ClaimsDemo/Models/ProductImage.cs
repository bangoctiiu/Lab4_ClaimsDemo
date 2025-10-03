using System.ComponentModel.DataAnnotations;

namespace Lab4_ClaimsDemo.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        [Required, StringLength(500)] // thêm giới hạn độ dài để tránh ảnh quá dài
        public string ImageUrl { get; set; } = string.Empty;

        [Required] // đảm bảo luôn có ProductId
        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;
    }
}
