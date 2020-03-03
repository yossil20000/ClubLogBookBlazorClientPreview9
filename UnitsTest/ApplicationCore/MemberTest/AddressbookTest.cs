using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
using UnitsTest.ApplicationCore.Builder;

namespace UnitsTest.ApplicationCore.MemberTest
{
	
	public class AddressbookTest
	{
		public ContactBook addressBooks = new ContactBook();
		 int step = 0;
		[Test]
		public void AddressAddRemoveTest()
		{
			Contact contact = new Contact();
			//contact.Addresses.Add("Internal Box 248", "gilon", "Israel", "IL", "2010300", ContactType.HOME);
			//DumpAddressAddRemoveTest();
			//contact.AddAddress("Internal Box 248", "gilon", "Israel", "US", "2010300", ContactType.WORK);
			//DumpAddressAddRemoveTest();
			//var addUpdate = contact.GetAddress(1);
			//addUpdate.Street = "Brener 35";
			//addUpdate.City = "Holon";
			//contact.AddUpdateAddress(addUpdate);
			//DumpAddressAddRemoveTest();
			////contact.DeleteAddress(addUpdate.Id);
			//DumpAddressAddRemoveTest();
			//contact.AddAddress("Ofir 60", "gilon", "Israel", "US", "2010300", ContactType.HOME);
			////contact.DeleteAddress(addUpdate.Id);
			//DumpAddressAddRemoveTest();
			//addressBooks.AddContact(contact);
		}
		[Test]
		public void AddressAddRandom()
		{
			Contact contact = new Contact();
			//AddressBuilder builder = new AddressBuilder();
			//Address address = builder.WithDefaultValues();
			//contact.AddAddress(address);
			//DumpAddressAddRemoveTest();
			//contact.AddAddress(builder.RandomValue());
			//DumpAddressAddRemoveTest();
			//contact.AddAddress(builder.Build());
			//DumpAddressAddRemoveTest();
			//addressBooks.AddContact(contact);
		}
		private void DumpAddressAddRemoveTest()
		{
			System.Diagnostics.Debug.WriteLine($"{step++} ---");
			foreach (var contact in addressBooks.Contacts)
			{
				foreach(var address in contact.Addresses)
				{
					System.Diagnostics.Debug.WriteLine(address.ToString());
				}
				
			}
		}
	}
}
