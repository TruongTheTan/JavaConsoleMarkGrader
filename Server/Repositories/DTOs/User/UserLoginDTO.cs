using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs.User
{
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
