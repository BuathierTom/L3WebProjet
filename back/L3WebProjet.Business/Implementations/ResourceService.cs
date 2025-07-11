using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;
using L3WebProjet.DataAccess.Interfaces;

namespace L3WebProjet.Business.Implementations
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public ResourceService(IResourceRepository resourceRepository, IStoreRepository storeRepository, ISectionRepository sectionRepository, IWarehouseRepository warehouseRepository)
        {
            _resourceRepository = resourceRepository;
            _storeRepository = storeRepository;
            _sectionRepository = sectionRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<ResourceDto>> GetAllResourcesAsync(CancellationToken cancellationToken = default)
        {
            var daos = await _resourceRepository.GetAllAsync(cancellationToken);
            return daos.Select(ResourceDto.ToDto);
        }

        public async Task<ResourceDto?> GetResourceByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dao = await _resourceRepository.GetByIdAsync(id, cancellationToken);
            return dao is null ? null : ResourceDto.ToDto(dao);
        }

        public async Task<ResourceDto> CreateResourceAsync(ResourceCreateRequest request, CancellationToken cancellationToken = default)
        {
            var dao = new ResourceDao
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Amount = request.Amount,
                LastUpdated = DateTime.UtcNow,
                StoreId = request.StoreId
            };

            await _resourceRepository.AddAsync(dao, cancellationToken);
            return ResourceDto.ToDto(dao);
        }

        public async Task UpdateResourceAsync(ResourceUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var dao = new ResourceDao
            {
                Id = request.Id,
                Type = request.Type,
                Amount = request.Amount,
                LastUpdated = DateTime.UtcNow,
                StoreId = request.StoreId
            };

            await _resourceRepository.UpdateAsync(dao, cancellationToken);
        }

        public async Task DeleteResourceAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _resourceRepository.DeleteAsync(id, cancellationToken);
        }

        public async Task<int> CalculateMoneyAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.GetByIdAsync(storeId, cancellationToken);
            if (store == null)
                throw new Exception("Store not found");

            var sections = await _sectionRepository.GetByStoreIdAsync(storeId, cancellationToken);
            var resource = (await _resourceRepository.GetByStoreIdAsync(storeId, cancellationToken))
                .FirstOrDefault(r => r.Type == "Money");

            if (resource == null)
                throw new Exception("Money resource not found");

            var warehouse = await _warehouseRepository.GetByStoreIdAsync(storeId, cancellationToken);
            if (warehouse == null)
                throw new Exception("Warehouse not found");

            var cap = 10000 * warehouse.Level;

            var now = DateTime.UtcNow;
            var secondsPassed = (now - store.LastCollectedAt).TotalSeconds;

            var generationRate = sections.Sum(s => s.Level * 2);
            var moneyToAdd = (int)(secondsPassed * generationRate);

            resource.Amount = Math.Min(resource.Amount + moneyToAdd, cap);
            store.LastCollectedAt = now;

            await _resourceRepository.UpdateAsync(resource, cancellationToken);
            await _storeRepository.UpdateAsync(store, cancellationToken);

            return resource.Amount;
        }
    }
}