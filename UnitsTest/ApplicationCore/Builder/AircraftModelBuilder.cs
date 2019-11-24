using System;
using System.Collections.Generic;
using System.Text;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;

namespace UnitsTest.ApplicationCore.Builder
{
	public class AircraftModelBuilder
	{
		private AirCraftModel _lastBuild;
		private Aircraft _lastAircraftBuild;
		public AircraftModelBuilder()
		{
			_lastBuild = new AirCraftModel();

			_lastAircraftBuild = new Aircraft();
			_lastAircraftBuild.AirCraftModel = _lastBuild;
		}
		public AirCraftModel BuildAircraftModel()
		{
			return _lastBuild;
		}
		public Aircraft BuildAircraft()
		{
			return _lastAircraftBuild;
		}
		public AirCraftModel WithDefaultValues()
		{
			return _lastBuild;
		}
		public AirCraftModel RandomValue()
		{
			_lastBuild.EngineTime = RandomGenerator.RandomNumber(0, 1000);
			_lastBuild.FirstFlight = new DateTime(1965, 01, 01);
			_lastBuild.Last100 = _lastBuild.EngineTime - RandomGenerator.RandomNumber(0, 99);
			_lastBuild.LastAltimeter = DateTime.Now.AddMonths(-RandomGenerator.RandomNumber(1, 6));
			_lastBuild.LastAnnual = DateTime.Now.AddMonths(-RandomGenerator.RandomNumber(1, 6));
			_lastBuild.LastELT = DateTime.Now.AddMonths(-RandomGenerator.RandomNumber(1,6));
			_lastBuild.LastFlight = DateTime.Now;
			_lastBuild.Manufacturer = "Cessna";
			_lastBuild.Model = "C172";
			
			return _lastBuild;
		}
	}
}
