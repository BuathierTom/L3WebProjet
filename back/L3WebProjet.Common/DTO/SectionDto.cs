namespace L3WebProjet.Common.DTO
{
    public class SectionDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty; // Ex: "Action", "Comédie"
        public int Level { get; set; }
        public bool IsUnderConstruction { get; set; }
        public DateTime? ConstructionEnd { get; set; }

        public Guid StoreId { get; set; }
    }
}