using L3WebProjet.Common.DTO;

namespace L3WebProjet.Business.Interfaces
{
    public interface ILeaderboardService
    {
        Task<IEnumerable<LeaderboardDto>> GetLeaderboardAsync(CancellationToken cancellationToken = default);
    }
}