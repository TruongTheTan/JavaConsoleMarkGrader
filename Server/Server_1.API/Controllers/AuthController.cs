using MarkGrader.Helpers;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Server_2.Services.UserService;


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
    }



    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDTO googleLoginDTO)
    {
        CustomResponse<AuthenticationUser> customResponse = await userService.GoogleLogin(googleLoginDTO);
        return HttpsUtility.ReturnActionResult(customResponse);
    }

}
