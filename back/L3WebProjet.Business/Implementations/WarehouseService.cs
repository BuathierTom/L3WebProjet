using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;

namespace L3WebProjet.Business.Implementations
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IResourceRepository _resourceRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository, IResourceRepository resourceRepository)
        {
            _warehouseRepository = warehouseRepository;
            _resourceRepository = resourceRepository;
        }

        public async Task<WarehouseDto?> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken)
        {
            return await _warehouseRepository.GetByStoreIdAsync(storeId, cancellationToken);
        }

        public async Task<WarehouseDto> AddAsync(Guid storeId, CancellationToken cancellationToken)
        {
            var dto = new WarehouseDto
            {
                StoreId = storeId,
                Level = 1
            };

            return await _warehouseRepository.AddAsync(dto, cancellationToken);
        }

        public async Task<bool> UpgradeWarehouseAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            var warehouse = await _warehouseRepository.GetByStoreIdAsync(storeId, cancellationToken);
            if (warehouse == null) return false;

            const int maxLevel = 100;
            if (warehouse.Level >= maxLevel)
                return false;

            var resources = await _resourceRepository.GetByStoreIdAsync(storeId, cancellationToken);
            var money = resources.FirstOrDefault(r => r.Type == "Money");
            if (money == null) return false;

            var nextLevel = warehouse.Level + 1;
            var nextCapacity = nextLevel * 10000;
            var upgradeCost = (int)(nextCapacity * 0.2); // 20% du cap du niveau cible

            if (money.Amount < upgradeCost)
                return false;

            money.Amount -= upgradeCost;
            warehouse.Level++;

            await _resourceRepository.UpdateAsync(money, cancellationToken);
            await _warehouseRepository.UpdateAsync(warehouse, cancellationToken);

            return true;
        }


    }
}