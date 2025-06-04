using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync();
        Task<StoreDto?> GetStoreByIdAsync(Guid id);
        Task<IEnumerable<StoreDto>> GetStoresByUserIdAsync(Guid userId);
        Task<StoreDto> CreateStoreAsync(StoreCreateRequest request);
        Task UpdateStoreAsync(StoreUpdateRequest request);
        Task DeleteStoreAsync(Guid id);
    }
}