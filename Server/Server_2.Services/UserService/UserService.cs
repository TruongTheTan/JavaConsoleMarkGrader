using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Repositories;
using Repositories.EntityRepository;
using Server.DAL.Entities;

namespace Server_2.Services.UserService;


/// <summary>
/// For configuration
/// </summary>
public partial class UserService
{
    private readonly IMapper mapper;
    private readonly UnitOfWork unitOfWork;
    private readonly UserRepository userRepository;
    private readonly IConfigurationRoot configuration;
    private readonly UserManager<AppUser> userManager;



    public UserService(IMapper mapper, UnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.userManager = userManager;
        this.userRepository = unitOfWork.UserRepository;


        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        this.configuration = builder.Build();
    }

}














