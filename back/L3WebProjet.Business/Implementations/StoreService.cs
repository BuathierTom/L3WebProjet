using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;
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
            var daos = await _storeRepository.GetAllAsync();
            return daos.Select(s => s.ToDto());
        }

        public async Task<IEnumerable<StoreDto>> GetAllStoresAsync(CancellationToken cancellationToken = default)
        {
            var daos = await _storeRepository.GetAllAsync(cancellationToken);
            return daos.Select(s => s.ToDto());
        }

        public async Task<StoreDto?> GetStoreByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dao = await _storeRepository.GetByIdAsync(id, cancellationToken);
            return dao?.ToDto();
        }

        public async Task<IEnumerable<StoreDto>> GetStoresByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var daos = await _storeRepository.GetByUserIdAsync(userId, cancellationToken);
            return daos.Select(s => s.ToDto());
        }

        public async Task<StoreDto> CreateStoreAsync(StoreCreateRequest request, CancellationToken cancellationToken = default)
        {
            var store = new StoreDao
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                LastCollectedAt = DateTime.UtcNow,
                UserId = request.UserId
            };

            await _storeRepository.AddAsync(store, cancellationToken);
            return store.ToDto();
        }

        public async Task UpdateStoreAsync(StoreUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var store = new StoreDao
            {
                Id = request.Id,
                Name = request.Name,
                UserId = request.UserId
            };

            await _storeRepository.UpdateAsync(store, cancellationToken);
        }

        public async Task DeleteStoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _storeRepository.DeleteAsync(id, cancellationToken);
        }
    }
}