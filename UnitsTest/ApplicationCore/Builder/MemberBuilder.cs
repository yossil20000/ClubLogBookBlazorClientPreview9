using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;

namespace UnitsTest.ApplicationCore.Builder
{
	public class MemberBuilder
	{
		private Pilot _lastBuild;
		public string TestIdNumber = "059828392";
		public string TestName = "Yosef";
		public string TestLastName = "Levy";
		public string TestMiddleName = "Menahem";
		public Gender TestGender = Gender.Male;
		public DateTime TestDateOfBirth = new DateTime(1965, 08, 21);
		public byte[] TestPhoto = null;
		public Decimal TestHeight = 172;
		public Decimal TestWeight = 77;
		public MemberBuilder()
		{
			_lastBuild = WithDefaultValues();
		}
		public Member Build()
		{
			return _lastBuild;
		}
		public Pilot WithDefaultValues()
		{
			_lastBuild = new Pilot(0,TestIdNumber,TestName,TestLastName,TestGender);
			_lastBuild.MiddleName = TestMiddleName;
			_lastBuild.DateOfBirth = TestDateOfBirth;
			_lastBuild.Height = TestHeight;
			_lastBuild.Weight = TestWeight;
			return _lastBuild;
		}
		public Member RandomValue()
		{
			_lastBuild = new Pilot(0,RandomGenerator.RandomNumber(100000,999999).ToString(), RandomGenerator.RandomString(5, true),RandomGenerator.RandomString(7,true),RandomGenerator.RandomNumber(0, 1) == 0 ? Gender.Female : Gender.Male);
			_lastBuild.MiddleName = RandomGenerator.RandomString(RandomGenerator.RandomNumber(2,8),true);
			_lastBuild.DateOfBirth = new DateTime(RandomGenerator.RandomNumber(1920, 2019), RandomGenerator.RandomNumber(1, 12), RandomGenerator.RandomNumber(1, 28));
			_lastBuild.Height = RandomGenerator.RandomNumber(120,200);
			_lastBuild.Weight = RandomGenerator.RandomNumber(45, 130);
			
			return _lastBuild;
		}
	}
}
