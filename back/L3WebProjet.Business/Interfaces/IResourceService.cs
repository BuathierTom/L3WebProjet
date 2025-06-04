using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface IResourceService
    {
        Task<IEnumerable<ResourceDto>> GetAllResourcesAsync(CancellationToken cancellationToken = default);
        Task<ResourceDto?> GetResourceByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<ResourceDto>> GetResourcesByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default);
        Task<ResourceDto> CreateResourceAsync(ResourceCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateResourceAsync(ResourceUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteResourceAsync(Guid id, CancellationToken cancellationToken = default);
    }
}