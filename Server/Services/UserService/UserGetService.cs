using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.DTOs;

namespace Services.UserService
{
	public class UserGetService : UserService
	{
		public UserGetService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager) : base(mapper, unitOfWork, userManager)
		{
		}



		public async Task<GetUserDTO> GetUserByEmail(string email)
		{
			IdentityUser identityUser = await userManager.FindByEmailAsync(email);

			if (identityUser != null)
				return mapper.Map<GetUserDTO>(identityUser);

			return null!;
		}




		public async Task<GetUserDTO?> GetUserByGuid(Guid Id)
		{
			IdentityUser user = await userManager.FindByIdAsync(Id.ToString());
			return user is null ? null : mapper.Map<GetUserDTO>(user);
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
