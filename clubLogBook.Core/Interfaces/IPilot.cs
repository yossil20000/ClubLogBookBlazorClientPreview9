using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public interface IPilot<TMember ,TLicense, TEndorsment, TCheckride> : IBasicEntity
	{




		TMember Member { get; set; }

		ICollection<TLicense> Licenses { get; set; }
		ICollection<TEndorsment> Endorsments { get; set; }

		ICollection<TCheckride> Checkrides { get; set; }
		// virtual  List<Club> Clubs { get; set; }


	}
}
