using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories;
using Repositories.DTOs;

namespace Services.UserService
{
	public class UserPasswordService : UserService
	{
		public UserPasswordService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(mapper, unitOfWork, userManager)
		{
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
	}
}
