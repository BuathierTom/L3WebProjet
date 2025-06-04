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

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(UserCreateRequest request)
        {
            var dto = new UserDto { Id = Guid.NewGuid(), Pseudo = request.Pseudo };
            await _userRepository.AddAsync(dto);
        }

        public async Task UpdateUserAsync(UserUpdateRequest request)
        {
            var dto = new UserDto { Id = request.Id, Pseudo = request.Pseudo };
            await _userRepository.UpdateAsync(dto);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }

    }
}