using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetAllAsync(cancellationToken);
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _userRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<UserDto> CreateUserAsync(UserCreateRequest request, CancellationToken cancellationToken = default)
        {
            var user = new UserDto
            {
                Id = Guid.NewGuid(),
                Pseudo = request.Pseudo
            };

            await _userRepository.AddAsync(user, cancellationToken);
            return user;
        }

        public async Task UpdateUserAsync(UserUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var user = new UserDto
            {
                Id = request.Id,
                Pseudo = request.Pseudo
            };

            await _userRepository.UpdateAsync(user, cancellationToken);
        }

        public async Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);
        }

    }
}