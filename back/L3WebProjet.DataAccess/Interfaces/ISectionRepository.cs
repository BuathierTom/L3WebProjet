using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<SectionDto>> GetAllAsync();
        Task<SectionDto?> GetByIdAsync(Guid id);
        Task AddAsync(SectionDto section);
        Task<IEnumerable<SectionDto>> GetByStoreIdAsync(Guid storeId);
        Task UpdateAsync(SectionDto section);
        Task DeleteAsync(Guid id);
    }
}