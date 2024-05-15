﻿using MarkGrader.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.UserService;

namespace MarkGrader.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

	private readonly UserService userService;


	public UserController(UserService userService)
	{
		this.userService = userService;
	}





	[HttpGet("list")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetUserList()
	{
		CustomResponse<List<GetUserDTO>> listUser = await userService.GetUserList();
		return HttpsUtility.ReturnActionResult(listUser);
	}




	[HttpGet("{id:Guid}")]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetUserById(Guid id)
	{
		CustomResponse<GetUserDTO> userFound = await userService.GetUserByGuid(id);
		return HttpsUtility.ReturnActionResult(userFound);
	}




	[HttpGet("{email}")]
	//[Authorize(Roles = "Admin")]
	public async Task<IActionResult> GetUserByEmail(string email)
	{
		CustomResponse<GetUserDTO> userFound = await userService.GetUserByEmail(email);
		return HttpsUtility.ReturnActionResult(userFound);
	}




	[HttpPost("create")]
	//[ValidateAntiForgeryToken]
	[Authorize(Roles = "Admin")]
	public async Task<IActionResult> CreateNewUser([FromBody] CreateUserDTO createUserDTO)
	{
		CustomResponse<dynamic> customResponse = await userService.CreateUserByEmail(createUserDTO);
		return HttpsUtility.ReturnActionResult(customResponse);
	}





	[HttpPatch("change-password")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordDTO changeUserPasswordDTO)
	{
		CustomResponse<dynamic> customResponse = await userService.ChangeUserPassword(changeUserPasswordDTO);
		return HttpsUtility.ReturnActionResult(customResponse);
	}






	[HttpPatch("reset-password")]
	public async Task<IActionResult> ResetPassword([FromBody] string email)
	{
		CustomResponse<dynamic> customResponse = await userService.ResetPassword(email);
		return HttpsUtility.ReturnActionResult(customResponse);
	}





	[HttpPatch("confirm-email")]
	public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmEmail userConfirmEmail)
	{
		CustomResponse<dynamic> customResponse = await userService.ConfirmEmail(userConfirmEmail);
		return HttpsUtility.ReturnActionResult(customResponse);
	}
}



