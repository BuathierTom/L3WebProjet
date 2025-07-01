using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces;

public interface IWarehouseRepository
{
    Task<WarehouseDto?> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken);
    Task<WarehouseDto> AddAsync(WarehouseDto dto, CancellationToken cancellationToken);
    Task UpdateAsync(WarehouseDto dto, CancellationToken cancellationToken);
}
