using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ClubLogBook.Shared
{
	public static class IdentityInfo
	{
		public enum Roles
		{
			Guests,
			Users,
			Members,
			AccountManage,
			Administrators,
			Developer
		}
		public enum Policiy
		{

		}
		public static string Role(List<Roles> p)
		{
			return string.Join(",", p.ToString());
		}
		public static List<string > IdentityRoles { get { return Enum.GetValues(typeof(Roles)).Cast<Roles>().Select(x => x.ToString()).ToList(); } }
		public static List<string> IdentityPolicies { get { return Enum.GetValues(typeof(Policiy)).Cast<Policiy>().Select(x => x.ToString()).ToList(); } }
	}
}
