
using ClubLogBook.Core.Interfaces;

using System.Linq;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Extensions;

namespace ClubLogBook.Core.Entities
{
	public class UserInfo : AuditableEntity
	{

		public string IdNumber { get;   set; } = "";
		public string FirstName { get;  set; } = "";
		public string LastName { get;  set; } = "";
		public string MiddleName { get;  set; } = "";
		public string MemberInfo { get; set; } = "";
		public UserInfo()
		{ }
		public UserInfo(Member member)
		{
			this.Id = member.Id;
			this.IdNumber = member.IdNumber;
			this.FirstName = member.FirstName;
			this.LastName = member.LastName;
			this.MiddleName = member.MiddleName;
			if(member.Contact != null)
			{
				
				MemberInfo = member.GetJason();
			}


		}
		public string GetJason()
		{
			return EntityExtentionscs.GetJason<UserInfo>(this);
		}
		public UserInfo GetFromJason(string strJason)
		{
			return EntityExtentionscs.GetFromJason<UserInfo>(strJason);
		}
	}
}
