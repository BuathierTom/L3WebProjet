using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;

        public ResourceService(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
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

    }
}