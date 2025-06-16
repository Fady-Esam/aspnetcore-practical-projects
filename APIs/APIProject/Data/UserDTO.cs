using System.ComponentModel.DataAnnotations;

namespace APIProject.Data
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        [StringLength(100, ErrorMessage = "Lenght must not exceed 100 character")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(80, ErrorMessage = "Lenght must not exceed 100 character")]
        public string Password { get; set; }
    }
}
