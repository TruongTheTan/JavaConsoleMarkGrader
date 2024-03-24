using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.UserService;

namespace MarkGrader.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{

		private readonly IUserService userService;



		public UserController(IUserService userService)
		{
			this.userService = userService;
		}





		[HttpPost("basic-login")]
		public async Task<IActionResult> BasicLogin([FromBody] UserLoginDTO user)
		{

			AuthenticationUser? authenticationUser = await this.userService.Login(user);


			if (authenticationUser == null)
				return NotFound("User not found");


			HttpContext.Response.Cookies.Append("token", authenticationUser.Token!,
				new CookieOptions()
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.None,
					//Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JWT:Expiration"]))
				});

			return Ok(authenticationUser);
		}








		[HttpPost("google-login")]
		public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDTO)
		{

			AuthenticationUser? authenticationUser = await this.userService.GoogleLogin(googleLoginDTO);

			if (authenticationUser == null)
				return NotFound("User not found");

			return Ok(authenticationUser);
		}





		[HttpGet("list")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserList()
		{
			List<GetUserDTO> list = await userService.GetUserList();
			return Ok(list);
		}



		[HttpPost("create")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateNewUser([FromBody] CreateUserDTO createUserDTO)
		{
			bool createdSuccessfull = await userService.AddNewUser(createUserDTO);

			if (createdSuccessfull)
				return Created(nameof(CreateNewUser), createUserDTO);

			else
				return Problem();
		}





		[HttpPatch("change-password")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDTO changeUserPasswordDTO)
		{

			bool updateSuccessfull = await userService.ChangeUserPassword(changeUserPasswordDTO);

			if (updateSuccessfull)
				return Ok("Change password successfully");

			return Problem();
		}





		[HttpPatch("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] string email)
		{

			bool resetSuccessfull = await userService.ResetPassword(email);

			if (resetSuccessfull)
				return Ok("Reset password successfully");

			return Problem();
		}
	}
}
