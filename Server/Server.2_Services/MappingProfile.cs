using AutoMapper;
using Repositories.DTOs;
using Server.DAL.Entities;
using Server_4.DAL.Models;

namespace Server_2.Services;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		UserMapping();
		TestCaseMapping();
		SemesterMapping();
	}



	private void UserMapping()
	{

		//CreateMap<IdentityUser, GetStudentDTO>()
		//   //.ForMember(des => des.GradingTime, option => option.MapFrom(student => student.GradingTime!.Value.ToString("dd-MM-yyyy hh:mm:ss tt")))
		//   .ReverseMap();


		CreateMap<AppUser, AuthenticationUser>()
			.ForMember(des => des.Name, option => option.MapFrom(user => user.UserName))
			.ReverseMap();

		CreateMap<AppUser, GetUserDTO>().ReverseMap();
		CreateMap<AspNetUser, GetUserDTO>().ReverseMap();
		CreateMap<AspNetUser, AuthenticationUser>().ReverseMap();
		CreateMap<AppUser, CreateUserDTO>().ReverseMap();
	}



	private void SemesterMapping()
	{
		CreateMap<Semester, GetSemesterDTO>().ReverseMap();
		CreateMap<Semester, CreateSemesterDTO>().ReverseMap();
		CreateMap<Semester, UpdateSemesterDTO>().ReverseMap();
	}



	private void TestCaseMapping()
	{
		CreateMap<TestCase, GetTestCaseDTO>()
			.ForMember(dest => dest.Inputs, opt => opt.MapFrom(src => src.Input!.Split(" ", StringSplitOptions.None).ToList()))
			.ForMember(dest => dest.Outputs, opt => opt.MapFrom(src => src.Output!.Split(" ", StringSplitOptions.None).ToList()))
			.ForMember(dest => dest.SemesterName, opt => opt.MapFrom(src => src.Semester!.SemesterName));


		CreateMap<CreateTestCaseDTO, TestCase>()
			.ForMember(dest => dest.Input, opt => opt.MapFrom(src => string.Join(" ", src.Inputs!)))
			.ForMember(dest => dest.Output, opt => opt.MapFrom(src => string.Join(" ", src.Outputs!)));


		CreateMap<UpdateTestCaseDTO, TestCase>()
			.ForMember(dest => dest.Input, opt => opt.MapFrom(src => string.Join(" ", src.Input!)))
			.ForMember(dest => dest.Output, opt => opt.MapFrom(src => string.Join(" ", src.Output!)));
	}
}
