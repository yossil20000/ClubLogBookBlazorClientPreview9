using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Entities
{
	public class ContactBook : BaseEntity, IAggregateRoot
	{
		private readonly List<Address> _addresses = new List<Address>();
		public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
		
		public ContactBook() { }
		public void AddAddress(string street, string city, string state, string country, string zipCode, ContactType contactType)
		{
			Address address = new Address(street, city, state, country, zipCode, contactType);
			address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id)+1 ;
			_addresses.Add(address);
			
		}
		public void AddAddress(Address address)
		{
			
			address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(i => i.Id) + 1;
			_addresses.Add(address);

		}
		public void DeleteAddress(int id)
		{
			_addresses.RemoveAll(i => i.Id == id);
				
		}
		public void UpdateAddress(Address address)
		{
			var exsisting = _addresses.FirstOrDefault(i => i.Id == address.Id);
			if(exsisting != null)
				exsisting = address;
		}
		public Address GetAddress(int id)
		{
			return _addresses.FirstOrDefault(i => i.Id == id);
		}
	}
}
