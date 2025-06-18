namespace L3WebProjet.Common.DTO
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Mon Vidéoclub";
        public DateTime CreatedAt { get; set; }
        public DateTime LastCollectedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
    }
}