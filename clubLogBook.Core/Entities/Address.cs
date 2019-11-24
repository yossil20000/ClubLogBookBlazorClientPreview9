using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	
	public class Address: BaseEntity

	{
		public Address()
		{
			Street = "";
			Zipcode = "";
			City = "";
			Id = 0;
			State = "IL";
			Country = "Israel";
			Zipcode = "0";
		}
		public Address(string street,string city,string state,string country,string zipCode, ContactType contactType)
		{
			
			Street = street;City = city;State = state;Country = country;Zipcode = zipCode;Type = contactType;
		}
		public string Street { get; set; } = "";
		public string Zipcode { get; set; } = "";

		public string City { get; set; } = "";

		public string State { get; set; } = "";
		public string Country { get; set; } = "";
		public ContactType Type { get; set; }
		public override string ToString()
		{
			return $"{Id}:{Street},{City},{Zipcode},{State},{Country},{Type}";
		}
		public override int GetHashCode()
		{
			return $"{Street}{Zipcode}{City}{Country}{State}{Type}".GetHashCode();
		}

	}
}
