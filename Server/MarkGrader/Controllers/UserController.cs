using Microsoft.AspNetCore.Identity;
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
		private readonly SignInManager<IdentityUser> _signInManager;


		public UserController(IUserService userService, SignInManager<IdentityUser> _signInManager)
		{
			this._signInManager = _signInManager;
			this.userService = userService;
		}



		[HttpPost("basic-login")]
		public async Task<IActionResult> BasicLogin([FromBody] UserLoginDTO user)
		{

			GetUserDTO? userDTO = await this.userService.Login(user);


			if (userDTO == null)
				return NotFound("User not found");


			HttpContext.Response.Cookies.Append("token", userDTO.Token!,
				new CookieOptions()
				{
					HttpOnly = true,
					Secure = true,
					SameSite = SameSiteMode.None,
					//Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["JWT:Expiration"]))
				});

			return Ok(userDTO);
		}







		/*
		[HttpPost("google-login")]
		public async Task<IActionResult> GoogleLogin([FromBody] string googleIdToken)
		{

			try
			{
				var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken.Trim());


				// The token is invalid or unable to decode
				if (payload == null)
					return Problem();

				UserDTO userDTO = await this.userService.GetUserByEmail(payload.Email.Trim());
				return Ok(userDTO);
			}
			catch (InvalidJwtException ex)
			{
				return Problem(ex.ToString());

			}
		}
		*/



		[HttpPost("create")]
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

			if (changeUserPasswordDTO.OldPassword != changeUserPasswordDTO.NewPassword)
				return BadRequest("Password and confirm password are not equal");

			bool updateSuccessfull = await userService.ChangeUserPassword(changeUserPasswordDTO);

			if (updateSuccessfull)
				return Ok("Change password successfully");

			return Problem();
		}
	}
}
