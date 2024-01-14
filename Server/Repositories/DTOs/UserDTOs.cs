using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
	public class GetUserDTO
	{
		public string Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
		public string? RoleName { get; set; }
	}



	public class UserLoginDTO
	{
		[Required, EmailAddress]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
	}



	public class CreateUserDTO
	{
		[Required]
		public string? Name { get; set; }

		[Required, EmailAddress]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
		public string Email { get; set; } = null!;

		[Required]
		public string? RoleName { get; set; }
	}




	public class ChangeUserPasswordDTO
	{
		[Required, EmailAddress]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email format")]
		public string Email { get; set; } = null!;


		[Required]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; } = null!;
	}
}
