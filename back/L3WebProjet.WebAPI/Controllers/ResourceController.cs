using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Common.Request;

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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var resources = await _resourceService.GetAllResourcesAsync(cancellationToken);
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var resource = await _resourceService.GetResourceByIdAsync(id, cancellationToken);
            return resource is null ? NotFound() : Ok(resource);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResourceCreateRequest request, CancellationToken cancellationToken)
        {
            var created = await _resourceService.CreateResourceAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ResourceUpdateRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            await _resourceService.UpdateResourceAsync(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _resourceService.DeleteResourceAsync(id, cancellationToken);
            return NoContent();
        }
        
        [HttpPost("calculate/{storeId}")]
        public async Task<IActionResult> Calculate(Guid storeId, CancellationToken cancellationToken)
        {
            var result = await _resourceService.CalculateMoneyAsync(storeId, cancellationToken);

            if (result < 0)
                return BadRequest("Store or resource not found");

            return Ok(new { total = result });
        }
    }
}