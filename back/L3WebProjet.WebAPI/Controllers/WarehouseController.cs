using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Business.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using L3WebProjet.Common.DTO;

namespace L3WebProjet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }
        
        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId, CancellationToken cancellationToken)
        {
            var result = await _warehouseService.GetByStoreIdAsync(storeId, cancellationToken);
            return result is null ? NotFound("Entrepôt non trouvé") : Ok(result);
        }
        
        [HttpPost("upgrade/{storeId}")]
        public async Task<IActionResult> Upgrade(Guid storeId, CancellationToken cancellationToken)
        {
            var success = await _warehouseService.UpgradeWarehouseAsync(storeId, cancellationToken);
            return success ? Ok("Entrepôt amélioré") : BadRequest("Entrepôt introuvable ou niveau maximum atteint ou argent insuffisant");
        }
        
        [HttpGet("capacity/{storeId}")]
        public async Task<IActionResult> GetCapacity(Guid storeId, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseService.GetByStoreIdAsync(storeId, cancellationToken);
            return warehouse is null
                ? NotFound("Entrepôt non trouvé")
                : Ok(new { capacity = warehouse.Capacity });
        }
    }
}
