using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess.Implementations
{
    public class SectionRepository : ISectionRepository
    {
        private readonly VideoclubDbContext _context;

        public SectionRepository(VideoclubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SectionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Sections.ToListAsync(cancellationToken);
        }

        public async Task<SectionDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sections.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<SectionDto>> GetByStoreIdAsync(Guid storeId, CancellationToken cancellationToken = default)
        {
            return await _context.Sections
                .Where(s => s.StoreId == storeId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(SectionDto section, CancellationToken cancellationToken = default)
        {
            await _context.Sections.AddAsync(section, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(SectionDto section, CancellationToken cancellationToken = default)
        {
            _context.Sections.Update(section);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var section = await _context.Sections.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
            if (section != null)
            {
                _context.Sections.Remove(section);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }


    }
}