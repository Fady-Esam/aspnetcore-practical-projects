using System.ComponentModel.DataAnnotations;

namespace Trips.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter your destination")]
        public string Destination { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please Enter your Comidation")]
        public string Comidation { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }

    }
}
