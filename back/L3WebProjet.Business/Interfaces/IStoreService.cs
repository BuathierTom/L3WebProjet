using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDto>> GetAllStoresAsync(CancellationToken cancellationToken = default);
        Task<StoreDto?> GetStoreByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<StoreDto>> GetStoresByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<StoreDto> CreateStoreAsync(StoreCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateStoreAsync(StoreUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteStoreAsync(Guid id, CancellationToken cancellationToken = default);
    }
}