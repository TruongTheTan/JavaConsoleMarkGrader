using Microsoft.AspNetCore.Identity;
using Repositories.DTOs;

namespace Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<dynamic>> ChangeUserPassword(ChangeUserPasswordDTO changeUserPasswordDTO)
	{

		CustomResponse<dynamic> customResponse = new();
		var user = await userManager.FindByEmailAsync(changeUserPasswordDTO.Email);

		if (user != null)
		{
			var result = await userManager.ChangePasswordAsync(user, changeUserPasswordDTO.OldPassword, changeUserPasswordDTO.NewPassword);

			customResponse.StatusCode = result.Succeeded ? ServiceUtilities.OK : ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = result.Succeeded ? "Password has been changed successfully" : "Fail to change password";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "User not found by email";
		}
		return customResponse;
	}



	public async Task<CustomResponse<dynamic>> ResetPassword(string email)
	{
		CustomResponse<dynamic> customResponse = new();
		IdentityUser user = await userManager.FindByEmailAsync(email);

		if (user != null)
		{
			string defaultPassword = configuration.GetSection("DefaultPassword").Value!;

			bool isRemoveOldPasswordSuccessful = (await userManager.RemovePasswordAsync(user)).Succeeded;
			bool isAddNewPasswordSuccessful = (await userManager.AddPasswordAsync(user, defaultPassword)).Succeeded;


			bool isResetPasswordSuccessful = isRemoveOldPasswordSuccessful && isAddNewPasswordSuccessful;

			customResponse.StatusCode = isResetPasswordSuccessful ? ServiceUtilities.OK : ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = isResetPasswordSuccessful ? "Password reset successfully" : "Failed to reset password";
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "User not found by email";
		}
		return customResponse;
	}
}

