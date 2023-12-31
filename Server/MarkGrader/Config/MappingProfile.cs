using AutoMapper;

namespace MarkGrader.Config
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{
			UserMapping();
			TestCaseMapping();
			SemesterMapping();
		}


		private static void UserMapping()
		{
			/*
			 CreateMap<User, GetStudentDTO>()
                //.ForMember(des => des.GradingTime, option => option.MapFrom(student => student.GradingTime!.Value.ToString("dd-MM-yyyy hh:mm:ss tt")))
                .ReverseMap();


            CreateMap<User, UserDTO>()
                .ForMember(des => des.RoleName, option => option.MapFrom(user => user.Role!.Name))
                .ReverseMap();
			*/
		}



		private static void SemesterMapping()
		{
			/*
			CreateMap<Semester, GetSemesterDTO>().ReverseMap();
            CreateMap<Semester, CreateSemesterDTO>().ReverseMap();
            CreateMap<Semester, UpdateSemesterDTO>().ReverseMap();
			*/
		}



		private static void TestCaseMapping()
		{
			/*
			CreateMap<TestCase, GetTestCaseDTO>()
				.ForMember(dest => dest.Input, opt => opt.MapFrom(src => src.Input!.Split(" ", StringSplitOptions.None).ToList()))
				.ForMember(dest => dest.Output, opt => opt.MapFrom(src => src.Output!.Split(" ", StringSplitOptions.None).ToList()));



			CreateMap<CreateTestCaseDTO, TestCase>()
				.ForMember(dest => dest.Input, opt => opt.MapFrom(src => string.Join(" ", src.Input!)))
				.ForMember(dest => dest.Output, opt => opt.MapFrom(src => string.Join(" ", src.Output!)));


			CreateMap<UpdateTestCaseDTO, TestCase>()
				.ForMember(dest => dest.Input, opt => opt.MapFrom(src => string.Join(" ", src.Input!)))
				.ForMember(dest => dest.Output, opt => opt.MapFrom(src => string.Join(" ", src.Output!)));
			*/
		}
	}
}
