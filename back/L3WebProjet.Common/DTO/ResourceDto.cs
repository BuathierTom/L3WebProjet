using L3WebProjet.Common.DAO;

namespace L3WebProjet.Common.DTO
{
    public class ResourceDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Amount { get; set; }
        public DateTime LastUpdated { get; set; }
        public Guid StoreId { get; set; }
        
        public static ResourceDto ToDto(ResourceDao dao)
        {
            return new ResourceDto
            {
                Id = dao.Id,
                Type = dao.Type,
                Amount = dao.Amount,
                LastUpdated = dao.LastUpdated,
                StoreId = dao.StoreId
            };
        }
        
    }
}