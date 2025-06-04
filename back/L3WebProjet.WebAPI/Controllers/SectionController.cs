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
        public async Task<ActionResult<IEnumerable<SectionDto>>> GetAll()
        {
            var sections = await _sectionService.GetAllSectionsAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SectionDto>> GetById(Guid id)
        {
            var section = await _sectionService.GetSectionByIdAsync(id);
            return section is null ? NotFound() : Ok(section);
        }

        [HttpGet("store/{storeId}")]
        public async Task<ActionResult<IEnumerable<SectionDto>>> GetByStore(Guid storeId)
        {
            var sections = await _sectionService.GetSectionsByStoreIdAsync(storeId);
            return Ok(sections);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SectionCreateRequest request)
        {
            var section = await _sectionService.CreateSectionAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = section.Id }, section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SectionUpdateRequest request)
        {
            if (id != request.Id) return BadRequest("ID mismatch between URL and body");
            await _sectionService.UpdateSectionAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _sectionService.DeleteSectionAsync(id);
            return NoContent();
        }
    }
}