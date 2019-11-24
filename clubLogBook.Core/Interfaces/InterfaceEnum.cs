using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public enum ContactType
	{
		HOME,
		WORK
	}
	public enum Gender
	{
		Male,
		Female
	}
	
	[Flags]
	public enum SupportedClub : int
	{
		BAZ = 1,
		BAZ1 = 2,
		BOTH = BAZ & BAZ1
	}
	//[Flags]
	//public enum ClubRoll : int 
	//{
	//	Administrator =1,
	//	AccountManager = 2,
	//	ClubMember = 4,
	//	ClubPilot = 8 

	//}
}
