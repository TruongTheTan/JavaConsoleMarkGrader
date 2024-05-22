using MarkGrader.Helpers;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.UserService;


namespace MarkGrader.Controllers;



[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

	private readonly UserService userService;


	public AuthController(UserService userService)
	{
		this.userService = userService;
	}



	[HttpPost("basic-login")]
	public async Task<IActionResult> BasicLogin([FromBody] UserLoginDTO user)
	{
		CustomResponse<AuthenticationUser> customResponse = await userService.BasicLogin(user);
		return HttpsUtility.ReturnActionResult(customResponse);
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
	}





	[HttpPost("google-login")]
	public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDTO)
	{
		CustomResponse<AuthenticationUser> customResponse = await userService.GoogleLogin(googleLoginDTO);
		return HttpsUtility.ReturnActionResult(customResponse);
	}

}
