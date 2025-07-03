using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.Common.DAO;
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
            var daos = await _sectionRepository.GetAllAsync(cancellationToken);
            return daos.Select(SectionDto.ToDto);
        }

        public async Task<SectionDto?> GetSectionByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dao = await _sectionRepository.GetByIdAsync(id, cancellationToken);
            return dao is null ? null : SectionDto.ToDto(dao);
        }

        public async Task<IEnumerable<SectionDto>> GetSectionsByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            var daos = await _sectionRepository.GetByStoreIdAsync(storeId, cancellationToken);
            return daos.Select(SectionDto.ToDto);
        }

        public async Task<SectionDto> CreateSectionAsync(SectionCreateRequest request, CancellationToken cancellationToken = default)
        {
            var existingSections = await _sectionRepository.GetByStoreIdAsync(request.StoreId, cancellationToken);
            var sectionCount = existingSections.Count();

            int cost = sectionCount switch
            {
                0 => 0,      // Première gratuite
                1 => 100,
                2 => 500,
                _ => 1000
            };

            var resources = await _resourceRepository.GetByStoreIdAsync(request.StoreId, cancellationToken);
            var money = resources.FirstOrDefault(r => r.Type == "Money");

            if (money == null || money.Amount < cost)
                throw new Exception("Not enough money to create a section");

            money.Amount -= cost;
            await _resourceRepository.UpdateAsync(money, cancellationToken);

            var section = new SectionDao
            {
                Id = Guid.NewGuid(),
                Type = request.Type,
                Level = 1,
                IsUnderConstruction = false,
                ConstructionEnd = null,
                StoreId = request.StoreId
            };

            await _sectionRepository.AddAsync(section, cancellationToken);

            return SectionDto.ToDto(section);
        }

        public async Task UpdateSectionAsync(SectionUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var section = new SectionDao
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

            var upgradeCost = (int)(100 * Math.Pow(section.Level, 1.6));
            if (money.Amount < upgradeCost) return false;

            money.Amount -= upgradeCost;
            section.Level++;

            await _resourceRepository.UpdateAsync(money, cancellationToken);
            await _sectionRepository.UpdateAsync(section, cancellationToken);

            return true;
        }
    }
}