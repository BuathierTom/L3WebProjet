namespace L3WebProjet.Common.DTO
{
    public class StoreDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "Mon Vidéoclub";
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; }
    }
}