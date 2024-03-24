using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Repositories.DTOs;
using Repositories.Models;

namespace MarkGrader.Configurations
{
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


			CreateMap<IdentityUser, AuthenticationUser>()
				.ForMember(des => des.Name, option => option.MapFrom(user => user.UserName))
				.ReverseMap();

			CreateMap<IdentityUser, GetUserDTO>().ReverseMap();
			CreateMap<AspNetUser, GetUserDTO>().ReverseMap();
			CreateMap<AspNetUser, AuthenticationUser>().ReverseMap();
			CreateMap<IdentityUser, CreateUserDTO>().ReverseMap();
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
				.ForMember(dest => dest.Input, opt => opt.MapFrom(src => src.Input!.Split(" ", StringSplitOptions.None).ToList()))
				.ForMember(dest => dest.Output, opt => opt.MapFrom(src => src.Output!.Split(" ", StringSplitOptions.None).ToList()));



			CreateMap<CreateTestCaseDTO, TestCase>()
				.ForMember(dest => dest.Input, opt => opt.MapFrom(src => string.Join(" ", src.Input!)))
				.ForMember(dest => dest.Output, opt => opt.MapFrom(src => string.Join(" ", src.Output!)));


			CreateMap<UpdateTestCaseDTO, TestCase>()
				.ForMember(dest => dest.Input, opt => opt.MapFrom(src => string.Join(" ", src.Input!)))
				.ForMember(dest => dest.Output, opt => opt.MapFrom(src => string.Join(" ", src.Output!)));

		}


	}
}
