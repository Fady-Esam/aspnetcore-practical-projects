namespace Trips.Models.ViewModels
{
    public class TripVM
    {
        public Trip Trip { get; set; } = new Trip();
        public int PageNumber { get; set; }
    }
}
