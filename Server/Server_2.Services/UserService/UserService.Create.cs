using System.Net;
using Repositories.DTOs;
using Server.DAL.Entities;
using Services;

namespace Server_2.Services.UserService;



public partial class UserService
{
	public async Task<CustomResponse<dynamic>> CreateUserByEmail(CreateUserDTO createUserDTO)
	{
		CustomResponse<dynamic> customResponse = new();


		bool isValidEmail = await ServiceUtility.ValidateEmailAsync(createUserDTO.Email);
		bool isUserExisted = (await userManager.FindByEmailAsync(createUserDTO.Email)) != null;


		// User has already existed
		if (isValidEmail == false)
		{
			customResponse.StatusCode = (int)HttpStatusCode.Forbidden;
			customResponse.Message = "Email is not valid, please check again";
		}
		else if (isUserExisted)
		{
			customResponse.StatusCode = (int)HttpStatusCode.Conflict;
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

					await ServiceUtility.SendEmailAsync("Email confirmation", body, createUserDTO.Email);

					customResponse.StatusCode = (int)HttpStatusCode.Created;
					customResponse.Message = "User created successfully";
				}
				else
				{
					customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
					customResponse.Message = "Create user fail";
				}
			}
			catch (Exception ex)
			{
				customResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
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

