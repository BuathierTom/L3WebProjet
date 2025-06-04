namespace L3WebProjet.Common.Request
{
    public class SectionUpdateRequest
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Level { get; set; }
        public bool IsUnderConstruction { get; set; }
        public DateTime? ConstructionEnd { get; set; }
        public Guid StoreId { get; set; }
    }
}