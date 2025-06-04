using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;

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

        public async Task CreateStoreAsync(StoreDto store)
        {
            await _storeRepository.AddAsync(store);
        }

        public async Task UpdateStoreAsync(StoreDto store)
        {
            await _storeRepository.UpdateAsync(store);
        }

        public async Task DeleteStoreAsync(Guid id)
        {
            await _storeRepository.DeleteAsync(id);
        }

    }
}