using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task CreateUserAsync(UserCreateRequest request);
        Task UpdateUserAsync(UserUpdateRequest request);
        Task DeleteUserAsync(Guid id);
    }
}