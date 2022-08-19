using Microsoft.AspNetCore.Mvc;
using PAY_UP.Application.Abstracts.Services;
using PAY_UP.Application.Dtos.Users;
using PAY_UP.Common.Helpers;

namespace PAY_UP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetUsers)), ProducesResponseType(typeof(ResponseObject<GetUserDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> GetUsers(bool isActive = false)
        {
            return Ok(await _userService.GetAllAsync(isActive));
        }

        /// <summary>
        /// creates a new user
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(CreateUser)), ProducesResponseType(typeof(ResponseObject<GetUserDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto model)
        {
            return Ok(await _userService.CreateAsync(model, "user"));
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("updateuser", Name = nameof(UpdateUser)), ProducesResponseType(typeof(ResponseObject<GetUserDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto model)
        {
            return Ok(await _userService.UpdateAsync(model));
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = nameof(GetUserById)), ProducesResponseType(typeof(ResponseObject<GetUserDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            return Ok(await _userService.GetByIdAsync(id));
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("/{email}", Name = nameof(GetUserByEmail)), ProducesResponseType(typeof(ResponseObject<GetUserDto>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            return Ok(await _userService.GetByEmailAsync(email));
        }

        [HttpDelete("/{id}", Name = nameof(DeleteUser)), ProducesResponseType(typeof(ResponseObject<bool>), StatusCodes.Status201Created), ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            return Ok(await _userService.DeleteUserAsync(id));
        }

    }
}
