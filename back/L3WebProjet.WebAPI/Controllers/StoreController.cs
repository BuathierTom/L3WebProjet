using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Common.Request;

namespace L3WebProjet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var stores = await _storeService.GetAllStoresAsync(cancellationToken);
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var store = await _storeService.GetStoreByIdAsync(id, cancellationToken);
            return store is null ? NotFound() : Ok(store);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId, CancellationToken cancellationToken)
        {
            var stores = await _storeService.GetStoresByUserIdAsync(userId, cancellationToken);
            return Ok(stores);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreCreateRequest request, CancellationToken cancellationToken)
        {
            var createdStore = await _storeService.CreateStoreAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdStore.Id }, createdStore);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, StoreUpdateRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            await _storeService.UpdateStoreAsync(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _storeService.DeleteStoreAsync(id, cancellationToken);
            return NoContent();
        }

    }
}