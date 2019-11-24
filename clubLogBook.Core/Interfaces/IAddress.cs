using System;
using System.Collections.Generic;
using System.Text;

namespace ClubLogBook.Core.Interfaces
{
	public interface IAddress
	{
		string MailAddress { get; set; }
		string City { get; set; }
		int Zipcode { get; set; }
		string State { get; set; }
		string Country { get; set; }
		ContactType Type { get; set; }

	}
}
