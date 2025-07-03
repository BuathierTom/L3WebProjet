using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces;

public interface IWarehouseRepository
{
    Task<WarehouseDao?> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken);
    Task<WarehouseDao> AddAsync(WarehouseDao dto, CancellationToken cancellationToken);
    Task UpdateAsync(WarehouseDao dto, CancellationToken cancellationToken);
}
