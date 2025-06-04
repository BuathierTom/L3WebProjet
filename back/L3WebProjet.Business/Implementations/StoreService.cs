using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;

        public StoreService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
        {
            return await _storeRepository.GetAllAsync();
        }

        public async Task<StoreDto?> GetStoreByIdAsync(Guid id)
        {
            return await _storeRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<StoreDto>> GetStoresByUserIdAsync(Guid userId)
        {
            return await _storeRepository.GetByUserIdAsync(userId);
        }
        public async Task<StoreDto> CreateStoreAsync(StoreCreateRequest request)
        {
            var store = new StoreDto { Id = Guid.NewGuid(), Name = request.Name, CreatedAt = DateTime.UtcNow, UserId = request.UserId };
            await _storeRepository.AddAsync(store);
            return store;
        }

        public async Task UpdateStoreAsync(StoreUpdateRequest request)
        {
            var store = new StoreDto { Id = request.Id, Name = request.Name, UserId = request.UserId };
            await _storeRepository.UpdateAsync(store);
        }

        public async Task DeleteStoreAsync(Guid id)
        {
            await _storeRepository.DeleteAsync(id);
        }

    }
}