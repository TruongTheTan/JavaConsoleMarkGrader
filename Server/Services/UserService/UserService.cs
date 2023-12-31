using AutoMapper;
using Repositories;
using Repositories.DTOs.User;
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


		public async Task<UserDTO> GetUserByEmailAndPassword(string email, string password)
		{
			User userFound = await userRepository.GetUserByEmailAndPassword(email, password);

			if (userFound != null)
				return mapper.Map<UserDTO>(userFound);

			return null!;
		}




		public async Task<UserDTO> GetUserByEmail(string email)
		{
			User userFound = await userRepository.GetUserByEmail(email);

			if (userFound != null)
				return mapper.Map<UserDTO>(userFound);

			return null!;
		}
	}
}
