﻿using System.Net;
using Repositories.DTOs;
using Server.DAL.Entities;

namespace Server_2.Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<dynamic>> ChangeUserPassword(ChangeUserPasswordDTO changeUserPasswordDTO)
	{

		CustomResponse<dynamic> customResponse = new();
		var user = await userManager.FindByEmailAsync(changeUserPasswordDTO.Email);

		if (user == null)
		{
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "User not found by email";
		}
		else
		{
			var result = await userManager.ChangePasswordAsync(user, changeUserPasswordDTO.OldPassword, changeUserPasswordDTO.NewPassword);

			customResponse.StatusCode = result.Succeeded ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError;
			customResponse.Message = result.Succeeded ? "Password has been changed successfully" : "Fail to change password";
		}
		return customResponse;
	}



	public async Task<CustomResponse<dynamic>> ResetPassword(string email)
	{
		CustomResponse<dynamic> customResponse = new();
		AppUser user = await userManager.FindByEmailAsync(email);


		if (user is null)
		{
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "User not found by email";
			return customResponse;
		}


		string defaultPassword = configuration.GetSection("DefaultPassword").Value!;
		bool isRemoveOldPasswordSuccessful = false, isAddNewPasswordSuccessful = false;

		using var transaction = await unitOfWork.Context.Database.BeginTransactionAsync();

		try
		{
			isRemoveOldPasswordSuccessful = (await userManager.RemovePasswordAsync(user)).Succeeded;
			isAddNewPasswordSuccessful = (await userManager.AddPasswordAsync(user, defaultPassword)).Succeeded;

			await transaction.CommitAsync();
		}
		catch (Exception ex)
		{
			await transaction.RollbackAsync();
			customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
			customResponse.Message = ex.Message;
		}
		finally
		{
			await transaction.DisposeAsync();
		}

		bool isResetPasswordSuccessful = isRemoveOldPasswordSuccessful && isAddNewPasswordSuccessful;

		customResponse.StatusCode = isResetPasswordSuccessful ? (int)HttpStatusCode.OK : (int)HttpStatusCode.InternalServerError;
		customResponse.Message = isResetPasswordSuccessful ? "Password reset successfully" : "Failed to reset password";

		return customResponse;
	}
}

