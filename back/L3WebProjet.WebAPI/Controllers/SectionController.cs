using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Create(SectionDto section)
        {
            await _sectionService.CreateSectionAsync(section);
            return CreatedAtAction(nameof(GetById), new { id = section.Id }, section);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SectionDto section)
        {
            if (id != section.Id) return BadRequest();
            await _sectionService.UpdateSectionAsync(section);
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