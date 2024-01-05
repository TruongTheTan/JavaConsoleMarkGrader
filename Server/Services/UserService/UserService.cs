using AutoMapper;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;
using Repositories.Models;

namespace Services.UserService
{
	public class UserService : IUserService
	{
		private readonly IMapper mapper;
		private readonly UnitOfWork unitOfWork;
		private readonly UserRepository userRepository;



		public UserService(IMapper mapper, UnitOfWork unitOfWork)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			userRepository = unitOfWork.UserRepository;
		}



		public async Task<GetUserDTO> GetUserByEmailAndPassword(string email, string password)
		{
			User userFound = await userRepository.GetUserByEmailAndPassword(email, password);

			if (userFound != null)
				return mapper.Map<GetUserDTO>(userFound);

			return null!;
		}




		public async Task<GetUserDTO> GetUserByEmail(string email)
		{
			User userFound = await userRepository.GetUserByEmail(email);

			if (userFound != null)
				return mapper.Map<GetUserDTO>(userFound);

			return null!;
		}



		public async Task<bool> AddNewUser(CreateUserDTO createUserDTO, string defaultPassword)
		{
			if (await userRepository.GetUserByEmail(createUserDTO.Email) == null)
			{
				User newUser = mapper.Map<User>(createUserDTO);
				newUser.Password = defaultPassword;
				return await userRepository.CreateAsync(newUser);
			}
			return false;
		}



		public async Task<GetUserDTO?> GetUserByGuid(Guid Id)
		{
			User user = await userRepository.GetUserByGuid(Id);
			return user is null ? null : mapper.Map<GetUserDTO>(user);
		}



		public async Task<bool> ChangeUserPassword(string email, string password)
		{
			User user = await userRepository.GetUserByEmail(email);

			if (user != null)
			{
				user.Password = password;
				return await userRepository.UpdateAsync(user);
			}
			return false;
		}
	}
}
