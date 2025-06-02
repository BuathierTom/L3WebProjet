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

        public async Task<IEnumerable<ResourceDto>> GetAllAsync()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<ResourceDto?> GetByIdAsync(Guid id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task AddAsync(ResourceDto resource)
        {
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
        }
    }
}