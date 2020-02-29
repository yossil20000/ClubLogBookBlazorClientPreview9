using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	
	public class Address: AuditableEntity

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
		[Required]
		[MaxLength(100)]
		public string Street { get; set; } = "";
		[Required]
		[MaxLength(15)]
		public string Zipcode { get; set; } = "";
		[Required]
		[MaxLength(30)]
		public string City { get; set; } = "";
		[Required]
		[MaxLength(3)]
		public string State { get; set; } = "";
		[Required]
		[MaxLength(30)]
		public string Country { get; set; } = "";
		public ContactType Type { get; set; } = ContactType.HOME;
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
