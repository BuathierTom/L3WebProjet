using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(StoreDto store)
        {
            await _storeService.CreateStoreAsync(store);
            return CreatedAtAction(nameof(GetById), new { id = store.Id }, store);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, StoreDto store)
        {
            if (id != store.Id) return BadRequest();
            await _storeService.UpdateStoreAsync(store);
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