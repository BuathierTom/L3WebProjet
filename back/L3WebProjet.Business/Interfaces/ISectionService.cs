using L3WebProjet.Common.DTO;

namespace L3WebProjet.Business.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDto>> GetAllSectionsAsync();
        Task<SectionDto?> GetSectionByIdAsync(Guid id);
        Task<IEnumerable<SectionDto>> GetSectionsByStoreIdAsync(Guid storeId);
        Task CreateSectionAsync(SectionDto section);
        Task UpdateSectionAsync(SectionDto section);
        Task DeleteSectionAsync(Guid id);
    }
}