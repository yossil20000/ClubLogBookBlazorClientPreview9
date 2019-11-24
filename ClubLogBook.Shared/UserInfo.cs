using System;
using System.Collections.Generic;
using System.Linq;

namespace ClubLogBook.Shared
{
    public class UserInfo
    {
		public string Id { get; set; } = "";
        public bool IsAuthenticated { get; set; }
		public string UserName { get; set; } = "A";
		public string Email { get; set; } = "B";
		public string MemberDescription { get; set; }
		public Dictionary<string, List<string>> ExposedClaims { get; set; } = new Dictionary<string, List<string>>();
    }
	public class AdminUserInfo : UserInfo
	{
		//public string SelectedRole { get; set; } = IdentityInfo.Roles.Guests.ToString();
		public List<string> Roles { get; set; } = new List<string>();
		public List<string> Policies { get; set; } = new List<string>();

		public int PilotId { get; set; }
		public string Password { get; set; } = "";
		//public string PasswordHash { get; set; } = "*****";
		public string GetformatedRoles()
		{

			string formated = String.Join("/", Roles.OrderBy(s => s));
			return formated;
		}
	}
}
