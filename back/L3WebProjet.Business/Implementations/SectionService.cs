using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionService(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IEnumerable<SectionDto>> GetAllSectionsAsync(CancellationToken cancellationToken = default)
        {
            return await _sectionRepository.GetAllAsync(cancellationToken);
        }

        public async Task<SectionDto?> GetSectionByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _sectionRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<SectionDto>> GetSectionsByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            return await _sectionRepository.GetByStoreIdAsync(storeId, cancellationToken);
        }

        public async Task<SectionDto> CreateSectionAsync(SectionCreateRequest request, CancellationToken cancellationToken = default)
        {
            var section = new SectionDto
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Level = 1,
                IsUnderConstruction = false,
                ConstructionEnd = null,
                StoreId = request.StoreId
            };

            await _sectionRepository.AddAsync(section, cancellationToken);
            return section;
        }

        public async Task UpdateSectionAsync(SectionUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var section = new SectionDto
            {
                Id = request.Id,
                Type = request.Type,
                Level = request.Level,
                StoreId = request.StoreId
            };

            await _sectionRepository.UpdateAsync(section, cancellationToken);
        }

        public async Task DeleteSectionAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _sectionRepository.DeleteAsync(id, cancellationToken);
        }
        
    }
}