using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<StoreDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StoreDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(StoreDto store, CancellationToken cancellationToken = default);
        Task UpdateAsync(StoreDto store, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}