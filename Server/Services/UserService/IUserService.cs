using Repositories.DTOs;

namespace Services.UserService
{
	public interface IUserService
	{
		Task<GetUserDTO?> GetUserByGuid(Guid Id);

		Task<bool> ChangeUserPassword(string email, string password);

		Task<GetUserDTO> GetUserByEmailAndPassword(string email, string password);
		Task<GetUserDTO> GetUserByEmail(string email);
		Task<bool> AddNewUser(CreateUserDTO createUserDTO, string defaultPassword);
	}
}
