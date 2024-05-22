using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Repositories.DTOs;
using Server.DAL.Entities;

namespace Services.UserService;

public partial class UserService
{
	public async Task<CustomResponse<AuthenticationUser>> BasicLogin(UserLoginDTO userLoginDTO)
	{
		CustomResponse<AuthenticationUser> customResponse = new();
		AppUser? user = await userManager.FindByEmailAsync(userLoginDTO.Email.Trim());


		if (user is null)
		{
			customResponse.StatusCode = ServiceUtilities.NOT_FOUND;
			customResponse.Message = "User (email) not found, please check again";
		}
		else
		{
			bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);


			if (isEmailConfirmed == false)
			{
				customResponse.StatusCode = ServiceUtilities.FORBBIDEN;
				customResponse.Message = "Email is not confirmed yet";
			}
			else
			{
				bool validPassword = await userManager.CheckPasswordAsync(user, userLoginDTO.Password);

				if (validPassword == false)
				{
					customResponse.StatusCode = ServiceUtilities.UNAUTHORIZED;
					customResponse.Message = "Incorrect password, please check again";
				}
				else
				{
					AuthenticationUser authenticationUser = mapper.Map<AuthenticationUser>(user);
					authenticationUser.RoleName = (await userManager.GetRolesAsync(user))[0];

					string secretKey = configuration.GetSection("JWT:SecretKey").Value!;
					int expiration = Convert.ToInt32(configuration.GetSection("JWT:Expiration").Value!);

					ServiceUtilities.CreateJwtToken(ref authenticationUser, secretKey, expiration);

					customResponse.StatusCode = ServiceUtilities.OK;
					customResponse.Data = authenticationUser;
					customResponse.Message = "Basic login successfully";
				}
			}
		}
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


		if (payload == null)
		{
			customResponse.Message = "Invalid google token";
			customResponse.StatusCode = ServiceUtilities.FORBBIDEN;
		}
		else
		{
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
					customResponse.StatusCode = ServiceUtilities.FORBBIDEN;
				}
				else
				{
					await userManager.AddLoginAsync(user!, info);

					AuthenticationUser authenticationUser = mapper.Map<AuthenticationUser>(user);
					authenticationUser.RoleName = (await userManager.GetRolesAsync(user!))[0];


					string secretKey = configuration.GetSection("JWT:SecretKey").Value!;
					int expiration = Convert.ToInt32(configuration.GetSection("JWT:Expiration").Value!);

					ServiceUtilities.CreateJwtToken(ref authenticationUser, secretKey, expiration);

					customResponse.StatusCode = ServiceUtilities.OK;
					customResponse.Data = authenticationUser;
					customResponse.Message = "Google login successfully";
				}
			}
		}
		return customResponse;
	}
}

