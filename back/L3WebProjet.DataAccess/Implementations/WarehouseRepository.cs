using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess.Implementations;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly VideoclubDbContext _context;

    public WarehouseRepository(VideoclubDbContext context)
    {
        _context = context;
    }

    public async Task<WarehouseDao?> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken)
    {
        return await _context.Warehouses
            .FirstOrDefaultAsync(w => w.StoreId == storeId, cancellationToken);
    }

    public async Task<WarehouseDao> AddAsync(WarehouseDao dao, CancellationToken cancellationToken)
    {
        if (dao.Id == Guid.Empty)
            dao.Id = Guid.NewGuid();

        _context.Warehouses.Add(dao);
        await _context.SaveChangesAsync(cancellationToken);

        return dao;
    }

    public async Task UpdateAsync(WarehouseDao dao, CancellationToken cancellationToken)
    {
        _context.Warehouses.Update(dao);
        await _context.SaveChangesAsync(cancellationToken);
    }
}