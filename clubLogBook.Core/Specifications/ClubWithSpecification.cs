using System;
using System.Collections.Generic;
using System.Linq;
using ClubLogBook.Core.Entities;
namespace ClubLogBook.Core.Specifications
{
	[Flags]
	public enum ClubServiceSpec : int
	{
		IncludeAddress = 1,
		IncludePhone = 2,
		IncludeEmail = 4,
		IncludeAirplanes = 8
	}
	public sealed class ClubWithSpecification : BaseSpecification<Club>
	{
		
		public ClubWithSpecification(int clubId) : base(b => b.Id == clubId)
		{
			
		}
		public ClubWithSpecification(string clubName , bool includeMembers = false , bool includeAircraft = false) : base(b => b.Name == clubName)
		{
			if(includeAircraft)
				AddInclude($"{nameof(Club.Aircrafts)}.{nameof(Aircraft.AirCraftModel)}");
			if (includeMembers)
			{
				AddInclude($"{nameof(Club.Members)}.{nameof(Member.Contact)}.{nameof(Contact.Addresses)}");
				AddInclude($"{nameof(Club.Members)}.{nameof(Member.Contact)}.{nameof(Contact.EMAILs)}");
				//AddInclude($"{nameof(Club.Members)}.{nameof(Member.Contact)}.{nameof(Contact.Phones)}");
			}

			//AddInclude(b => b.Members);
		}
		public ClubWithSpecification(string clubName, ClubServiceSpec clubServiceSpec) : base(b => b.Name == clubName)
		{
			switch(clubServiceSpec)
			{
				case ClubServiceSpec.IncludeAirplanes:
					AddInclude($"{nameof(Club.Aircrafts)}.{nameof(Aircraft.AirCraftModel)}");
					break;
				case ClubServiceSpec.IncludeAddress:
					AddInclude($"{nameof(Club.Members)}.{nameof(Member.Contact)}.{nameof(Contact.Addresses)}");
					break;
				case ClubServiceSpec.IncludeEmail:
					AddInclude($"{nameof(Club.Members)}.{nameof(Member.Contact)}.{nameof(Contact.EMAILs)}");
					break;
				case ClubServiceSpec.IncludePhone:
					AddInclude($"{nameof(Club.Members)}.{nameof(Member.Contact)}.{nameof(Contact.Phones)}");
					break;
			}
			

			//AddInclude(b => b.Members);
		}
	}
}
