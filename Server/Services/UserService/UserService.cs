using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;

namespace Services.UserService
{
	public class UserService : IUserService
	{

		private readonly IMapper mapper;
		private readonly UnitOfWork unitOfWork;
		private readonly UserRepository userRepository;
		private readonly IConfigurationRoot configuration;
		private readonly UserManager<IdentityUser> userManager;



		public UserService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			this.userRepository = unitOfWork.UserRepository;


			var builder = new ConfigurationBuilder();

			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			this.configuration = builder.Build();
		}








		public async Task<GetUserDTO> GetUserByEmail(string email)
		{
			IdentityUser identityUser = await userManager.FindByEmailAsync(email);

			if (identityUser != null)
				return mapper.Map<GetUserDTO>(identityUser);

			return null!;
		}



		public async Task<bool> AddNewUser(CreateUserDTO createUserDTO)
		{

			if (await userManager.FindByEmailAsync(createUserDTO.Email) == null)
			{

				var user = new IdentityUser()
				{
					Email = createUserDTO.Email,
					UserName = createUserDTO.Name!,
				};


				IdentityResult result = await userManager.CreateAsync(user, configuration.GetSection("DefaultPassword").Value!);


				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, createUserDTO.RoleName);

					_ = Utils.SendEmailAsync("New account created", "", "Admin", user.Email);

					return true;
				}

			}
			return false;
		}





		public async Task<GetUserDTO?> GetUserByGuid(Guid Id)
		{
			IdentityUser user = await userManager.FindByIdAsync(Id.ToString());
			return user is null ? null : mapper.Map<GetUserDTO>(user);
		}






		public async Task<bool> ChangeUserPassword(ChangeUserPasswordDTO changeUserPasswordDTO)
		{
			var user = await userManager.FindByEmailAsync(changeUserPasswordDTO.Email);

			if (user != null)
			{
				var result = await userManager.ChangePasswordAsync(user, changeUserPasswordDTO.OldPassword, changeUserPasswordDTO.NewPassword);

				return result.Succeeded;
			}
			return false;
		}






		public async Task<AuthenticationUser?> Login(UserLoginDTO userLoginDTO)
		{
			IdentityUser user = await userManager.FindByEmailAsync(userLoginDTO.Email);

			if (user != null)
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
			return null;
		}






		public async Task<bool> ResetPassword(string email)
		{
			IdentityUser user = await userManager.FindByEmailAsync(email);

			if (user != null)
			{
				IdentityResult result = await userManager.RemovePasswordAsync(user);

				if (result.Succeeded)
				{
					result = await userManager.AddPasswordAsync(user, configuration.GetSection("DefaultPassword").Value!);
					return result.Succeeded;
				}
			}
			return false;

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


		public async Task<List<GetUserDTO>> GetUserList()
		{
			List<GetUserDTO> listUser = new();
			List<IdentityUser> list = await userManager.Users.ToListAsync();


			foreach (var user in list)
			{
				GetUserDTO getUserDTO = mapper.Map<GetUserDTO>(user);
				getUserDTO.RoleName = (await userManager.GetRolesAsync(user))[0];

				listUser.Add(getUserDTO);
			}

			return listUser;
		}
	}
}
