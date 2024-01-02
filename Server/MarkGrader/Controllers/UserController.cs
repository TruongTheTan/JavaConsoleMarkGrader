using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repositories.DTOs.User;
using Services.UserService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarkGrader.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserService userService;
		private readonly IConfiguration configuration;


		public UserController(IConfiguration configuration, IUserService userService)
		{
			this.userService = userService;
			this.configuration = configuration;
		}



		[HttpPost("basic-login")]
		public async Task<IActionResult> BasicLogin([FromBody] UserLoginDTO user)
		{

			UserDTO userDTO = await this.userService.GetUserByEmailAndPassword(user.Email.Trim(), user.Password);

			if (userDTO == null)
				return NotFound("User not found");

			this.CreateToken(ref userDTO);

			return Ok(userDTO);
		}




		/*
		[HttpPost("google-login")]
		public async Task<IActionResult> GoogleLogin([FromBody] string googleIdToken)
		{

			
			try
			{
				var payload = await GoogleJsonWebSignature.ValidateAsync(googleIdToken.Trim());


				// The token is invalid or unable to decode
				if (payload == null)
					return Problem();

				UserDTO userDTO = await this.userService.GetUserByEmail(payload.Email.Trim());
				return ReturnActionResult(userDTO);
			}
			catch (InvalidJwtException ex)
			{
				return Problem(ex.ToString());

			}
		}
		*/



		private void CreateToken(ref UserDTO user)
		{
			var jwtTokenHandle = new JwtSecurityTokenHandler();

			byte[] secretKeyBytes = Encoding.UTF8.GetBytes(this.configuration["JWT:SecretKey"]);

			SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature);



			var tokenDescription = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Name!),
					new Claim(ClaimTypes.Email, user.Email!),
					new Claim(ClaimTypes.Role, user.RoleName!),
				}),

				Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(this.configuration["JWT:Expiration"])),
				SigningCredentials = signingCredentials
			};

			SecurityToken token = jwtTokenHandle.CreateToken(tokenDescription);
			user.Token = jwtTokenHandle.WriteToken(token);
		}
	}
}
