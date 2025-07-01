using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreDao>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<StoreDao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StoreDao>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task AddAsync(StoreDao store, CancellationToken cancellationToken = default);
        Task UpdateAsync(StoreDao store, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}