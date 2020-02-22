using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Core.Common;

namespace ClubLogBook.Core.Entities
{
	public interface IClub : IAuditableEntity, IAggregateRoot
	{
		
		string Name { get; set; }
		

	}
	public class Club : AuditableEntity, IClub
	{
		public Club()
		{
			Name = "";
			Members = new HashSet<Pilot>();
			Aircrafts = new HashSet<Aircraft>();
		}

		public string Name { get; set; }
		//private HashSet<Pilot> _ClubMembers { get; }
		//private HashSet<Aircraft> _ClubAircrafts { get; }
		public void AddMember(Pilot member)
		{

			if (!Members.Any(i => i.IdNumber == member.IdNumber))
			{
				Members.Add(member);
			}
			return;
		}
		public void AddAircraft(Aircraft aircraft)
		{
			if (!Aircrafts.Any(i => i.Id == aircraft.Id))
			{
				Aircrafts.Add(aircraft);
			}
			return;
		}

		public virtual ICollection<Pilot> Members { get; set; }
		public virtual ICollection<Aircraft> Aircrafts { get; set; }
	}
	//public class Club : AuditableEntity, IClub
	//{
	//	public Club()
	//	{
	//		Name = "";
	//		_ClubMembers = new HashSet<Pilot>();
	//		_ClubAircrafts = new HashSet<Aircraft>();
	//	}

	//	public string Name { get; set; }
	//	private HashSet<Pilot> _ClubMembers { get;  }
	//	private HashSet<Aircraft> _ClubAircrafts { get;  }
	//	public void AddMember(Pilot member)
	//	{

	//		if(!_ClubMembers.Any(i => i.IdNumber == member.IdNumber))
	//		{
	//			_ClubMembers.Add(member);
	//		}
	//		return;
	//	}
	//	public void AddAircraft(Aircraft aircraft)
	//	{
	//		if(!_ClubAircrafts.Any(i => i.Id == aircraft.Id))
	//		{
	//			_ClubAircrafts.Add(aircraft);
	//		}
	//		return;
	//	}

	//	public virtual ICollection<Pilot> Members => _ClubMembers;
	//	public virtual ICollection<Aircraft> Aircrafts => _ClubAircrafts;
	//}
}
