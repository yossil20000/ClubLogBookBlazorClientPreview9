using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public class Phone : BaseEntity
	{
		public Phone() { Id = 0; }
		public string CountryCode { get; set; } = "972";
		public string AreaCode { get; set; } = "054";
		public string PhoneNumber { get; set; }
		public ContactType Type { get; set; }
		public override int GetHashCode()
		{
			return $"{CountryCode}{AreaCode}{PhoneNumber}{Type}".GetHashCode();
		}

	}
}
