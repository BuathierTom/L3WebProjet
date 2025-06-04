using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IResourceRepository
    {
        Task<IEnumerable<ResourceDto>> GetAllAsync();
        Task<ResourceDto?> GetByIdAsync(Guid id);
        Task AddAsync(ResourceDto resource);
        Task<IEnumerable<ResourceDto>> GetByStoreIdAsync(Guid storeId);
    }
}