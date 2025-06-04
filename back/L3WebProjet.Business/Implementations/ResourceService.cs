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

        public async Task<IEnumerable<ResourceDto>> GetAllResourcesAsync()
        {
            return await _resourceRepository.GetAllAsync();
        }

        public async Task<ResourceDto?> GetResourceByIdAsync(Guid id)
        {
            return await _resourceRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ResourceDto>> GetResourcesByStoreIdAsync(Guid storeId)
        {
            return await _resourceRepository.GetByStoreIdAsync(storeId);
        }

        public async Task<ResourceDto> CreateResourceAsync(ResourceCreateRequest request)
        {
            var resource = new ResourceDto
            {
                Id = Guid.NewGuid(),
                Type = request.Type, 
                Amount= request.Amount,
                StoreId = request.StoreId
            };

            await _resourceRepository.AddAsync(resource);
            return resource;
        }

        public async Task UpdateResourceAsync(ResourceUpdateRequest request)
        {
            var resource = new ResourceDto
            {
                Id = request.Id,
                Type = request.Type,
                Amount = request.Amount,
                StoreId = request.StoreId
            };

            await _resourceRepository.UpdateAsync(resource);
        }

        public async Task DeleteResourceAsync(Guid id)
        {
            await _resourceRepository.DeleteAsync(id);
        }

    }
}