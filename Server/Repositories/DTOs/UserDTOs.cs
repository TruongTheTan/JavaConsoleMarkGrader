using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
	public class GetUserDTO
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
		public string? RoleName { get; set; }
	}



	public class UserLoginDTO
	{
		[Required]
		[DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
		public string Email { get; set; } = null!;

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;
	}
}
