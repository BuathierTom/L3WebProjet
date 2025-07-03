using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;

namespace L3WebProjet.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDao>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserDao?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(UserDao user, CancellationToken cancellationToken = default);
        Task UpdateAsync(UserDao user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    }
}