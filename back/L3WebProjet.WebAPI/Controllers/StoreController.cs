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
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetAll()
        {
            var stores = await _storeService.GetAllStoresAsync();
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDto>> GetById(Guid id)
        {
            var store = await _storeService.GetStoreByIdAsync(id);
            return store is null ? NotFound() : Ok(store);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<StoreDto>>> GetByUser(Guid userId)
        {
            var stores = await _storeService.GetStoresByUserIdAsync(userId);
            return Ok(stores);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreCreateRequest request)
        {
            var store = await _storeService.CreateStoreAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = store.Id }, store);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, StoreUpdateRequest request)
        {
            if (id != request.Id) return BadRequest("ID mismatch between URL and body");
            await _storeService.UpdateStoreAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _storeService.DeleteStoreAsync(id);
            return NoContent();
        }
    }
}