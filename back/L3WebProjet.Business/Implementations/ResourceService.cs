using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ISectionRepository _sectionRepository;

        public ResourceService(IResourceRepository resourceRepository, IStoreRepository storeRepository, ISectionRepository sectionRepository)
        {
            _resourceRepository = resourceRepository;
            _storeRepository = storeRepository;
            _sectionRepository = sectionRepository;
        }
    
        public async Task<IEnumerable<ResourceDto>> GetAllResourcesAsync(CancellationToken cancellationToken = default)
        {
            return await _resourceRepository.GetAllAsync(cancellationToken);
        }

        public async Task<ResourceDto?> GetResourceByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _resourceRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<ResourceDto>> GetResourcesByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            return await _resourceRepository.GetByStoreIdAsync(storeId, cancellationToken);
        }

        public async Task<ResourceDto> CreateResourceAsync(ResourceCreateRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceDto
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Amount = request.Amount,
                StoreId = request.StoreId
            };

            await _resourceRepository.AddAsync(resource, cancellationToken);
            return resource;
        }

        public async Task UpdateResourceAsync(ResourceUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var resource = new ResourceDto
            {
                Id = request.Id,
                Type = request.Type,
                Amount = request.Amount,
                StoreId = request.StoreId
            };

            await _resourceRepository.UpdateAsync(resource, cancellationToken);
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

            var now = DateTime.UtcNow;
            var secondsPassed = (now - store.LastCollectedAt).TotalSeconds;

            var generationRate = sections.Sum(s => s.Level * 10);
            var moneyToAdd = (int)(secondsPassed * generationRate);

            resource.Amount += moneyToAdd;
            store.LastCollectedAt = now;

            await _resourceRepository.UpdateAsync(resource, cancellationToken);
            await _storeRepository.UpdateAsync(store, cancellationToken);

            return resource.Amount;
        }

    }
}