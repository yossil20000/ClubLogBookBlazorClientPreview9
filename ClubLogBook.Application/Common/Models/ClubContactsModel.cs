using AutoMapper;
using ClubLogBook.Application.Interfaces.Mapping;
using ClubLogBook.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.Models
{
	public enum ContactType
	{
		HOME,
		WORK
	}
	public enum Gender
	{
		Male,
		Female
	}
	public static class AddressExtention
	{
		
		public static  string GetFullAddress(this AddressModel address)
		{
			

			return $"{address.Street} {address?.City} {address?.State} {address?.Zipcode} {address?.Country}";
		}
	}
	public class ClubContactsModel  : IHaveCustomMapping
	{
		public int Id { get; set; }
		[Display(Name ="Id Number")]
		[Required]
		public new  string   IdNumber { get; set; }
		[Display(Name ="Full Name")]
		public string FullName { get; set; }
		[Display(Name = "First Name")]
		[Required]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		[Required]
		public string LastName { get; set; }
		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }
		[Display(Name ="Date Of Birth")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
		public DateTime DateOfBirth { get; set; }
		[Required]
		public Gender Gender { get; set; }
		//[Display(Name ="Home Address")]
		//public string Address { get; set; }
		//public string Phone { get; set; }
		public List<PhoneModel> Phones { get; set; } = new List<PhoneModel>();
		public List<EMAILModel> Emails { get; set; } = new List<EMAILModel>();
		public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
		
		//[Display(Name ="eMail")]
		//public string Mail { get; set; }
		public string GetFullName()
		{
			return $"{FirstName} {LastName}";
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Pilot, ClubContactsModel>();
		}

		public Decimal Height { get; set; }
		public Decimal Weight { get; set; }
		public string UserId { get; set; } = String.Empty;
		public ClubContactsModel() { }
		
		
	}
	public class AddressModel : IHaveCustomMapping

	{
		public int Id { get; set; }
		public AddressModel()
		{
			Street = "";
			Zipcode = "";
			City = "";
			Id = 0;
			State = "IL";
			Country = "Israel";
			Zipcode = "0";
		}
		public AddressModel(string street, string city, string state, string country, string zipCode, ContactType contactType)
		{

			Street = street; City = city; State = state; Country = country; Zipcode = zipCode; Type = contactType;
		}
		public string Street { get; set; } = "";
		public string Zipcode { get; set; } = "";

		public string City { get; set; } = "";

		public string State { get; set; } = "";
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
		public string GetFormated()
		{


			return $"{Street} {City} {State} {Zipcode} {Country}";
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Contact, Address>();
			configuration.CreateMap<Address, AddressModel>();
		}
	}
	public class EMAILModel : IHaveCustomMapping
	{
		public int Id { get; set; }
		public EMAILModel() { Id = 0; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string EMail { get; set; } = "";
		[Required]
		public ContactType Type { get; set; } = ContactType.HOME;
		public override int GetHashCode()
		{
			return $"{EMail}{Type}".GetHashCode();
		}
		public string GetFormated()
		{
			return $"{EMail}";
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Contact, EMAIL>();
			configuration.CreateMap<EMAIL, EMAILModel>();
		}
	}
	public class PhoneModel : IHaveCustomMapping
	{
		public int Id { get; set; }
	
		public PhoneModel() { Id = 0; }
		[Required]
		public string CountryCode { get; set; } = "972";
		[Required]
		public string AreaCode { get; set; } = "054";
		[Required]
		public string PhoneNumber { get; set; } = "";
		[Required]
		public ContactType Type { get; set; } = ContactType.HOME;
		public override int GetHashCode()
		{
			return $"{CountryCode}{AreaCode}{PhoneNumber}{Type}".GetHashCode();
		}
		public string GetFormated()
		{
			return  $"{CountryCode}-{AreaCode}-{PhoneNumber}";
		}

		public void CreateMappings(Profile configuration)
		{
			configuration.CreateMap<Contact, Phone>();
			configuration.CreateMap<Phone, PhoneModel>();
		}
	}
}
