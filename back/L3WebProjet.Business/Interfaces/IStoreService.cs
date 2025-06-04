using L3WebProjet.Common.DTO;

namespace L3WebProjet.Business.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync();
        Task<StoreDto?> GetStoreByIdAsync(Guid id);
        Task<IEnumerable<StoreDto>> GetStoresByUserIdAsync(Guid userId);
        Task CreateStoreAsync(StoreDto store);
        Task UpdateStoreAsync(StoreDto store);
        Task DeleteStoreAsync(Guid id);
    }
}