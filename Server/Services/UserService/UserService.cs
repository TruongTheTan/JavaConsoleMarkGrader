using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.DTOs;
using Repositories.EntiyRepository;

namespace Services.UserService
{

	/* Use for getting user */
	public class UserService
	{
		protected readonly IMapper mapper;
		protected readonly UnitOfWork unitOfWork;
		protected readonly UserRepository userRepository;
		protected readonly IConfigurationRoot configuration;
		protected readonly UserManager<IdentityUser> userManager;



		public UserService(IMapper mapper, UnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
		{
			this.mapper = mapper;
			this.unitOfWork = unitOfWork;
			this.userManager = userManager;
			this.userRepository = unitOfWork.UserRepository;


			var builder = new ConfigurationBuilder();

			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			this.configuration = builder.Build();
		}




		public async Task<bool> ConfirmEmail(UserConfirmEmail userConfirmEmail)
		{
			IdentityUser identityUser = await userManager.FindByEmailAsync(userConfirmEmail.Email.Trim());
			var result = await userManager.ConfirmEmailAsync(identityUser, userConfirmEmail.Token);


			if (result.Succeeded)
			{
				const string body = "Your email has been confirmed successfully, now you can login";
				_ = Utils.SendEmailAsync("Confirm email successfully", body, userConfirmEmail.Email.Trim());

				return true;
			}
			return false;
		}
	}













}
