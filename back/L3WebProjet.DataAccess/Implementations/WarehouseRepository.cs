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

    public async Task<WarehouseDto?> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken)
    {
        return await _context.Warehouses
            .FirstOrDefaultAsync(w => w.StoreId == storeId, cancellationToken);
    }

    public async Task<WarehouseDto> AddAsync(WarehouseDto dto, CancellationToken cancellationToken)
    {
        if (dto.Id == Guid.Empty)
            dto.Id = Guid.NewGuid();

        _context.Warehouses.Add(dto);
        await _context.SaveChangesAsync(cancellationToken);

        return dto;
    }

    public async Task UpdateAsync(WarehouseDto dto, CancellationToken cancellationToken)
    {
        _context.Warehouses.Update(dto);
        await _context.SaveChangesAsync(cancellationToken);
    }
}