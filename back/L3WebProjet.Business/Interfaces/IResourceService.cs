using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceDto>> GetAllResourcesAsync();
        Task<ResourceDto?> GetResourceByIdAsync(Guid id);
        Task<IEnumerable<ResourceDto>> GetResourcesByStoreIdAsync(Guid storeId);
        Task<ResourceDto> CreateResourceAsync(ResourceCreateRequest request);
        Task UpdateResourceAsync(ResourceUpdateRequest request);
        Task DeleteResourceAsync(Guid id);
    }
}