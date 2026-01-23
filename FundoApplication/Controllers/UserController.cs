using BusinessLogicLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Dto;

namespace FundoApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UsersController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserSignUpRequestDto userDto)
        {
            try
            {
                var result = await userBL.RegisterUserAsync(userDto);

                var response = new
                {
                    userId = result.UserId,
                    email = result.Email,
                    message = "User registered successfully"
                };

                return StatusCode(201, new ApiResponseDto<object>(true, "Success", response));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<string>(false, ex.Message, ex.Message));
            }
        }

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var user = await userBL.LoginUserAsync(loginDto.Email, loginDto.Password);

                var response = new
                {
                    userId = user.UserId,
                    email = user.Email,
                    message = "Login successful"
                };

                return Ok(new ApiResponseDto<object>(true, "Success", response));
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiResponseDto<string>(false, ex.Message, ex.Message));
            }
        }

        // PUT: api/users/update/1
        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserSignUpRequestDto userDto)
        {
            try
            {
                var updatedUser = await userBL.UpdateUserAsync(userId, userDto);

                return Ok(new ApiResponseDto<UserResponseDto>(
                    true,
                    "User updated successfully",
                    updatedUser
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<string>(false, ex.Message, ex.Message));
            }
        }

        // GET: api/users/1
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await userBL.GetUserByIdAsync(userId);

                return Ok(new ApiResponseDto<UserResponseDto>(
                    true,
                    "User retrieved successfully",
                    user
                ));
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponseDto<string>(false, ex.Message, ex.Message));
            }
        }

        // POST: api/users/change-password/1
        [HttpPost("change-password/{userId}")]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] ChangePasswordDto dto)
        {
            try
            {
                bool result = await userBL.ChangePasswordAsync(userId, dto.OldPassword, dto.NewPassword);

                return Ok(new ApiResponseDto<object>(
                    true,
                    "Password changed successfully",
                    null
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<string>(false, ex.Message, ex.Message));
            }
        }

        // POST: api/users/forget-password
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordDto dto)
        {
            try
            {
                bool result = await userBL.ForgetPasswordAsync(dto.Email, dto.NewPassword);

                return Ok(new ApiResponseDto<object>(
                    true,
                    "Password reset successfully",
                    null
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponseDto<string>(false, ex.Message, ex.Message));
            }
        }
    }
}
