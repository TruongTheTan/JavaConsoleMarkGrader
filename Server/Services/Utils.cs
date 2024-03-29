using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.DTOs;



namespace Services
{
	public static class Utils
	{

		private static readonly IConfigurationRoot configuration;


		static Utils()
		{
			var builder = new ConfigurationBuilder();

			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

			configuration = builder.Build();
		}



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




		public static async Task<bool> SendEmailAsync(string subject, string body, string to)
		{

			if (!(await ValidateEmailAsync(to)))
				return false;


			string sender = configuration.GetSection("MailSettings:Sender").Value!;
			string password = configuration.GetSection("MailSettings:AppPassword").Value!;

			MailMessage mail = new();
			SmtpClient smtpClient = new("smtp.gmail.com");


			mail.From = new MailAddress(sender);
			mail.To.Add(to);
			mail.Subject = subject;
			mail.Body = body;


			smtpClient.Port = 587;
			smtpClient.UseDefaultCredentials = false;
			smtpClient.Credentials = new NetworkCredential(sender, password);
			smtpClient.EnableSsl = true;


			try
			{
				await smtpClient.SendMailAsync(mail);
				Console.WriteLine("Email sent successfully.");
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to send email: " + ex.Message);
				return false;
			}
			finally
			{
				mail.Dispose();
				smtpClient.Dispose();
			}
		}




		private static async Task<bool> ValidateEmailAsync(string email)
		{

			const string apiKey = "1d6495eafd9045e486e37c8d0d2b40b8";
			string apiUrl = $"https://emailvalidation.abstractapi.com/v1/?api_key={apiKey}&email={email}";


			using HttpClient client = new();
			HttpResponseMessage response = await client.GetAsync(apiUrl);

			if (!response.IsSuccessStatusCode)
				return false;

			string resultJsonString = await response.Content.ReadAsStringAsync();
			return resultJsonString.Contains("\"deliverability\":\"DELIVERABLE\"");
		}
	}
}
