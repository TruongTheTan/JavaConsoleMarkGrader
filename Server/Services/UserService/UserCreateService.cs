using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;

namespace Services.UserService
{
	public class UserCreateService : UserService
	{
		public UserCreateService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(mapper, unitOfWork, userManager)
		{
		}




		public async Task<bool> CreateUserByEmail(CreateUserDTO createUserDTO)
		{

			if (await userManager.FindByEmailAsync(createUserDTO.Email) == null)
			{

				var user = new IdentityUser()
				{
					Email = createUserDTO.Email,
					UserName = createUserDTO.Name!,
				};



				string defaultPassword = configuration.GetSection("DefaultPassword").Value!;
				IdentityResult result = await userManager.CreateAsync(user, defaultPassword);


				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, createUserDTO.RoleName);
					string token = await userManager.GenerateEmailConfirmationTokenAsync(user);


					string body = $"<a href='http://localhost:4200/confirm-email'>Click this link to confirm your email</a> \n" +
						$"Please copy this code to the following form: {token}";

					return await Utils.SendEmailAsync("Email confirmation", body, createUserDTO.Email);
				}

			}
			return false;
		}
	}
}
