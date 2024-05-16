using Repositories.DTOs;
using Server.DAL.Entities;

namespace Services.UserService;



public partial class UserService
{
	public async Task<CustomResponse<dynamic>> CreateUserByEmail(CreateUserDTO createUserDTO)
	{
		CustomResponse<dynamic> customResponse = new();


		bool isValidEmail = await ServiceUtilities.ValidateEmailAsync(createUserDTO.Email);
		bool isUserExisted = (await userManager.FindByEmailAsync(createUserDTO.Email)) != null;


		// User has already existed
		if (isValidEmail == false)
		{
			customResponse.StatusCode = ServiceUtilities.FORBBIDEN;
			customResponse.Message = "Email is not valid, please check again";
		}
		else if (isUserExisted)
		{
			customResponse.StatusCode = ServiceUtilities.CONFLICT;
			customResponse.Message = "This email has been registered";
		}
		else
		{
			var user = new AppUser()
			{
				Email = createUserDTO.Email,
				UserName = createUserDTO.Name!,
				IsActive = true,
			};


			string defaultPassword = configuration.GetSection("DefaultPassword").Value!.Trim();
			using var transaction = await unitOfWork.Context.Database.BeginTransactionAsync();


			try
			{
				bool isCreateUserSuccess = (await userManager.CreateAsync(user, defaultPassword)).Succeeded;
				bool isAddToRoleSuccess = (await userManager.AddToRoleAsync(user, createUserDTO.RoleName)).Succeeded;

				await transaction.CommitAsync();


				if (isCreateUserSuccess && isAddToRoleSuccess)
				{
					string token = await userManager.GenerateEmailConfirmationTokenAsync(user);

					string body = $"<a href='http://localhost:4200/confirm-email'>Click this link to confirm your email</a> \n" +
					$"Please copy this code to the following form: {token}";

					await ServiceUtilities.SendEmailAsync("Email confirmation", body, createUserDTO.Email);

					customResponse.StatusCode = ServiceUtilities.CREATED;
					customResponse.Message = "User created successfully";
				}
				else
				{
					customResponse.StatusCode = ServiceUtilities.INTERNAL_SERVER_ERROR;
					customResponse.Message = "Create user fail";
				}
			}
			catch (Exception ex)
			{
				customResponse.StatusCode = ServiceUtilities.INTERNAL_SERVER_ERROR;
				customResponse.Message = ex.Message;
				await transaction.RollbackAsync();
			}
			finally
			{
				await transaction.DisposeAsync();
			}
		}
		return customResponse;
	}
}

