using Microsoft.AspNetCore.Identity;

namespace Server.DAL.Entities;

public class AppUser : IdentityUser<Guid>
{
	public bool IsActive { get; set; }
}
