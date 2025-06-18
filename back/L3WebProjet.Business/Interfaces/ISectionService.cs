using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface ISectionService
    {
        Task<IEnumerable<SectionDto>> GetAllSectionsAsync(CancellationToken cancellationToken = default);
        Task<SectionDto?> GetSectionByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SectionDto>> GetSectionsByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task<SectionDto> CreateSectionAsync(SectionCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateSectionAsync(SectionUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteSectionAsync(Guid id, CancellationToken cancellationToken = default);
    }
}