using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<SectionDao>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<SectionDao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<SectionDao>> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task AddAsync(SectionDao section, CancellationToken cancellationToken = default);
        Task UpdateAsync(SectionDao section, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}