using backend_dotnet.Dtos.User;
using backend_dotnet.Interfaces;
using backend_dotnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly IUnitOfWork _unitOfWork;

        public UserController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = userDto.Username,
                Email = userDto.Email,
            };

            var existingEmail = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingEmail != null)
                return BadRequest(new { Code = "DuplicateEmail", Description = "Email is already taken." });


            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _unitOfWork.SaveChangesAsync();


            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto userDto)
        // {
        //     var existingUser = await _unitOfWork.Users.GetByIdAsync(id);

        //     if (existingUser == null)
        //         return NotFound($"User with ID {id} not found");

        //     existingUser.Username = userDto.Username;
        //     existingUser.Email = userDto.Email;
        //     // Add other properties as needed

        //     _unitOfWork.Users.Update(existingUser);
        //     await _unitOfWork.SaveChangesAsync();

        //     return NoContent();
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null)
                return NotFound($"User with ID {id} not found");

            _unitOfWork.Users.Remove(user);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}
