using System.ComponentModel.DataAnnotations;

namespace T2207A_API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Thumbnall { get; set; }
        public int Qty { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } // Thêm tên danh mục vào DTO
    }
}
