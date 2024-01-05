using Repositories.DTOs;

namespace Services.UserService
{
	public interface IUserService
	{
		Task<GetUserDTO> GetUserByEmailAndPassword(string email, string password);
		Task<GetUserDTO> GetUserByEmail(string email);
	}
}
