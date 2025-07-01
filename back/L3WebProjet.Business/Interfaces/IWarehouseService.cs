using L3WebProjet.Common.DTO;

namespace L3WebProjet.Business.Interfaces
{
    public interface IWarehouseService
    {
        Task<WarehouseDto?> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken);
        Task<WarehouseDto> AddAsync(Guid storeId, CancellationToken cancellationToken);
        Task<bool> UpgradeWarehouseAsync(Guid storeId, CancellationToken cancellationToken);
    }

}