namespace APIProject.Data.DTOs
{
    internal class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemDTO> Items { get; set; } = new List<ItemDTO>();
    }
}
