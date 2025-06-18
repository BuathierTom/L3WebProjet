namespace L3WebProjet.Common.DTO
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid StoreId { get; set; }
    }
}