using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public class Phone : AuditableEntity
	{
		public Phone() { Id = 0; }
		public string CountryCode { get; set; } = "972";
		public string AreaCode { get; set; } = "054";
		public string PhoneNumber { get; set; } = string.Empty;
		public ContactType Type { get; set; } = ContactType.HOME;
		public override int GetHashCode()
		{
			return $"{CountryCode}{AreaCode}{PhoneNumber}{Type}".GetHashCode();
		}

	}
}
