namespace ITLagerVerwaltungSystem.Core.DTOs
{
    public class MaterialDto
    {
        // Add properties as needed
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }

        // Collection of file paths for material pictures
        public ICollection<string>? PicturePaths { get; set; }

        // Quantity of materials in stock
        public int Quantity { get; set; }
    }
}
