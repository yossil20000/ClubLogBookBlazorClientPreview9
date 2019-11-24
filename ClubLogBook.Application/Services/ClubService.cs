using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Specifications;
namespace ClubLogBook.Application.Services
{
	public class ClubService : IClubService
	{
		private readonly IClubRepository _clubRepository;
		private readonly IMemberRepository _clubMemberRepository;
		private readonly IAircraftLogBookRepository _aircraftLogbookRepository;
		private readonly IAppLogger<ClubService> _logger;
		private readonly IFlightRepository _flightRepository;
		private readonly IAsyncRepository<Aircraft> _asyncRepositoryAircraft;
		
		public ClubService(IClubRepository clubRepository, IMemberRepository clubMemberRepository,IFlightRepository flightRpository,IAsyncRepository<Aircraft> asyncRepositoryAircraft, IAircraftLogBookRepository aircraftLogbookRepository = null, IAppLogger<ClubService> logger = null)
		{
			_clubRepository = clubRepository;
			_clubMemberRepository = clubMemberRepository;
			_aircraftLogbookRepository = aircraftLogbookRepository;
			_logger = logger;
			_flightRepository = flightRpository;
			_asyncRepositoryAircraft = asyncRepositoryAircraft;
		}
		public ClubService(IClubRepository clubRepository)
		{
			_clubRepository = clubRepository;
			
		}
		public async Task<IReadOnlyCollection<Club>> GetClubs()
		{
			return await _clubRepository.ListAllAsync();
		}
		public async Task<ICollection<Pilot>> GetClubMembers(string clubName)
		{
			//List<Pilot> member = new List<Pilot>();
			var clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludeAddress);

			IReadOnlyList<Club> club1 =( await _clubRepository.ListAsync(clubSpec));
			clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludePhone);
			club1 = club1 = (await _clubRepository.ListAsync(clubSpec));
			clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludeEmail);
			club1 = club1 = (await _clubRepository.ListAsync(clubSpec));
			int clubId = club1.FirstOrDefault().Id;
			Club club =  _clubRepository.GetAllByIdAsync(clubId);
			//Club club = await _clubRepository.GetByIdAsync(clubId);
			//club = await _clubRepository.ListAllAsync();

			var mem =  club.Members;
			//member = new List<Member>(mem);
			//foreach(var item in club)
			//{
			//	foreach(var m in item.Members)
			//	{
			//		member.Add(m);
			//	}
			//}


			return mem;
		}
		public async Task<ICollection<Aircraft>> GetClubAircraft(string clubName)
		{
			//List<Pilot> member = new List<Pilot>();
			var clubSpec = new ClubWithSpecification(clubName);

			var club = (await _clubRepository.ListAsync(clubSpec));
			//int clubId = club1.FirstOrDefault().Id;
			//Club club = _clubRepository.GetAllByIdAsync(clubId);
			//Club club = await _clubRepository.GetByIdAsync(clubId);
			//club = await _clubRepository.ListAllAsync();

			var mem = club.FirstOrDefault().Aircrafts;
			//member = new List<Member>(mem);
			//foreach(var item in club)
			//{
			//	foreach(var m in item.Members)
			//	{
			//		member.Add(m);
			//	}
			//}


			return mem;
		}

		public async Task<IReadOnlyCollection<Club>> GetClubById(int id)
		{
			var clubspec = new ClubWithSpecification(id);
			var club = (await _clubRepository.ListAsync(clubspec));
			
			//List<Club> cc = new List<Club>(club);
			
			return club;
			
		}


		public async Task<Pilot> GetPilotById(int id)
		{
			return await _clubMemberRepository.GetByIdAsync(id);
		}
		public async Task<ICollection<Flight>> GetClubAircraftFlight(string clubName, int aircraftId)
		{
			var clubSpec = new ClubWithSpecification(clubName,false,true);

			var club = (await _clubRepository.ListAsync(clubSpec));
			//int clubId = club1.FirstOrDefault().Id;
			//Club club = _clubRepository.GetAllByIdAsync(clubId);
			//Club club = await _clubRepository.GetByIdAsync(clubId);
			//club = await _clubRepository.ListAllAsync();

			var mem = club.FirstOrDefault().Aircrafts.Where(i => i.Id == aircraftId).FirstOrDefault();
			var logBookSpec = new AircraftLogbookSpecification(mem?.TailNumber);
			var lb = await _aircraftLogbookRepository?.ListAsync(logBookSpec);
			var lba = lb?.FirstOrDefault();
			return lba?.Flights;
			
		}
		public async Task AddContactToClub(string clubName, Pilot pilot)
		{
			var exist = _clubMemberRepository.AddPilot(pilot);
			var clubSpec = new ClubWithSpecification(clubName);

			Club club = await ( _clubRepository.GetAllByIdAsync(clubSpec));
			//clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludePhone);
			//club1 = club1 = (await _clubRepository.ListAsync(clubSpec));
			//clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludeEmail);
			//club1 = club1 = (await _clubRepository.ListAsync(clubSpec));

			
			club.AddMember(pilot);
			
			
			
			await _clubRepository.AddAsync(club);
		}

		public async Task AddPilotToClub(string clubName, int pilotId)
		{
			var pilot = await _clubMemberRepository.GetByIdAsync(pilotId);
			var clubSpec = new ClubWithSpecification(clubName);

			Club club =  await (_clubRepository.GetAllByIdAsync(clubSpec));
			//clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludePhone);
			//club1 = club1 = (await _clubRepository.ListAsync(clubSpec));
			//clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludeEmail);
			//club1 = club1 = (await _clubRepository.ListAsync(clubSpec));

			if(club != null)
			club.AddMember(pilot);



			await _clubRepository.AddAsync(club);
		}

		public async Task RemoveContactFromClub(string clubName, int pilotId)
		{
			var clubSpec = new ClubWithSpecification(clubName);

			Club club = await (_clubRepository.GetAllByIdAsync(clubSpec));
			Pilot pilot = await _clubMemberRepository.GetByIdAsync(pilotId);
			club.Members.Remove(pilot);
			await _clubRepository.UpdateAsync(club);
		}
		public async Task DeleteFlight(int id)
		{
			await _flightRepository.DeleteFlight(id);
		}

		public async Task AddFlight(string clubName, Flight flight, int aircraftId, int pilotId)
		{
			var clubSpec = new ClubWithSpecification(clubName,false,true);
			
			

			var club = (await _clubRepository.ListAsync(clubSpec));
			var p = (await _clubMemberRepository.GetPilot(pilotId));
			//int clubId = club1.FirstOrDefault().Id;
			//Club club = _clubRepository.GetAllByIdAsync(clubId);
			//Club club = await _clubRepository.GetByIdAsync(clubId);
			//club = await _clubRepository.ListAllAsync();

			var mem = club.FirstOrDefault().Aircrafts.Where(i => i.Id == aircraftId).FirstOrDefault();
			flight.Pilot = p;
			flight.Aircraft = mem;
			var logbookflights = await GetClubAircraftFlight(clubName, aircraftId);

			var exist = logbookflights.Where(f => f.Equals(flight)).FirstOrDefault();
			if (exist != null)
				return;
			await _aircraftLogbookRepository.AddFlight(flight);
			 
			
		}

		public async Task UpdateFlight(Flight flight)
		{
			
			await _aircraftLogbookRepository.UpdateFlight(flight);
		}
		public async Task UpdatePilot(Pilot pilot)
		{
			
		}
		public async Task<ICollection<Aircraft>> GetClubAircrafts(string clubName)
		{
			//List<Pilot> member = new List<Pilot>();
			var clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludeAirplanes);

			IReadOnlyList<Club> club1 = (await _clubRepository.ListAsync(clubSpec));
			//clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludePhone);
			//club1 = club1 = (await _clubRepository.ListAsync(clubSpec));
			//clubSpec = new ClubWithSpecification(clubName, ClubServiceSpec.IncludeEmail);
			//club1 = club1 = (await _clubRepository.ListAsync(clubSpec));
			int clubId = club1.FirstOrDefault().Id;
			Club club = _clubRepository.GetAllByIdAsync(clubId);
			//Club club = await _clubRepository.GetByIdAsync(clubId);
			//club = await _clubRepository.ListAllAsync();

			var mem = club.Aircrafts;
			//member = new List<Member>(mem);
			//foreach(var item in club)
			//{
			//	foreach(var m in item.Members)
			//	{
			//		member.Add(m);
			//	}
			//}


			return mem;
		}

		public async Task<bool> AddAircraftToClub(string clubName, int aircraftId)
		{
			if (clubName == string.Empty || aircraftId <= 0)
				return await Task.FromResult(false);
			var clubSpec = new ClubWithSpecification(clubName);

			Club club = await (_clubRepository.GetAllByIdAsync(clubSpec));
			Aircraft aircraft = await _asyncRepositoryAircraft.GetByIdAsync(aircraftId);

			if (club != null && aircraft != null)
			{
				club.AddAircraft(aircraft);
				await _clubRepository.UpdateAsync(club);
				return await Task.FromResult(true);
			}
			return await Task.FromResult(false) ;
		}
		public async Task<bool> RemoveAircraftToClub(string clubName, int aircraftId)
		{
			if (clubName == string.Empty || aircraftId <= 0)
				return await Task.FromResult(false);
			var clubSpec = new ClubWithSpecification(clubName);

			Club club = await (_clubRepository.GetAllByIdAsync(clubSpec));
			Aircraft aircraft = await _asyncRepositoryAircraft.GetByIdAsync(aircraftId);

			if (club != null && aircraft != null)
			{
				club.Aircrafts.Remove(aircraft);
				await _clubRepository.UpdateAsync(club);
				return await Task.FromResult(true);
			}
			return await Task.FromResult(false);
		}
	}
}
