using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
using System.Linq;
using ClubLogBook.Core.Common;

namespace ClubLogBook.Core.Entities
{
	public enum MemberStatus
	{
		NotActive,
		Susspend,
		Active

	}
	public  class Member : AuditableEntity, IAggregateRoot, IMember
	{
		public Member() { }
		public Member(int id, string idNumber, string firstName, string lastname, Gender gender)
		{
			Id = id; IdNumber = IdNumber; FirstName = firstName; LastName = lastname; Gender = gender;
		}
		public Member(Member member)
		{
			
		}

		public string IdNumber { get; set; } = "";
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string MiddleName { get; set; } = "";
		public Gender Gender { get; set; }
		public DateTime DateOfBirth { get; set; } =		DateTime.Now.AddYears(-5);
		public byte[] Photo { get; set; } = new byte[0];
		public Decimal Height { get; set; }
		public Decimal Weight { get; set; }

		public Contact Contact { get; set; } = new Contact();
		//public ClubRoll ClubRoll { get; set; } = ClubRoll.ClubMember;
		public string UserId { get; set; } 
		
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"Id:{Id} IdNumber:{IdNumber}");
			sb.AppendLine();
			sb.Append($"{FirstName} {MiddleName} {LastName} is {Gender}");
			sb.AppendLine();
			return sb.ToString();
			
		}
		public Member Get()
		{
			return this;
		}
		
	}
}
