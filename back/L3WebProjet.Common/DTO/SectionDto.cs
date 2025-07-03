using L3WebProjet.Common.DAO;

namespace L3WebProjet.Common.DTO
{
    public class SectionDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = string.Empty; // Ex: "Action", "Comédie"
        public int Level { get; set; }
        public int UpgradePrice => (int)(100 * Math.Pow(Level, 1.6));
        public bool IsUnderConstruction { get; set; }
        public DateTime? ConstructionEnd { get; set; }
        public Guid StoreId { get; set; }
        
        //toDto
        public static SectionDto ToDto(SectionDao sectionDao)
        {
            return new SectionDto
            {
                Id = sectionDao.Id,
                Type = sectionDao.Type,
                Level = sectionDao.Level,
                IsUnderConstruction = sectionDao.IsUnderConstruction,
                ConstructionEnd = sectionDao.ConstructionEnd,
                StoreId = sectionDao.StoreId
            };
        }
    }
}