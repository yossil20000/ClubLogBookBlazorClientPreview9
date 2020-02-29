using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ClubLogBook.Core.Common;
using ClubLogBook.Core.Interfaces;

namespace ClubLogBook.Core.Entities
{
	public class Contact : AuditableEntity, IAggregateRoot
	{
		//private List<Address> _addresses { get; set; } = new List<Address>();
		//private List<EMAIL> _eMails { get; set; } = new List<EMAIL>();
		//private List<Phone> _phones { get; set; } = new List<Phone>();
		public Contact() {
			Addresses = new HashSet<Address>();
			EMAILs = new HashSet<EMAIL>();
			Phones = new HashSet<Phone>();
		}
		public virtual ICollection<Address> Addresses { get; set; }
		public virtual ICollection<EMAIL> EMAILs { get; set; }
		public virtual ICollection<Phone> Phones { get; set; }
		[Timestamp]
		public byte[] RowVersion { get; set; }
		//public void SetPhones(ICollection<Phone> phone)
		//{
		//	Phones = phone.ToList();
		//}
		//public void AddAddress(string street, string city, string state, string country, string zipCode, ContactType contactType)
		//{
		//	Address address = new Address(street, city, state, country, zipCode, contactType);
		//	address.Id = Addresses.Count == 0 ? 1 : Addresses.Max(i => i.Id) + 1;
		//	Addresses.Add(address);

		//}
		//public void AddAddress(Address address)
		//{

		//	//address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
		//	Addresses.Add(address);

		//}
		//public void AddPhone(Phone phone)
		//{

		//	//address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
		//	Phones.Add(phone);

		//}
		//public void AddEmail(EMAIL eMAIL)
		//{

		//	//address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
		//	EMAILs.Add(eMAIL);

		//}
		
		//public void AddUpdateAddress(Address address)
		//{

		//	var exsisting = Addresses.FirstOrDefault(i => i.GetHashCode() == address.GetHashCode());
		//	if (exsisting != null)
		//		exsisting = address;
		//	else
		//		Addresses.Add(address);
		//}
		//public Address GetAddress(int id)
		//{
		//	return Addresses.FirstOrDefault(i => i.Id == id);
		//}
		//public void AddUpdateEmail(EMAIL eMAIL)
		//{

		//	var exsisting = EMAILs.FirstOrDefault(i => i.GetHashCode() == eMAIL.GetHashCode());
		//	if (exsisting != null)
		//		exsisting = eMAIL;
		//	else
		//		EMAILs.Add(eMAIL);
		//}
		//public void AddUpdatePhone(Phone phone)
		//{

		//	var exsisting = Phones.FirstOrDefault(i => i.GetHashCode() == phone.GetHashCode());
		//	if (exsisting != null)
		//		exsisting = phone;
		//	else
		//		Phones.Add(phone);
		//}
	}
	//public class Contact : AuditableEntity , IAggregateRoot
	//{
	//	private List<Address> _addresses { get; set; } = new List<Address>();
	//	private List<EMAIL> _eMails { get; set; } = new List<EMAIL>();
	//	private List<Phone> _phones { get; set; } = new List<Phone>();
	//	public Contact() { }
	//	public virtual ICollection<Address> Addresses => _addresses;
	//	public virtual ICollection<EMAIL> EMAILs => _eMails;
	//	public virtual ICollection<Phone> Phones => _phones;
	//	public void SetPhones(ICollection<Phone> phone)
	//	{
	//		_phones = phone.ToList();
	//	}
	//	public void AddAddress(string street, string city, string state, string country, string zipCode, ContactType contactType)
	//	{
	//		Address address = new Address(street, city, state, country, zipCode, contactType);
	//		address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
	//		_addresses.Add(address);

	//	}
	//	public void AddAddress(Address address)
	//	{

	//		//address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
	//		_addresses.Add(address);

	//	}
	//	public void AddPhone(Phone phone)
	//	{

	//		//address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
	//		_phones.Add(phone);

	//	}
	//	public void AddEmail(EMAIL eMAIL)
	//	{

	//		//address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
	//		_eMails.Add(eMAIL);

	//	}
	//	public void DeleteAddress(int id)
	//	{
	//		_addresses.RemoveAll(i => i.Id == id);

	//	}
	//	public void AddUpdateAddress(Address address)
	//	{

	//		var exsisting = _addresses.FirstOrDefault(i => i.GetHashCode() == address.GetHashCode());
	//		if (exsisting != null)
	//			exsisting = address;
	//		else
	//			_addresses.Add(address);
	//	}
	//	public Address GetAddress(int id)
	//	{
	//		return _addresses.FirstOrDefault(i => i.Id == id);
	//	}
	//	public void AddUpdateEmail(EMAIL eMAIL)
	//	{

	//		var exsisting = _eMails.FirstOrDefault(i => i.GetHashCode() == eMAIL.GetHashCode());
	//		if (exsisting != null)
	//			exsisting = eMAIL;
	//		else
	//			_eMails.Add(eMAIL);
	//	}
	//	public void AddUpdatePhone(Phone phone)
	//	{

	//		var exsisting = _phones.FirstOrDefault(i => i.GetHashCode() == phone.GetHashCode());
	//		if (exsisting != null)
	//			exsisting = phone;
	//		else
	//			_phones.Add(phone);
	//	}
	//}
}
