using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<SectionDto>> GetAllAsync();
        Task<SectionDto?> GetByIdAsync(Guid id);
        Task AddAsync(SectionDto section);
    }
}