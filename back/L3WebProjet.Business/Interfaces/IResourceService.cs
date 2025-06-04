using L3WebProjet.Common.DTO;

namespace L3WebProjet.Business.Interfaces
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceDto>> GetAllResourcesAsync();
        Task<ResourceDto?> GetResourceByIdAsync(Guid id);
        Task<IEnumerable<ResourceDto>> GetResourcesByStoreIdAsync(Guid storeId);
        Task CreateResourceAsync(ResourceDto resource);
        Task UpdateResourceAsync(ResourceDto resource);
        Task DeleteResourceAsync(Guid id);
    }
}