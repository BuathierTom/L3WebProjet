using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IResourceRepository
    {
        Task<IEnumerable<ResourceDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ResourceDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ResourceDto>> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task AddAsync(ResourceDto resource, CancellationToken cancellationToken = default);
        Task UpdateAsync(ResourceDto resource, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}