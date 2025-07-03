using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IResourceRepository
    {
        Task<IEnumerable<ResourceDao>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResourceDao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ResourceDao>> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task AddAsync(ResourceDao resource, CancellationToken cancellationToken = default);
        Task UpdateAsync(ResourceDao resource, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}