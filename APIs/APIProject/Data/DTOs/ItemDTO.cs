namespace APIProject.Data.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal ItemPrice { get; set; }
        public string ItemDescription { get; set; }
        public IFormFile Image { get; set; }
        public int CategoryId { get; set; }
    }
}
