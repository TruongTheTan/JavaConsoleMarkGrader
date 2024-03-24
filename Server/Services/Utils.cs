using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Repositories.DTOs;



namespace Services
{
	public static class Utils
	{


		public static void CreateJwtToken(ref AuthenticationUser user, string secretKey, int expiration)
		{

			var jwtTokenHandle = new JwtSecurityTokenHandler();

			byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

			SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature);



			var tokenDescription = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new[]
				{
					new Claim("Id", user.Id!),
					new Claim(ClaimTypes.Name, user.Name!),
					new Claim(ClaimTypes.Email, user.Email!),
					new Claim(ClaimTypes.Role, user.RoleName!),
				}),

				Expires = DateTime.UtcNow.AddMinutes(expiration),
				SigningCredentials = signingCredentials,
				IssuedAt = DateTime.Now
			};

			SecurityToken token = jwtTokenHandle.CreateToken(tokenDescription);
			user.Token = jwtTokenHandle.WriteToken(token);
		}




		public static async Task SendEmailAsync(string subject, string body, string sender, string recipient)
		{

			string smtpServer = "your_smtp_server";
			int smtpPort = 587; // Port number may vary depending on your SMTP server
			string smtpUsername = "your_username";
			string smtpPassword = "your_password";


			string senderEmail = sender;
			string recipientEmail = recipient;


			MailMessage mail = new(senderEmail, recipientEmail)
			{
				Subject = subject,
				Body = "This is your default password: 123@123A. Please change your password now" // body
			};

			// Set the SMTP client settings
			SmtpClient smtpClient = new(smtpServer, smtpPort)
			{
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(smtpUsername, smtpPassword),
				EnableSsl = true
			};

			try
			{
				await smtpClient.SendMailAsync(mail);
				Console.WriteLine("Email sent successfully.");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to send email: " + ex.Message);
			}
			finally
			{
				mail.Dispose();
				smtpClient.Dispose();
			}
		}
	}
}
