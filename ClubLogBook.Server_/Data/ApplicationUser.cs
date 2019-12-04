using Microsoft.AspNetCore.Identity;
using System;
namespace ClubLogBook.Server.Data
{
	public class ApplicationUser : IdentityUser<Guid>
	{
	}
	public class ApplicationRoles : IdentityRole<Guid>
	{
	}
}
