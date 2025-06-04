using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;

namespace L3WebProjet.Business.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IEnumerable<SectionDto>> GetAllSectionsAsync()
        {
            return await _sectionRepository.GetAllAsync();
        }

        public async Task<SectionDto?> GetSectionByIdAsync(Guid id)
        {
            return await _sectionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<SectionDto>> GetSectionsByStoreIdAsync(Guid storeId)
        {
            return await _sectionRepository.GetByStoreIdAsync(storeId);
        }

        public async Task CreateSectionAsync(SectionDto section)
        {
            await _sectionRepository.AddAsync(section);
        }

        public async Task UpdateSectionAsync(SectionDto section)
        {
            await _sectionRepository.UpdateAsync(section);
        }

        public async Task DeleteSectionAsync(Guid id)
        {
            await _sectionRepository.DeleteAsync(id);
        }

    }
}