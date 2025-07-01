using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace L3WebProjet.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly VideoclubDbContext _context;

        public UserRepository(VideoclubDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDao>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<UserDao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task AddAsync(UserDao user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(UserDao user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }


    }
}