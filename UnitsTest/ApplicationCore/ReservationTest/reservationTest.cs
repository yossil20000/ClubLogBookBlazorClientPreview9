using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Exctensions;
using ClubLogBook.Core.Interfaces;
using ClubLogBook.Infrastructure.Data;
using UnitsTest.ApplicationCore.Builder;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Infrastructure.Data.Import;
using UnitsTest.ApplicationInfrastructure;
using ClubLogBook.Core.Services;
using System.Threading.Tasks;
using ClubLogBook.Core.Specifications;
using ClubLogBook.Server.Interfaces;
namespace UnitsTest.ApplicationCore.Services
{
	public class ReservationTest
	{
		public ReservationService cr;
		public MemberService mr;
		ClubLogbookContext _context;
		[Test]
		public async Task ClubLogbookContextTest()
		{
			ImportDataTest import = new ImportDataTest();
			import.InitContext();
			try
			{
				_context = import._context;
				var membersVar = _context.Members;
				List<Pilot> members = await _context.Members.ToListAsync();
				
				
				List<Club> list2 = await _context.Clubs.Include(a => a.Members).ThenInclude(a => a.Contact).ToListAsync();
				
				List<Club> list = await _context.Clubs.Include(a => a.Aircrafts).ThenInclude(a => a.AirCraftModel).ToListAsync();
				var cl = list.FirstOrDefault();
				var list1 = await _context.Clubs.ToListAsync();
				ContactBook contactBooks = await _context.ContactBooks.Include(i => i.Contacts).ThenInclude(ii => ii.Addresses).SingleOrDefaultAsync();
				contactBooks = await _context.ContactBooks.Include(i => i.Contacts).ThenInclude(ii => ii.Phones).SingleOrDefaultAsync();
				List<Address> addresses = contactBooks.Contacts.SelectMany(i => i.Addresses).ToList();
				List<Phone> phones = contactBooks.Contacts.SelectMany(i => i.Phones).ToList();
				var clmem = cl.Members;

				//cl.Members = members;

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

		}

		[Test]
		public async Task ClubServiceTest()
		{
			ImportDataTest import = new ImportDataTest();
			import.InitContext();
			try
			{
				_context = import._context;
				IReservationRepository reservationRepository = new ReservationRepository(_context);
				cr = new ReservationService(reservationRepository);
				AircraftReservation ar = new AircraftReservation();
				ar.DateFrom = DateTime.Now;
				ar.DateTo = DateTime.Now.AddHours(1);
				ar.TailNumber = "4xCGC";
				ar.IdNumber = "123456";
				_context.AircraftReservations.Add(ar);
				_context.SaveChanges();
				var reser = await cr.GetReservation();
				await cr.AddReservation(ar);

				ar.IdNumber = "098765";
				await cr.EditReservation(ar);

				reser = await cr.GetAircraftReservation(ar.TailNumber);
				var aa = await  cr.AddReservation(ar);
				AircraftReservation ab = new AircraftReservation();
				ab.DateFrom = ar.DateTo;
				ab.DateTo = ab.DateFrom.AddHours(5);
				ab.TailNumber = ar.TailNumber;
				aa = await cr.AddReservation(ab);
				reser = await cr.GetReservation();
				reser = await cr.GetPilotReservation(ar.IdNumber);
				await cr.DeleteReservation(ar.Id);
				reser = await cr.GetReservation();


			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			


		}
		[Test]
		public async Task ClubServiceAddTest()
		{
			ImportDataTest import = new ImportDataTest();
			import.InitContext();
			try
			{
				_context = import._context;
				IReservationRepository reservationRepository = new ReservationRepository(_context);
				IMemberRepository memberRepository = new MemberRepository(_context);
				mr = new MemberService(memberRepository);
				Pilot p = await  mr.GetPilotById(15);
				cr = new ReservationService(reservationRepository);
				AircraftReservation ar = new AircraftReservation();
				ar.DateFrom = new DateTime(2019, 08, 29, 14, 23, 00);
				ar.DateTo = new DateTime(2019, 08, 29, 19, 20, 00);
				ar.TailNumber = "4xCGC";
				ar.IdNumber = "123456";
				ar.AircraftId = 8;

				ar.ReservationInfo = new UserInfo(p).GetJason();
				var reser = await cr.GetReservation();
				await cr.AddReservation(ar);
				ar.DateFrom = new DateTime(2019, 08, 29, 19, 22, 00);
				ar.DateTo = new DateTime(2019, 08, 29, 20, 20, 00);
				await cr.AddReservation(ar);
				reser = await cr.GetReservation();
				reser = await cr.GetPilotReservation(ar.IdNumber);
				await cr.DeleteReservation(ar.Id);
				reser = await cr.GetReservation();
				await cr.AddReservation(ar);

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}



		}
		//[Test]
		//public async Task ClubServiceAircrafTest()
		//{
		//	ImportDataTest import = new ImportDataTest();
		//	import.InitContext();
		//	try
		//	{
		//		_context = import._context;
		//		cr = new ClubRepository(_context);
		//		ClubService clubService = new ClubService(cr);
		//		ICollection<Aircraft> aircraft = await clubService.GetClubAircraft("BAZ");
		//		foreach(var ar in aircraft)
		//		{
		//			System.Diagnostics.Debug.WriteLine(ar.ToString());
		//		}


		//	}
		//	catch (Exception ex)
		//	{
		//		System.Diagnostics.Debug.WriteLine(ex.Message);
		//	}



		//}
		//[Test]
		//public async Task ClubServiceAircrafFlightTest()
		//{
		//	ImportDataTest import = new ImportDataTest();
		//	import.InitContext();
		//	try
		//	{
		//		_context = import._context;
		//		cr = new ClubRepository(_context);
		//		AircraftLogBookRepository acr = new AircraftLogBookRepository(_context);
		//		FlightRepository fr = new FlightRepository(_context);
		//		MemberRepository mr = new MemberRepository(_context);
		//		ClubService clubService = new ClubService(cr,mr,fr,acr,null);
		//		int aircraftId = 8;

		//		ICollection<Flight> flights = await clubService.GetClubAircraftFlight("BAZ", aircraftId);
		//		var group = flights.GroupBy(o => o.Pilot);
		//		foreach (var g in group)
		//		{
		//			System.Diagnostics.Debug.WriteLine(g.FirstOrDefault()?.Pilot?.FirstName);
		//			foreach (var gi in g)
		//			{
		//				System.Diagnostics.Debug.WriteLine(gi.ToString());
		//			}

		//		}


		//	}
		//	catch (Exception ex)
		//	{
		//		System.Diagnostics.Debug.WriteLine(ex.Message);
		//	}



		//}
		//[Test]
		//public async Task FlighRecordServiceTest()
		//{
		//	ImportDataTest import = new ImportDataTest();
		//	_context = import._context;
		//	//IFlightRecordViewModelService _flightRecordViewModelService = new FlightRecordViewModelService();
		//}
		//[Test]
		//public async Task RepositoryClubAircrafFlightTest()
		//{
		//	ImportDataTest import = new ImportDataTest();
		//	import.InitContext();
		//	try
		//	{
		//		_context = import._context;
		//		cr = new ClubRepository(_context);
		//		AircraftLogBookRepository acr = new AircraftLogBookRepository(_context);
		//		FlightRepository fr = new FlightRepository(_context);
		//		MemberRepository mr = new MemberRepository(_context);
		//		ClubRepository _clubRepository = new ClubRepository(_context);

		//		ClubService clubService = new ClubService(cr, mr, fr, acr, null);
		//		int aircraftId = 8;
		//		var clubSpec = new ClubWithSpecification("BAZ", false, true);

		//		var club = (await _clubRepository.ListAsync(clubSpec));
		//		FlighWithSpecification flightPagingSpec = new FlighWithSpecification(0, 10, 8, 14);
		//		FlighWithSpecification flightSpec = new FlighWithSpecification( 8, 14);

		//		var flight = await fr.ListAsync(flightPagingSpec);
		//		var flightCount = await fr.CountAsync(flightSpec);

		//		ICollection<Flight> flights = await clubService.GetClubAircraftFlight("BAZ", aircraftId);
		//		var group = flights.GroupBy(o => o.Pilot);
		//		foreach (var g in group)
		//		{
		//			System.Diagnostics.Debug.WriteLine(g.FirstOrDefault()?.Pilot?.FirstName);
		//			foreach (var gi in g)
		//			{
		//				System.Diagnostics.Debug.WriteLine(gi.ToString());
		//			}

		//		}


		//	}
		//	catch (Exception ex)
		//	{
		//		System.Diagnostics.Debug.WriteLine(ex.Message);
		//	}



		//}

		//[Test]
		//public async Task PilotRepositoryTest()
		//{
		//	ImportDataTest import = new ImportDataTest();
		//	import.InitContext();
		//	try
		//	{
		//		_context = import._context;
		//		EFRepository<Pilot> eFRepository = new EFRepository<Pilot>(_context);
		//		var pilots = await eFRepository.ListAllAsync();
		//		System.Diagnostics.Debug.WriteLine($"{pilots.Count}");
		//		foreach (var p in pilots)
		//		{

		//			System.Diagnostics.Debug.WriteLine(p.FirstName);
		//		}
		//		IAsyncRepository<Pilot> asyncRepositoryPilot = new EFRepository<Pilot>(_context);
		//		PilotWithSpeciification spec = new PilotWithSpeciification(0, pilots.Count, null, "g","");
		//		var pilot =   await asyncRepositoryPilot.ListAsync(spec);
		//		System.Diagnostics.Debug.WriteLine($"{pilot.Count} {spec.ToString()}");
		//		foreach (var p in pilot)
		//		{

		//			System.Diagnostics.Debug.WriteLine($"{p.ToString()}");
		//		}
		//		spec = new PilotWithSpeciification(0, pilots.Count, null, "y", "05982");
		//		pilot = await asyncRepositoryPilot.ListAsync(spec);
		//		System.Diagnostics.Debug.WriteLine($"{pilot.Count} {spec.ToString()}");
		//		foreach (var p in pilot)
		//		{

		//			System.Diagnostics.Debug.WriteLine($"{p.ToString()}");
		//		}
		//		var p1 = await asyncRepositoryPilot.GetByIdAsync(12);
		//		System.Diagnostics.Debug.WriteLine($"GetByIdAsync : {p1?.ToString()}");
		//		var count = await  asyncRepositoryPilot.CountAsync(spec);
		//		System.Diagnostics.Debug.WriteLine($"CountAsync:{count}");
		//		pilot = await asyncRepositoryPilot.ListAllAsync();
		//		System.Diagnostics.Debug.WriteLine($"ListAllAsync: {pilot.Count} {spec.ToString()}");
		//		foreach (var p in pilot)
		//		{

		//			System.Diagnostics.Debug.WriteLine($"{p.ToString()}");
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		System.Diagnostics.Debug.WriteLine(ex.Message);
		//	}



		//}
	}
}
