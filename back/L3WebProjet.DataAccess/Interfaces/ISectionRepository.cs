using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<SectionDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SectionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SectionDto>> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task AddAsync(SectionDto section, CancellationToken cancellationToken = default);
        Task UpdateAsync(SectionDto section, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}