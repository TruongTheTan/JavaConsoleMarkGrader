using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;

namespace Services.UserService
{
	public class UserLoginService : UserService
	{
		public UserLoginService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(mapper, unitOfWork, userManager)
		{
		}




		public async Task<AuthenticationUser?> BasicLogin(UserLoginDTO userLoginDTO)
		{
			IdentityUser? user = await userManager.FindByEmailAsync(userLoginDTO.Email.Trim());
			if (user != null)
			{

				bool isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);


				if (isEmailConfirmed)
				{
					bool validPassword = await userManager.CheckPasswordAsync(user, userLoginDTO.Password);

					if (validPassword)
					{
						AuthenticationUser authenticationUser = mapper.Map<AuthenticationUser>(user);
						authenticationUser.RoleName = (await userManager.GetRolesAsync(user))[0];

						string secretKey = configuration.GetSection("JWT:SecretKey").Value!;
						int expiration = Convert.ToInt32(configuration.GetSection("JWT:Expiration").Value!);

						Utils.CreateJwtToken(ref authenticationUser, secretKey, expiration);

						return authenticationUser;
					}
				}
			}
			return null;
		}








		public async Task<AuthenticationUser?> GoogleLogin(GoogleLoginDTO googleLoginDTO)
		{

			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string>() { configuration.GetSection("GoogleAuthenSettings:clientId").Value! }
			};


			var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDTO.IdToken, settings);
			if (payload == null)
				return null;

			UserLoginInfo info = new(googleLoginDTO.Provider, payload.Subject, googleLoginDTO.Provider);


			IdentityUser? user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);


			if (user is null)
			{
				user = await userManager.FindByEmailAsync(payload.Email);
				if (user == null)
					return null;
				await userManager.AddLoginAsync(user!, info);
			}


			AuthenticationUser authenticationUser = mapper.Map<AuthenticationUser>(user);
			authenticationUser.RoleName = (await userManager.GetRolesAsync(user!))[0];


			string secretKey = configuration.GetSection("JWT:SecretKey").Value!;
			int expiration = Convert.ToInt32(configuration.GetSection("JWT:Expiration").Value!);

			Utils.CreateJwtToken(ref authenticationUser, secretKey, expiration);

			return authenticationUser;
		}
	}
}
