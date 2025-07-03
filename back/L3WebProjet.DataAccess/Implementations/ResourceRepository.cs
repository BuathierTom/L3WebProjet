using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess.Implementations
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly VideoclubDbContext _context;

        public ResourceRepository(VideoclubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ResourceDao>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Resources.ToListAsync(cancellationToken);
        }

        public async Task<ResourceDao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Resources.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<ResourceDao>> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            return await _context.Resources
                .Where(r => r.StoreId == storeId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ResourceDao resource, CancellationToken cancellationToken = default)
        {
            await _context.Resources.AddAsync(resource, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(ResourceDao resource, CancellationToken cancellationToken = default)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var resource = await _context.Resources.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }


    }
}