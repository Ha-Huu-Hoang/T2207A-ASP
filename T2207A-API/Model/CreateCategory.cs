using System.ComponentModel.DataAnnotations;

namespace T2207A_API.Model.Category
{
    public class CreateCategory
    {
        [Required(ErrorMessage ="vui long nhap du kieu")]
        [MinLength(3,ErrorMessage = "Nhap toi thieu 3 ky tu")]
        [MaxLength(255,ErrorMessage ="Nhap toi thieu 3 ky tu")]
        public string name { get; set; }
    }
}
