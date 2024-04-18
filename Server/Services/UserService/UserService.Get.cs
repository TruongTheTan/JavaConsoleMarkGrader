using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<GetUserDTO>> GetUserByEmail(string email)
	{
		CustomResponse<GetUserDTO> customResponse = new();
		IdentityUser identityUser = await userManager.FindByEmailAsync(email.Trim());

		if (identityUser == null)
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "User not found by email";
		}
		else
		{
			GetUserDTO getUserDTO = mapper.Map<GetUserDTO>(identityUser);
			getUserDTO.RoleName = (await userManager.GetRolesAsync(identityUser))[0];

			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Message = "User found";
			customResponse.Data = getUserDTO;
		}
		return customResponse;
	}




	public async Task<CustomResponse<GetUserDTO>> GetUserByGuid(Guid Id)
	{
		IdentityUser user = await userManager.FindByIdAsync(Id.ToString().Trim());

		CustomResponse<GetUserDTO> customResponse = new()
		{
			Data = user != null ? mapper.Map<GetUserDTO>(user) : null,
			Message = user == null ? "User not found" : "User found",
			StatusCode = user == null ? ServiceUtilities.NOT_FOUND : ServiceUtilities.OK
		};
		return customResponse;
	}





	public async Task<CustomResponse<List<GetUserDTO>>> GetUserList()
	{

		CustomResponse<List<GetUserDTO>> customResponse = new();
		List<IdentityUser> list = await userManager.Users.ToListAsync();


		if (list != null && list.Count > 0)
		{
			foreach (var user in list)
			{
				GetUserDTO getUserDTO = mapper.Map<GetUserDTO>(user);
				getUserDTO.RoleName = (await userManager.GetRolesAsync(user))[0];
				(customResponse.Data ??= new()).Add(getUserDTO);
			}
			customResponse.StatusCode = ServiceUtilities.OK;
			customResponse.Message = "User list found";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "User list is empty";
		}
		return customResponse;
	}
}

