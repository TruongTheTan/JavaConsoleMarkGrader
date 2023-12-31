using Repositories.DTOs.User;

namespace Services.UserService
{
	public interface IUserService
	{
		Task<UserDTO> GetUserByEmailAndPassword(string email, string password);
		Task<UserDTO> GetUserByEmail(string email);
	}
}
