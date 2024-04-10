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
		private readonly UserService userService;
		private readonly UserGetService userGetService;
		private readonly UserLoginService userLoginService;
		private readonly UserCreateService userCreateService;
		private readonly UserPasswordService userPasswordService;

		public UserController(UserService userService, UserGetService userGetService, UserLoginService userLoginService, UserCreateService userCreateService, UserPasswordService userPasswordService)
		{
			this.userService = userService;
			this.userGetService = userGetService;
			this.userLoginService = userLoginService;
			this.userCreateService = userCreateService;
			this.userPasswordService = userPasswordService;
		}

		[HttpPost("basic-login")]
		public async Task<IActionResult> BasicLogin([FromBody] UserLoginDTO user)
		{

			AuthenticationUser? authenticationUser = await this.userLoginService.BasicLogin(user);


			if (authenticationUser == null)
				return NotFound("User not found");

			/*
			HttpContext.Response.Cookies.Append("token", authenticationUser.Token!,
				new CookieOptions()
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.None,
					//Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JWT:Expiration"]))
				});
			*/
			return Ok(authenticationUser);
		}








		[HttpPost("google-login")]
		public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDTO)
		{

			AuthenticationUser? authenticationUser = await this.userLoginService.GoogleLogin(googleLoginDTO);

			if (authenticationUser == null)
				return NotFound("User not found");

			return Ok(authenticationUser);
		}





		[HttpGet("list")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUserList()
		{
			List<GetUserDTO> list = await userGetService.GetUserList();
			return Ok(list);
		}





		[HttpPost("create")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateNewUser([FromBody] CreateUserDTO createUserDTO)
		{
			bool createdSuccessfull = await userCreateService.CreateUserByEmail(createUserDTO);

			if (createdSuccessfull)
				return Created(nameof(CreateNewUser), createUserDTO);

			else
				return Problem("Create new user failed");
		}





		[HttpPatch("change-password")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDTO changeUserPasswordDTO)
		{

			bool updateSuccessfull = await userPasswordService.ChangeUserPassword(changeUserPasswordDTO);

			if (updateSuccessfull)
				return Ok("Change password successfully");

			return Problem("Change password failed");
		}






		[HttpPatch("reset-password")]
		public async Task<IActionResult> ResetPassword([FromBody] string email)
		{

			bool resetSuccessfull = await userPasswordService.ResetPassword(email);

			if (resetSuccessfull)
				return Ok("Reset password successfully");

			return Problem("Reset password failed");
		}





		[HttpPatch("confirm-email")]
		public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmEmail userConfirmEmail)
		{

			bool emailConfirmedSucess = await userService.ConfirmEmail(userConfirmEmail);

			if (emailConfirmedSucess)
				return Ok("Email confirmed successfully");

			return Problem("Fail to confirm email");
		}
	}



}
