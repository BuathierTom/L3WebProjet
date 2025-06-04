using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDto>> GetAllSectionsAsync();
        Task<SectionDto?> GetSectionByIdAsync(Guid id);
        Task<IEnumerable<SectionDto>> GetSectionsByStoreIdAsync(Guid storeId);
        Task<SectionDto> CreateSectionAsync(SectionCreateRequest request);
        Task UpdateSectionAsync(SectionUpdateRequest request);
        Task DeleteSectionAsync(Guid id);
    }
}