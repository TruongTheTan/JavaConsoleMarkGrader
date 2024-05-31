using System.Net;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;
using Server.DAL.Entities;

namespace Server_2.Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<GetUserDTO>> GetUserByEmail(string email)
	{
		CustomResponse<GetUserDTO> customResponse = new();
		AppUser identityUser = await userManager.FindByEmailAsync(email.Trim());

		if (identityUser == null)
		{
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "User not found by email";
		}
		else
		{
			GetUserDTO getUserDTO = mapper.Map<GetUserDTO>(identityUser);
			getUserDTO.RoleName = (await userManager.GetRolesAsync(identityUser))[0];

			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Message = "User found";
			customResponse.Data = getUserDTO;
		}
		return customResponse;
	}




	public async Task<CustomResponse<GetUserDTO>> GetUserByGuid(Guid Id)
	{
		AppUser user = await userManager.FindByIdAsync(Id.ToString().Trim());

		bool isUserNotNull = user != null;

		return new CustomResponse<GetUserDTO>()
		{
			Data = isUserNotNull ? mapper.Map<GetUserDTO>(user) : null,
			Message = isUserNotNull ? "User found" : "User not found",
			StatusCode = isUserNotNull ? (int)HttpStatusCode.OK : (int)HttpStatusCode.NotFound
		};
	}





	public async Task<CustomResponse<List<GetUserDTO>>> GetUserList()
	{

		CustomResponse<List<GetUserDTO>> customResponse = new();
		List<AppUser> list = await userManager.Users.ToListAsync();



		if (list == null && list.Count == 0)
		{
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "User list is empty";
		}
		else
		{
			customResponse.Data = new List<GetUserDTO>();

			foreach (var user in list)
			{
				GetUserDTO getUserDTO = mapper.Map<GetUserDTO>(user);
				getUserDTO.RoleName = (await userManager.GetRolesAsync(user))[0];
				customResponse.Data.Add(getUserDTO);
			}
			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Message = "User list found";
		}

		return customResponse;
	}
}

