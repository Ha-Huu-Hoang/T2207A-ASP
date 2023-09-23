using System.ComponentModel.DataAnnotations;

namespace T2207A_API.Model
{
    public class CreateProduct
    {
        [Required(ErrorMessage = "vui long nhap du kieu")]
        [MinLength(3, ErrorMessage = "Nhap toi thieu 3 ky tu")]
        [MaxLength(255, ErrorMessage = "Nhap toi thieu 3 ky tu")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Thumbnall { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater than or equal to 0")]
        public int Qty { get; set; }

        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
    }
   
}

