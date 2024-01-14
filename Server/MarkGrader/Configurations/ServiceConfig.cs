using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Repositories.Models;
using Services.SemesterService;
using Services.StudentService;
using Services.TestCaseService;
using Services.UserService;

namespace MarkGrader.Configurations
{
	public class ServiceConfig
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




		public static void AddJWTAuthentication(string secretKey)
		{

			var secretKeyByte = Encoding.UTF8.GetBytes(secretKey);

			Services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddCookie(x => x.Cookie.Name = "token")
			.AddJwtBearer(o =>
			{
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(secretKeyByte),
					ClockSkew = TimeSpan.Zero,
				};
				o.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies["token"];
						return Task.CompletedTask;
					}
				};
				o.SaveToken = true;
			});
		}





		public static void AddAuthorization()
		{
			Services.AddAuthorization(options =>
			{
				options.AddPolicy("AdminAndTeacherRole", policy => policy.RequireRole("Admin", "Teacher"));
			});
		}





		public static void AddDIs()
		{

			/*
			Services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<Java_Console_Auto_GraderContext>()
				.AddDefaultTokenProviders();
			*/
			Services.AddIdentityCore<IdentityUser>().AddEntityFrameworkStores<Java_Console_Auto_GraderContext>();

			Services.AddScoped<UserManager<IdentityUser>>();

			Services.AddScoped<UnitOfWork>();
			Services.AddScoped<Java_Console_Auto_GraderContext>();

			Services.AddScoped<IUserService, UserService>();
			Services.AddScoped<IStudentService, StudentService>();
			Services.AddScoped<ISemesterService, SemesterService>();
			Services.AddScoped<ITestCaseService, TestCaseService>();


			var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
			Services.AddSingleton(mappingConfig.CreateMapper());
		}




		public static void AddSwagger()
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
		}




		public static void AddIdentityUser()
		{

			Services.AddScoped<TempDB>();
			Services.AddDbContext<TempDB>(options => options.UseSqlServer("server=(local);database=Java_Console_Auto_Grader;Trusted_Connection=True;uid=sa;pwd=sa;"));


			Services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<TempDB>()
				.AddDefaultTokenProviders();


			Services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.User.RequireUniqueEmail = true;
				//options.SignIn.RequireConfirmedEmail = true;


				// Password settings
				//options.Password.RequiredLength = 10;
				//options.Password.RequireNonAlphanumeric = true;
				//options.Password.RequireUppercase = false;
				//options.Password.RequireLowercase = false;
				//options.Password.RequiredUniqueChars = 6;

				// Lockout settings
				//options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				//options.Lockout.MaxFailedAccessAttempts = 10;
				//options.Lockout.AllowedForNewUsers = true;

				// User settings


			});

		}
	}
}
