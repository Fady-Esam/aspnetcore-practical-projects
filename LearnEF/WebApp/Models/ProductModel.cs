using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

using Microsoft.Extensions.Primitives;



namespace WebApp.Models
{
    public class ProductModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [DisplayName("ProductNa")]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime CreatdDate { get; set; } = DateTime.Now;
        [DisplayName("CategoryName")]
        public int? CategoryId { get; set; }
        public string? imagePath { get; set; }
        [NotMapped]

        public IFormFile? ClientFile { get; set; }
    }
    
}
