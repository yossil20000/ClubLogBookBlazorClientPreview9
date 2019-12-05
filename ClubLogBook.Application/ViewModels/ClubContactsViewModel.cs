using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace ClubLogBook.Application.ViewModels
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
		
		public static  string GetFullAddress(this AddressViewModel address)
		{
			

			return $"{address.Street} {address?.City} {address?.State} {address?.Zipcode} {address?.Country}";
		}
	}
	public class ClubContactsViewModel 
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
		public List<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();
		public List<EMAILVieModel> Emails { get; set; } = new List<EMAILVieModel>();
		public List<AddressViewModel> Addresses { get; set; } = new List<AddressViewModel>();
		
		//[Display(Name ="eMail")]
		//public string Mail { get; set; }
		public string GetFullName()
		{
			return $"{FirstName} {LastName}";
		}
		public Decimal Height { get; set; }
		public Decimal Weight { get; set; }
		public string UserId { get; set; } = String.Empty;
		public ClubContactsViewModel() { }
		
		
	}
	public class AddressViewModel

	{
		public int Id { get; set; }
		public AddressViewModel()
		{
			Street = "";
			Zipcode = "";
			City = "";
			Id = 0;
			State = "IL";
			Country = "Israel";
			Zipcode = "0";
		}
		public AddressViewModel(string street, string city, string state, string country, string zipCode, ContactType contactType)
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

	}
	public class EMAILVieModel 
	{
		public int Id { get; set; }
		public EMAILVieModel() { Id = 0; }
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
	}
	public class PhoneViewModel 
	{
		public int Id { get; set; }
	
		public PhoneViewModel() { Id = 0; }
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
	}
}
