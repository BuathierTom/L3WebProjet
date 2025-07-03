using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Business.Interfaces;

namespace L3WebProjet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardController(ILeaderboardService leaderboardService)
        {
            _leaderboardService = leaderboardService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _leaderboardService.GetLeaderboardAsync(cancellationToken);
            return Ok(result);
        }
    }
}