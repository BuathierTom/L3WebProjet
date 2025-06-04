using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace L3WebProjet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetAll()
        {
            var resources = await _resourceService.GetAllResourcesAsync();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetById(Guid id)
        {
            var resource = await _resourceService.GetResourceByIdAsync(id);
            return resource is null ? NotFound() : Ok(resource);
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetByStore(Guid storeId)
        {
            var resources = await _resourceService.GetResourcesByStoreIdAsync(storeId);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceDto resource)
        {
            await _resourceService.CreateResourceAsync(resource);
            return CreatedAtAction(nameof(GetById), new { id = resource.Id }, resource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ResourceDto resource)
        {
            if (id != resource.Id) return BadRequest();
            await _resourceService.UpdateResourceAsync(resource);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _resourceService.DeleteResourceAsync(id);
            return NoContent();
        }
    }
}