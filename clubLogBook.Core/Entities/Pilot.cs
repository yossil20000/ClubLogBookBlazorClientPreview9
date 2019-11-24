using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	
	public interface IPilot<TLicense, TEndorsment, TCheckride> : IMember
	{
		

		IReadOnlyCollection<TLicense> Licenses { get; }
		IReadOnlyCollection<TEndorsment> Endorsments { get; }

		IReadOnlyCollection<TCheckride> Checkrides { get;  }
		// virtual  List<Club> Clubs { get; set; }


	}
	public class Pilot : Member, IPilot<License, Endorsment, Checkride>
	{
		private List<License> licenses = new List<License>();
		private List<Checkride> checkrides = new List<Checkride>();
		private List<Endorsment> endorsments = new List<Endorsment>();
		
		public Pilot() { }
		public Pilot(int id, string idNumber, string firstName, string lastname, Gender gender) : base( id, idNumber, firstName,  lastname,  gender) 
		{

			

		}

		public Pilot(Member member) :base(member)
		{
			
		}

		public void AddLicense(License license)
		{
			if(!licenses.Any(i => i.Id == license.Id))
			{
				licenses.Add(license);
			}
			return;
		}
		public void AddEndorsment(Endorsment endorsment)
		{
			if(!Endorsments.Any(i => i.Id == endorsment.Id))
			{
				endorsments.Add(endorsment);
			}
			return;
		}
		public void AddCheckride(Checkride checkride)
		{
			if(!checkrides.Any(i => i.Id == checkride.Id))
			{
				checkrides.Add(checkride);
			}
			return;
		}
		public IReadOnlyCollection<License> Licenses => licenses.AsReadOnly();
		public IReadOnlyCollection<Endorsment> Endorsments => endorsments.AsReadOnly();

		public IReadOnlyCollection<Checkride> Checkrides => checkrides.AsReadOnly();
		//public virtual  List<Club> Clubs { get; set; }
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			//sb.Append($"{this.ToString()}");
			sb.Append($"{base.ToString()}");
			
			
			return sb.ToString();
		}
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (this.GetType() != obj.GetType()) return false;

			Pilot other = (Pilot)obj;
			if (other.IdNumber == this.IdNumber)
			{
				return true;
			}
			else
			{
				return false;
			}

		}
		public Member GetBase()
		{
			return (Member)this;
		}
	}
}
