using L3WebProjet.Business.Interfaces;
using L3WebProjet.Common.DTO;
using Microsoft.AspNetCore.Mvc;
using L3WebProjet.Common.Request;

namespace L3WebProjet.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            var user = new UserDto { Id = Guid.NewGuid(), Pseudo = request.Pseudo };
            await _userService.CreateUserAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UserUpdateRequest request)
        {
            if (id != request.Id) return BadRequest("ID mismatch between URL and body");
            var user = new UserDto { Id = request.Id, Pseudo = request.Pseudo };
            await _userService.UpdateUserAsync(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}