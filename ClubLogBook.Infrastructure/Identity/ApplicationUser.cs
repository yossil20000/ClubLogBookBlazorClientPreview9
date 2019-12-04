using Microsoft.AspNetCore.Identity;
using System;
namespace ClubLogBook.Infrastructure.Identity
{
	public class ApplicationUser : IdentityUser<Guid>
	{
	}
	public class ApplicationRoles : IdentityRole<Guid>
	{
	}
}
