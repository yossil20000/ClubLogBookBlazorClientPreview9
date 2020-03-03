using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
using UnitsTest.ApplicationCore.Builder;
namespace UnitsTest.ApplicationCore.MemberTest
{
	public class ClubAddMemberTest
	{
		public Club club = new Club();

		public ClubAddMemberTest()
		{
			club.Id = 1;
			club.Name = "BUZ";
		}
		private string _testName = "Yosef";
		private DateTime _testDate = new DateTime(1965, 8, 21);
		//[Test]
		//public void AddMember()
		//{
		//	var club = new Club();
		//	club.AddMember(new Pilot(1, "054928392", _testName, "", ClubLogBook.Core.Interfaces.Gender.Male));
		//	Pilot p = new Pilot(2, "054928393", _testName, "", ClubLogBook.Core.Interfaces.Gender.Male);
			
		//	p.AddLicense(new License() { Id = 1, LicenseNumber = 2914 });
		//	club.AddMember(p);
		//	var members = club.Members;

		//	IEnumerable<IMember> m = members as IEnumerable<IMember>;
		//	IEnumerable<Pilot> nmn = m.<Pilot, IMember>();
		//	IEnumerable<Pilot>  nnn = IEnumerableExtentionscs.GetByType<Pilot>(m);

		//	foreach (var pe in nnn)
		//	{
		//		foreach (var pp in pe.Licenses)
		//		{
		//			var id = pp.LicenseNumber;
		//		}
		//	}
		//	foreach (var obj in members)
		//	{
		//		if(obj is Pilot)
		//		{
		//			var ps = obj as Pilot;
		//			foreach(var pp in ps.Licenses)
		//			{
		//				var id = pp.LicenseNumber; 
		//			}
		//		}
		//		var o = (obj as Member).FirstName;
		//	}
			
		//}
		[Test]
		public void CreateMembers()
		{
			AddressBuilder builderAddress = new AddressBuilder();
			MemberBuilder builder = new MemberBuilder();
			Pilot member = builder.WithDefaultValues();
			//member.Contact.AddUpdateAddress(builderAddress.WithDefaultValues());
			//member.Contact.AddUpdateEmail(new EMAIL() { Id = 1, EMail = "yos.1965@gmail.com" });
			//member.Contact.AddUpdatePhone(new Phone() { Id = 1, PhoneNumber = "0549050750", Type = ContactType.HOME });
			//club.AddMember(member);
			//Pilot p = new Pilot();
			//p.Id = 1;
			//club.AddMember(p);
			
		}
	}
}
