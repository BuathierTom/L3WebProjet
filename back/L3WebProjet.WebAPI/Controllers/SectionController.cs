using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Common.Request;

namespace L3WebProjet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var sections = await _sectionService.GetAllSectionsAsync(cancellationToken);
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var section = await _sectionService.GetSectionByIdAsync(id, cancellationToken);
            return section is null ? NotFound() : Ok(section);
        }

        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetByStoreId(Guid storeId, CancellationToken cancellationToken)
        {
            var sections = await _sectionService.GetSectionsByStoreIdAsync(storeId, cancellationToken);
            return Ok(sections);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SectionCreateRequest request, CancellationToken cancellationToken)
        {
            var section = await _sectionService.CreateSectionAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = section.Id }, section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SectionUpdateRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
                return BadRequest("ID mismatch");

            await _sectionService.UpdateSectionAsync(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _sectionService.DeleteSectionAsync(id, cancellationToken);
            return NoContent();
        }
    }
}