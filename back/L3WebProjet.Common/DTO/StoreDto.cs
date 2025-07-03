using L3WebProjet.Common.DAO;

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
    
    public static class StoreDtoExtensions
    {
        public static StoreDto ToDto(this StoreDao dao)
        {
            return new StoreDto
            {
                Id = dao.Id,
                Name = dao.Name,
                CreatedAt = dao.CreatedAt,
                LastCollectedAt = dao.LastCollectedAt,
                UserId = dao.UserId
            };
        }
    }
}