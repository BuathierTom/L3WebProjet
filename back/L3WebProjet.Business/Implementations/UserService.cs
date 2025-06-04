using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;

namespace L3WebProjet.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(UserDto user)
        {
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(UserDto user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}