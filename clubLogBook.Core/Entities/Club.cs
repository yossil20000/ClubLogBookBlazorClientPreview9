using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public interface IClub : IBasicEntity, IAggregateRoot
	{
		
		string Name { get; set; }
		

	}
	public class Club : BaseEntity, IClub
	{
		public Club()
		{
			//this.Aircrafts = new HashSet<Aircraft>();
			//Pilots = new HashSet<Pilot>();
			//Aircrafts = new HashSet<Aircraft>();
			Name = "";
			ClubMembers = new HashSet<Pilot>();
			ClubAircrafts = new HashSet<Aircraft>();
		}
		
		
		public string Name { get; set; }
		private HashSet<Pilot> ClubMembers { get;  }
		private HashSet<Aircraft> ClubAircrafts { get;  }
		public void AddMember(Pilot member)
		{

			if(!ClubMembers.Any(i => i.IdNumber == member.IdNumber))
			{
				ClubMembers.Add(member);
			}
			return;
		}
		public void AddAircraft(Aircraft aircraft)
		{
			if(!ClubAircrafts.Any(i => i.Id == aircraft.Id))
			{
				ClubAircrafts.Add(aircraft);
			}
			return;
		}

		public virtual ICollection<Pilot> Members => ClubMembers;
		public virtual ICollection<Aircraft> Aircrafts => ClubAircrafts;
	}
}
