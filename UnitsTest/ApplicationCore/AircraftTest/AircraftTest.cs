using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Exctensions;
using ClubLogBook.Core.Interfaces;
using UnitsTest.ApplicationCore.Builder;
using UnitsTest.ApplicationCore.MemberTest;
namespace UnitsTest.ApplicationCore.AircraftTest
{
	
	public class AircraftTest
	{
		public Aircraft aircraft;
		[Test]
		public void AircraftBuild()
		{
			AircraftModelBuilder builder = new AircraftModelBuilder();
			AirCraftModel model = builder.BuildAircraftModel();
			model = builder.RandomValue();
			aircraft = builder.BuildAircraft();

		}
	}
	public class ClubTest
	{
		public Club club;
		public ClubTest()
		{
			club = new Club();
			club.Name = "Baz";
			club.Id = 1;

		}
		[Test]
		public void ClubBuildTest()
		{
			AircraftTest aircraftTest = new AircraftTest();
			ClubAddMemberTest clubAddMemberTest = new ClubAddMemberTest();
			aircraftTest.AircraftBuild();
			club.AddAircraft(aircraftTest.aircraft);

			clubAddMemberTest.CreateMembers();
			foreach(var m in clubAddMemberTest.club.Members)
			{
				club.AddMember(m );
			}
			

		}
	}
}
