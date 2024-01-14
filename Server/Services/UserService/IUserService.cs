using Repositories.DTOs;

namespace Services.UserService
{
	public interface IUserService
	{
		Task<GetUserDTO?> Login(UserLoginDTO userLoginDTO);
		Task<GetUserDTO?> GetUserByGuid(Guid Id);
		Task<bool> ChangeUserPassword(ChangeUserPasswordDTO changeUserPasswordDTO);
		Task<GetUserDTO> GetUserByEmail(string email);
		Task<bool> AddNewUser(CreateUserDTO createUserDTO);
	}
}
