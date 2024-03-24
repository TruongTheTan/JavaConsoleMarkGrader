using Repositories.DTOs;

namespace Services.UserService
{
	public interface IUserService
	{
		Task<AuthenticationUser?> Login(UserLoginDTO userLoginDTO);
		Task<AuthenticationUser?> GoogleLogin(GoogleLoginDTO googleLoginDTO);
		Task<GetUserDTO?> GetUserByGuid(Guid Id);
		Task<bool> ChangeUserPassword(ChangeUserPasswordDTO changeUserPasswordDTO);
		Task<GetUserDTO> GetUserByEmail(string email);
		Task<bool> AddNewUser(CreateUserDTO createUserDTO);
		Task<bool> ResetPassword(string email);
		Task<List<GetUserDTO>> GetUserList();

	}
}
