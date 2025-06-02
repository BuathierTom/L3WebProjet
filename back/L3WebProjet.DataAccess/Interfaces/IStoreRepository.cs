using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreDto>> GetAllAsync();
        Task<StoreDto?> GetByIdAsync(Guid id);
        Task AddAsync(StoreDto store);
    }
}