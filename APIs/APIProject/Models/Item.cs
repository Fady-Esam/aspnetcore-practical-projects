using System.ComponentModel.DataAnnotations;

namespace APIProject.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter The Item Name")]
        [StringLength(100, ErrorMessage = "Item Name cannot exceed 100 characters.")]
        public string ItemName { get; set; }
        [Range(0, 99999.99, ErrorMessage = "Price Must be between 0 and 99999.99")]
        public decimal ItemPrice { get; set; }
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string ItemDescription { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
