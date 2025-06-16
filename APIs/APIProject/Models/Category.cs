using System.ComponentModel.DataAnnotations;

namespace APIProject.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter The Category Name")]
        [StringLength(100, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
    }

}
