using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;

namespace Services.UserService
{
	public class UserService : IUserService
	{


		private readonly IMapper mapper;
		private readonly UnitOfWork unitOfWork;
		private readonly UserRepository userRepository;
		private readonly IConfigurationRoot configuration;
		private readonly UserManager<IdentityUser> userManager;
		//private readonly SignInManager<IdentityUser> _signInManager;



		public UserService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			userRepository = unitOfWork.UserRepository;

			var builder = new ConfigurationBuilder()
							   .SetBasePath(Directory.GetCurrentDirectory())
							   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			this.configuration = builder.Build();
		}








		public async Task<GetUserDTO> GetUserByEmail(string email)
		{
			IdentityUser identityUser = await userManager.FindByEmailAsync(email);

			if (identityUser != null)
				return mapper.Map<GetUserDTO>(identityUser);

			return null!;
		}



		public async Task<bool> AddNewUser(CreateUserDTO createUserDTO)
		{

			if (await userManager.FindByEmailAsync(createUserDTO.Email) == null)
			{

				var user = new IdentityUser()
				{
					Email = createUserDTO.Email,
					UserName = createUserDTO.Name!,
				};

				IdentityResult result = await userManager.CreateAsync(user, configuration.GetSection("DefaultPassword").Value!);

				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, createUserDTO.RoleName);
					return true;
				}

			}
			return false;
		}



		public async Task<GetUserDTO?> GetUserByGuid(Guid Id)
		{
			IdentityUser user = await userManager.FindByIdAsync(Id.ToString());
			return user is null ? null : mapper.Map<GetUserDTO>(user);
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





		public async Task<GetUserDTO?> Login(UserLoginDTO userLoginDTO)
		{
			IdentityUser result = await userManager.FindByEmailAsync(userLoginDTO.Email);

			if (result != null)
			{
				bool validPassword = await userManager.CheckPasswordAsync(result, userLoginDTO.Password);

				if (validPassword)
				{
					GetUserDTO getUserDTO = mapper.Map<GetUserDTO>(result);
					getUserDTO.RoleName = (await userManager.GetRolesAsync(result))[0];

					CreateJwtToken(ref getUserDTO);

					return getUserDTO;
				}
			}
			return null;
		}







		private void CreateJwtToken(ref GetUserDTO user)
		{

			var jwtTokenHandle = new JwtSecurityTokenHandler();

			byte[] secretKeyBytes = Encoding.UTF8.GetBytes(configuration.GetSection("JWT:SecretKey").Value!);

			SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature);



			var tokenDescription = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", user.Id),
					new Claim(ClaimTypes.Name, user.Name!),
					new Claim(ClaimTypes.Email, user.Email!),
					new Claim(ClaimTypes.Role, user.RoleName!),
				}),

				Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration.GetSection("JWT:Expiration").Value!)),
				SigningCredentials = signingCredentials,
				IssuedAt = DateTime.Now
			};

			SecurityToken token = jwtTokenHandle.CreateToken(tokenDescription);
			user.Token = jwtTokenHandle.WriteToken(token);
		}

	}
}
