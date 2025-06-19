using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class SectionService : ISectionService
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IResourceRepository _resourceRepository;

        public SectionService(ISectionRepository sectionRepository, IStoreRepository storeRepository, IResourceRepository resourceRepository)
        {
            _sectionRepository = sectionRepository;
            _storeRepository = storeRepository;
            _resourceRepository = resourceRepository;
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
        
        public async Task<bool> UpgradeSectionAsync(Guid sectionId, CancellationToken cancellationToken = default)
        {
            var section = await _sectionRepository.GetByIdAsync(sectionId, cancellationToken);
            if (section == null) return false;
        
            const int maxSectionLevel = 50;
            if (section.Level >= maxSectionLevel)
                return false;
            
            var store = await _storeRepository.GetByIdAsync(section.StoreId, cancellationToken);
            if (store == null) return false;

            var resources = await _resourceRepository.GetByStoreIdAsync(store.Id, cancellationToken);
            var money = resources.FirstOrDefault(r => r.Type == "Money");
            if (money == null) return false;

            var upgradeCost = CalculateUpgradeCost(section.Level);

            if (money.Amount < upgradeCost) return false;

            money.Amount -= upgradeCost;
            section.Level++;

            await _resourceRepository.UpdateAsync(money, cancellationToken);
            await _sectionRepository.UpdateAsync(section, cancellationToken);

            return true;
        }

        private int CalculateUpgradeCost(int level)
        {
            return (int)(100 * Math.Pow(level, 1.6));
        }

    }
}