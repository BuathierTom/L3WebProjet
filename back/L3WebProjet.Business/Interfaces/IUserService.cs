using L3WebProjet.Common.DTO;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);
        Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UserDto> CreateUserAsync(UserCreateRequest request, CancellationToken cancellationToken = default);
        Task UpdateUserAsync(UserUpdateRequest request, CancellationToken cancellationToken = default);
        Task DeleteUserAsync(Guid id, CancellationToken cancellationToken = default);
        Task<UserDto> CreateUserWithStoreAsync(UserWithStoreCreateRequest request, CancellationToken cancellationToken = default);
    }
}