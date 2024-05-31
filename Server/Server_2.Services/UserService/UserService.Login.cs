using System.Net;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Repositories.DTOs;
using Server.DAL.Entities;
using Services;

namespace Server_2.Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<AuthenticationUser>> BasicLogin(UserLoginDTO userLoginDTO)
	{
		CustomResponse<AuthenticationUser> customResponse = new();
		AppUser? user = await userManager.FindByEmailAsync(userLoginDTO.Email.Trim());


		if (user is null)
		{
			customResponse.StatusCode = (int)HttpStatusCode.NotFound;
			customResponse.Message = "User (email) not found, please check again";
			return customResponse;
		}


		if (user.IsActive is false)
		{
			customResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
			customResponse.Message = "Your account is not active to access";
			return customResponse;
		}



		bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
		if (isEmailConfirmed == false)
		{
			customResponse.StatusCode = (int)HttpStatusCode.Forbidden;
			customResponse.Message = "Email is not confirmed yet";
			return customResponse;
		}



		bool isValidPassword = await userManager.CheckPasswordAsync(user, userLoginDTO.Password);
		if (isValidPassword == false)
		{
			customResponse.StatusCode = (int)HttpStatusCode.Unauthorized;
			customResponse.Message = "Incorrect password, please check again";
			return customResponse;
		}


		AuthenticationUser authenticationUser = mapper.Map<AuthenticationUser>(user);
		authenticationUser.RoleName = (await userManager.GetRolesAsync(user))[0].Trim();
		authenticationUser.Token = ServiceUtilities.CreateJwtToken(authenticationUser.RoleName, configuration);


		customResponse.StatusCode = (int)HttpStatusCode.OK;
		customResponse.Data = authenticationUser;
		customResponse.Message = "Basic login successfully";

		return customResponse;
	}








	public async Task<CustomResponse<AuthenticationUser>> GoogleLogin(GoogleLoginDTO googleLoginDTO)
	{

		CustomResponse<AuthenticationUser> customResponse = new();

		var settings = new GoogleJsonWebSignature.ValidationSettings()
		{
			Audience = new List<string>() { configuration.GetSection("GoogleAuthenSettings:clientId").Value! }
		};


		var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDTO.IdToken, settings);


		if (payload is null)
		{
			customResponse.Message = "Invalid google token";
			customResponse.StatusCode = (int)HttpStatusCode.Forbidden;
			return customResponse;
		}


		UserLoginInfo info = new(googleLoginDTO.Provider, payload.Subject, googleLoginDTO.Provider);
		AppUser? user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


		// User is not login with google (No record in DB)
		if (user is null)
		{
			user = await userManager.FindByEmailAsync(payload.Email);
			bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);


			if (isEmailConfirmed == false)
			{
				customResponse.Message = "Email is not confirmed yet";
				customResponse.StatusCode = (int)HttpStatusCode.Forbidden;
				return customResponse;
			}

			await userManager.AddLoginAsync(user!, info);

			AuthenticationUser authenticationUser = mapper.Map<AuthenticationUser>(user);
			authenticationUser.RoleName = (await userManager.GetRolesAsync(user!))[0];
			authenticationUser.Token = ServiceUtilities.CreateJwtToken(authenticationUser.RoleName, configuration);

			customResponse.StatusCode = (int)HttpStatusCode.OK;
			customResponse.Data = authenticationUser;
			customResponse.Message = "Google login successfully";

		}

		return customResponse;
	}
}

