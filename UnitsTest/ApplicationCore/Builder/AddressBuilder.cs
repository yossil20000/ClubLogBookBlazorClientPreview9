using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
namespace UnitsTest.ApplicationCore.Builder
{
	public class AddressBuilder
	{
		private Address _address;
		public string TestStreet => "123 Main St.";
		public string TestCity => "Kent";
		public string TestState => "OH";
		public string TestCountry => "USA";
		public string TestZipCode => "44240";
		public ContactType TestContactType => ContactType.HOME;
		public AddressBuilder()
		{
			_address = WithDefaultValues();
		}
		public Address Build()
		{
			return _address;
		}
		public Address WithDefaultValues()
		{
			_address = new Address( TestStreet, TestCity, TestState, TestCountry, TestZipCode, TestContactType);
			return _address;
		}
		public Address RandomValue()
		{
			_address = new Address(RandomGenerator.RandomString(5,true) + " " + RandomGenerator.RandomNumber(1,100), RandomGenerator.RandomString(5, true), RandomGenerator.RandomString(6, true), RandomGenerator.RandomString(3, true), RandomGenerator.RandomString(5, true), RandomGenerator.RandomNumber(0, 1) == 0 ? ContactType.HOME : ContactType.WORK);
			return _address;
		}
		
	}
}
