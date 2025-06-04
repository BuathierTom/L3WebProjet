using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess.Implementations
{
    public class StoreRepository : IStoreRepository
    {
        private readonly VideoclubDbContext _context;

        public StoreRepository(VideoclubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoreDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Stores.ToListAsync(cancellationToken);
        }

        public async Task<StoreDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Stores.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<StoreDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Stores
                .Where(s => s.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(StoreDto store, CancellationToken cancellationToken = default)
        {
            await _context.Stores.AddAsync(store, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(StoreDto store, CancellationToken cancellationToken = default)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }


    }
}