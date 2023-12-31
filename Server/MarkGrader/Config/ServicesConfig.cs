using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MarkGrader.Config
{
	public static class ServicesConfig
	{

		public static IServiceCollection Services { get; set; } = null!;




		public static void AddDIs()
		{
			// todo
		}



		public static void AddJWTAuthentication(string secretKey)
		{
			Services.AddSwaggerGen(x =>
			{
				x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "JWT Authorization",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				x.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});


			var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);

			Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
					ClockSkew = TimeSpan.Zero,
				});
			Services.AddAuthorization();
		}

	}



}
