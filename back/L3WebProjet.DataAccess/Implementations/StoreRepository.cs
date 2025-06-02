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

        public async Task<IEnumerable<StoreDto>> GetAllAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<StoreDto?> GetByIdAsync(Guid id)
        {
            return await _context.Stores.FindAsync(id);
        }

        public async Task AddAsync(StoreDto store)
        {
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
        }
    }
}