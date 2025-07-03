using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DAO;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;
using L3WebProjet.Common.Request;

namespace L3WebProjet.Business.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IResourceRepository _resourceRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        
        public UserService(IUserRepository userRepository, IStoreRepository storeRepository, IResourceRepository resourceRepository, ISectionRepository sectionRepository, IWarehouseRepository warehouseRepository)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _resourceRepository = resourceRepository;
            _sectionRepository = sectionRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            var daos = await _userRepository.GetAllAsync(cancellationToken);
            return daos.Select(u => u.ToDto());
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dao = await _userRepository.GetByIdAsync(id, cancellationToken);
            return dao?.ToDto();
        }

        public async Task<UserDto> CreateUserAsync(UserCreateRequest request, CancellationToken cancellationToken = default)
        {
            var user = new UserDao
            {
                Id = Guid.NewGuid(),
                Pseudo = request.Pseudo
            };

            await _userRepository.AddAsync(user, cancellationToken);
            return user.ToDto();
        }

        public async Task UpdateUserAsync(UserUpdateRequest request, CancellationToken cancellationToken = default)
        {
            var user = new UserDao
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

        public async Task<UserDto> CreateUserWithStoreAsync(UserWithStoreCreateRequest request, CancellationToken cancellationToken = default)
        {
            var user = new UserDao
            {
                Id = Guid.NewGuid(),
                Pseudo = request.Pseudo
            };

            await _userRepository.AddAsync(user, cancellationToken);

            var store = new StoreDao
            {
                Id = Guid.NewGuid(),
                Name = request.StoreName,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            await _storeRepository.AddAsync(store, cancellationToken);

            var warehouse = new WarehouseDao
            {
                Id = Guid.NewGuid(),
                StoreId = store.Id,
                Level = 1
            };
            await _warehouseRepository.AddAsync(warehouse, cancellationToken);

            var startingResources = new List<ResourceDao>
            {
                new ResourceDao { Id = Guid.NewGuid(), Type = "Money", Amount = 100, StoreId = store.Id }
            };

            foreach (var resource in startingResources)
            {
                await _resourceRepository.AddAsync(resource, cancellationToken);
            }

            var defaultSection = new SectionDao
            {
                Id = Guid.NewGuid(),
                Type = "Comédie",
                Level = 1,
                IsUnderConstruction = false,
                ConstructionEnd = null,
                StoreId = store.Id
            };

            await _sectionRepository.AddAsync(defaultSection, cancellationToken);

            return user.ToDto();
        }

        
    }
}