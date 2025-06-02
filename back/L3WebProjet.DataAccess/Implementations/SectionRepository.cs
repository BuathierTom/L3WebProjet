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

        public async Task<IEnumerable<SectionDto>> GetAllAsync()
        {
            return await _context.Sections.ToListAsync();
        }

        public async Task<SectionDto?> GetByIdAsync(Guid id)
        {
            return await _context.Sections.FindAsync(id);
        }

        public async Task AddAsync(SectionDto section)
        {
            await _context.Sections.AddAsync(section);
            await _context.SaveChangesAsync();
        }
    }
}