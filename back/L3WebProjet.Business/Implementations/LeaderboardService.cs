using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using L3WebProjet.DataAccess.Interfaces;

namespace L3WebProjet.Business.Implementations
{
    public class LeaderboardService : ILeaderboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IWarehouseRepository _warehouseRepository;

        public LeaderboardService(
            IUserRepository userRepository,
            IStoreRepository storeRepository,
            ISectionRepository sectionRepository,
            IWarehouseRepository warehouseRepository)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _sectionRepository = sectionRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<LeaderboardDto>> GetLeaderboardAsync(CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            var leaderboard = new List<LeaderboardDto>();

            foreach (var user in users)
            {
                var stores = await _storeRepository.GetByUserIdAsync(user.Id, cancellationToken);

                foreach (var store in stores)
                {
                    var sections = await _sectionRepository.GetByStoreIdAsync(store.Id, cancellationToken);
                    var warehouse = await _warehouseRepository.GetByStoreIdAsync(store.Id, cancellationToken);

                    var sectionScore = sections.Sum(s => s.Level);
                    var warehouseScore = warehouse?.Level ?? 0;

                    leaderboard.Add(new LeaderboardDto
                    {
                        UserId = user.Id,
                        Pseudo = user.Pseudo,
                        Score = sectionScore + warehouseScore
                    });
                }
            }

            return leaderboard.OrderByDescending(l => l.Score).ToList();
        }
    }
}