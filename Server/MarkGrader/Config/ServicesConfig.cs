using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Models;
using Services.SemesterService;
using Services.StudentService;
using Services.TestCaseService;
using Services.UserService;

namespace MarkGrader.Config
{
	public static class ServicesConfig
	{

		public static IServiceCollection Services { get; set; }



		public static void AddCors()
		{
			Services.AddCors(options =>
			{
				options.AddDefaultPolicy(
					builder =>
					{
						builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
					});
			});
		}



		public static void AddDIs()
		{
			Services.AddScoped<UnitOfWork>();
			Services.AddScoped<PRO192_Auto_GraderContext>();
			Services.AddScoped<IUserService, UserService>();
			Services.AddScoped<IStudentService, StudentService>();
			Services.AddScoped<ISemesterService, SemesterService>();
			Services.AddScoped<ITestCaseService, TestCaseService>();


			var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
			Services.AddSingleton(mappingConfig.CreateMapper());
		}



		public static void AddJWTAuthentication(string secretKey)
		{
			Services.AddSwaggerGen(x =>
			{
				x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Description = "Bearer Authentication with JWT Token",
					Type = SecuritySchemeType.Http
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

			Services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,

					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
					ClockSkew = TimeSpan.Zero,
				});
		}





		public static void AddAuthorization()
		{
			Services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminAndTeacherRole", policy => policy.RequireRole("Admin", "Teacher"));
			});
		}

	}



}
