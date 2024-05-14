using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.DTOs;
using Repositories.EntityRepository;

namespace Services.UserService;


/// <summary>
/// For configuration
/// </summary>
public partial class UserService
{
	private readonly IMapper mapper;
	private readonly UnitOfWork unitOfWork;
	private readonly UserRepository userRepository;
	private readonly IConfigurationRoot configuration;
	private readonly UserManager<IdentityUser> userManager;



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



	public async Task<CustomResponse<dynamic>> ConfirmEmail(UserConfirmEmail userConfirmEmail)
	{
		IdentityUser identityUser = await userManager.FindByEmailAsync(userConfirmEmail.Email.Trim());
		bool isConfirmSuccess = (await userManager.ConfirmEmailAsync(identityUser, userConfirmEmail.Token)).Succeeded;


		CustomResponse<dynamic> customResponse = new();


		if (isConfirmSuccess)
		{
			const string emailTitle = "Confirm email successfully";
			const string emailBody = "Your email has been confirmed successfully, now you can login";

			bool isSendEmailSuccess = await ServiceUtilities.SendEmailAsync(emailTitle, emailBody, userConfirmEmail.Email);

			if (isSendEmailSuccess)
			{
				customResponse.StatusCode = ServiceUtilities.OK;
				customResponse.Message = "Email confirm has been sent to your mail box, please check";
			}
		}
		else
		{
			customResponse.StatusCode = ServiceUtilities.INTERNAL_SERVER_ERROR;
			customResponse.Message = "Fail to confirm your email";
		}
		return customResponse;
	}

}














