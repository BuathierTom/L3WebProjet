using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;

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

        public async Task CreateResourceAsync(ResourceDto resource)
        {
            await _resourceRepository.AddAsync(resource);
        }

        public async Task UpdateResourceAsync(ResourceDto resource)
        {
            await _resourceRepository.UpdateAsync(resource);
        }

        public async Task DeleteResourceAsync(Guid id)
        {
            await _resourceRepository.DeleteAsync(id);
        }

    }
}