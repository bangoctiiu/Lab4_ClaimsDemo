using Lab4_ClaimsDemo.Data;
using Lab4_ClaimsDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab4_ClaimsDemo.Controllers
{
    [Authorize] // yêu cầu đăng nhập
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // ================== INDEX ==================
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Images)
                .ToListAsync();

            return View(products);
        }

        // ================== DETAILS ==================
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

// ================== CREATE (GET) ==================
[Authorize(Policy = "CreateProductPolicy")]
public IActionResult Create()
        {
            return View();
        }

        // ================== CREATE (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> Create(Product product, List<IFormFile> Images)
        {
            // Loại bỏ kiểm tra ModelState cho 2 field này
            ModelState.Remove("CreatedBy");
            ModelState.Remove("MainImage");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                ViewBag.Errors = errors;
                return View(product);
            }

            if (Images == null || Images.Count == 0)
            {
                ViewBag.ImageError = "Cần chọn ít nhất 1 ảnh.";
                return View(product);
            }

            product.Images = new List<ProductImage>();
            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            for (int i = 0; i < Images.Count; i++)
            {
                var file = Images[i];
                if (file.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var imageUrl = "/uploads/" + fileName;

                    if (i == 0)
                        product.MainImage = imageUrl; // Ảnh chính
                    else
                        product.Images.Add(new ProductImage { ImageUrl = imageUrl });
                }
            }

            product.CreatedBy = User.Identity?.Name;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================== EDIT (GET) ==================
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        // ================== EDIT (POST) ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> Edit(int id, Product product, List<IFormFile>? newImages,
                                     string? keepMainImage, int[]? keepImageIds)
        {
            if (id != product.Id) return NotFound();

            ModelState.Remove("CreatedBy");
            ModelState.Remove("MainImage");

            if (ModelState.IsValid)
            {
                var existingProduct = await _context.Products
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingProduct == null) return NotFound();

                // Update thông tin cơ bản
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Price = product.Price;

                // ====== Xử lý giữ / xóa ảnh ======
                // Nếu người dùng xóa MainImage
                if (string.IsNullOrEmpty(keepMainImage))
                {
                    existingProduct.MainImage = null;
                }

                // Giữ lại các ảnh phụ trong danh sách keepImageIds
                if (keepImageIds != null)
                {
                    existingProduct.Images = existingProduct.Images
                        .Where(img => keepImageIds.Contains(img.Id))
                        .ToList();
                }
                else
                {
                    existingProduct.Images.Clear();
                }

                // ====== Thêm ảnh mới ======
                if (newImages != null && newImages.Count > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    foreach (var file in newImages)
                    {
                        if (file.Length > 0)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                            var filePath = Path.Combine(uploadsFolder, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var imageUrl = "/uploads/" + fileName;

                            // Nếu chưa có MainImage thì set ảnh đầu tiên
                            if (string.IsNullOrEmpty(existingProduct.MainImage))
                                existingProduct.MainImage = imageUrl;
                            else
                                existingProduct.Images.Add(new ProductImage { ImageUrl = imageUrl });
                        }
                    }
                }

                _context.Update(existingProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        // ================== DELETE ==================
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CreateProductPolicy")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                // Xóa ảnh chính
                if (!string.IsNullOrEmpty(product.MainImage))
                {
                    var mainPath = Path.Combine(_env.WebRootPath, product.MainImage.TrimStart('/'));
                    if (System.IO.File.Exists(mainPath))
                        System.IO.File.Delete(mainPath);
                }

                // Xóa ảnh phụ
                foreach (var img in product.Images)
                {
                    var path = Path.Combine(_env.WebRootPath, img.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
