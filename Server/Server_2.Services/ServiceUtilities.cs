using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;



namespace Services;

public static class ServiceUtilities
{

	private static readonly IConfigurationRoot configuration;


	public static int OK { get => 200; }




	static ServiceUtilities()
	{
		var builder = new ConfigurationBuilder();

		builder.SetBasePath(Directory.GetCurrentDirectory());
		builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

		configuration = builder.Build();
	}



	public static string CreateJwtToken(string userRoleName, IConfigurationRoot configuration)
	{

		string secretKey = configuration.GetSection("JWT:SecretKey").Value!;
		int expiration = Convert.ToInt32(configuration.GetSection("JWT:Expiration").Value!);

		var jwtTokenHandle = new JwtSecurityTokenHandler();
		byte[] secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

		SigningCredentials signingCredentials = new(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature);


		var tokenDescription = new SecurityTokenDescriptor()
		{
			Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, userRoleName), }),
			Expires = DateTime.UtcNow.AddMinutes(expiration),
			SigningCredentials = signingCredentials,
			IssuedAt = DateTime.Now
		};

		SecurityToken token = jwtTokenHandle.CreateToken(tokenDescription);
		return jwtTokenHandle.WriteToken(token);
	}




	public static async Task<bool> SendEmailAsync(string subject, string body, string to)
	{

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




	internal static async Task<bool> ValidateEmailAsync(string email)
	{

		const string apiKey = "1d6495eafd9045e486e37c8d0d2b40b8";
		string verificationAPI = $"https://emailvalidation.abstractapi.com/v1/?api_key={apiKey}&email={email}";


		using HttpClient client = new();
		HttpResponseMessage response = await client.GetAsync(verificationAPI);


		if (!response.IsSuccessStatusCode)
			return false;


		string jsonStringResult = await response.Content.ReadAsStringAsync();
		var responseObject = JsonConvert.DeserializeAnonymousType(jsonStringResult, new { deliverability = "" });


		client.Dispose();
		response.Dispose();

		return responseObject!.deliverability.Equals("DELIVERABLE");
	}
}

