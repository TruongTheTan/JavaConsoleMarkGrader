using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repositories;
using Server.DAL.Entities;
using Server_2.Services;
using Server_2.Services.UserService;
using Server_4.DAL.Contexts;
using Server_4.DAL.Models;
using Services.SemesterService;
using Services.StudentService;
using Services.TestCaseService;


namespace MarkGrader.Configurations;

public static class ServiceConfig
{
    public static IServiceCollection Services { get; set; } = null!;



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
            //opt.DefaultScheme = "Windows";
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
        /*
		var authorizationPolicyBuilder = new AuthorizationPolicyBuilder();
		authorizationPolicyBuilder.RequireAuthenticatedUser()
			.AddAuthenticationSchemes(IISDefaults.AuthenticationScheme).Build();
		*/
    }





    public static void AddDependencyInjection()
    {
        Services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<MigrationDBContext>();

        Services.AddScoped<UnitOfWork>();
        Services.AddScoped<JavaConsoleAutoGraderContext>();

        Services.AddScoped<UserService>();
        Services.AddScoped<StudentService>();
        Services.AddScoped<TestCaseService>();
        Services.AddScoped<UserManager<AppUser>>();
        Services.AddScoped<ISemesterService, SemesterService>();


        var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
        Services.AddSingleton(mappingConfig.CreateMapper());
    }


    public static void AddIdentityUser()
    {
        Services.AddScoped<MigrationDBContext>();


        const string connectionString = "server=(local);database=JavaConsoleAutoGrader;Trusted_Connection=True;uid=sa;pwd=sa;";
        Services.AddDbContext<MigrationDBContext>(options => options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));



        Services.AddIdentity<AppUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<MigrationDBContext>()
            .AddDefaultTokenProviders();


        Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;


            // Lockout settings
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //options.Lockout.MaxFailedAccessAttempts = 10;
            //options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

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
            //x.OperationFilter<FileUploadOperationFilter>();
        });
    }





}

