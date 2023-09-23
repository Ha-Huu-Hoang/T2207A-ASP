using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2207A_API.DTOs;
using T2207A_API.Entities;
using T2207A_API.Model;

namespace T2207A_API.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly T2207aApiContext _context;
        public ProductController(T2207aApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Product> products = _context.Products.Include(p=>p.Category).ToList();
            List<ProductDTO> data = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Thumbnall = p.Thumbnall,
                Qty = p.Qty,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name // Thêm tên danh mục vào DTO
            }).ToList();

            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            try
            {
                Product product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

                if (product != null)
                {
                    ProductDTO data = new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        Thumbnall = product.Thumbnall,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category.Name // Thêm tên danh mục vào DTO
                    };
                    return Ok(data);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return NotFound();
        }
        [HttpPost]
        public IActionResult Create(CreateProduct model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Product product = new Product
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Description = model.Description,
                        Thumbnall = model.Thumbnall,
                        Qty = model.Qty,
                        CategoryId = model.CategoryId
                    };
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    // Kiểm tra xem Category có tồn tại hay không trước khi truy cập Name

                    ProductDTO data = new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Description = product.Description,
                        Thumbnall = product.Thumbnall,
                        Qty = product.Qty,
                        CategoryId = product.CategoryId,
                     
                    };

                    return Created($"api/product/{data.Id}", data);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            var msgs = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage);
            return BadRequest(string.Join(" | ", msgs));
        }
        

        
    }
}
