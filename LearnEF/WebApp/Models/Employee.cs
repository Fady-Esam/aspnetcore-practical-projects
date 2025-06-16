using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public string? JobTilte { get; set; }

    }
}
