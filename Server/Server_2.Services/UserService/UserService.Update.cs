using System.Net;
using Repositories.DTOs;
using Server.DAL.Entities;
using Services;

namespace Server_2.Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<dynamic>> ConfirmEmail(UserConfirmEmail userConfirmEmail)
	{
		AppUser identityUser = await userManager.FindByEmailAsync(userConfirmEmail.Email.Trim());
		bool isConfirmSuccess = (await userManager.ConfirmEmailAsync(identityUser, userConfirmEmail.Token)).Succeeded;


		CustomResponse<dynamic> customResponse = new();


		if (isConfirmSuccess)
		{
			const string emailTitle = "Confirm email successfully";
			const string emailBody = "Your email has been confirmed successfully, now you can login";

			bool isSendEmailSuccess = await ServiceUtility.SendEmailAsync(emailTitle, emailBody, userConfirmEmail.Email);

			if (isSendEmailSuccess)
			{
				customResponse.StatusCode = (int)HttpStatusCode.OK;
				customResponse.Message = "Email confirm has been sent to your mail box, please check";
			}
		}
		else
		{
			customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
			customResponse.Message = "Fail to confirm your email";
		}
		return customResponse;
	}





	public async Task<CustomResponse<dynamic>> ActivateUser(string email)
	{
		CustomResponse<dynamic> customResponse = new();


		AppUser identityUser = await userManager.FindByEmailAsync(email.Trim());
		identityUser.IsActive = true;


		bool activateSuccessful = (await userManager.UpdateAsync(identityUser)).Succeeded;


		if (activateSuccessful)
		{
			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Message = "Activate successfully";
		}
		else
		{
			customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
			customResponse.Message = "Fail to active user";
		}
		return customResponse;
	}







	public async Task<CustomResponse<dynamic>> DeActivateUser(string email)
	{
		CustomResponse<dynamic> customResponse = new();


		AppUser identityUser = await userManager.FindByEmailAsync(email.Trim());
		identityUser.IsActive = false;


		bool deActivateSuccessful = (await userManager.UpdateAsync(identityUser)).Succeeded;


		if (deActivateSuccessful)
		{
			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Message = "De-activate successfully";
		}
		else
		{
			customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
			customResponse.Message = "Fail to de-active user";
		}
		return customResponse;
	}
}
