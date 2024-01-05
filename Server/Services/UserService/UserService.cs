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
	}
}
