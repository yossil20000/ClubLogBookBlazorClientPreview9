using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Core.Interfaces;
namespace ClubLogBook.Core.Services
{
	public class AircraftManagerService : IAircraftManagerService
	{
		private readonly IClubRepository clubRepository;
		private readonly IAircraftRepository aircraftRepository;
		IAsyncRepository<AircraftPrice> asyncRepositoryAircraftPrice;
		public AircraftManagerService(IClubRepository clubRepository,IAircraftRepository aircraftRepository,IAsyncRepository<AircraftPrice> asyncRepositoryAircraftPrice)
		{
			this.clubRepository = clubRepository;
			this.aircraftRepository = aircraftRepository;
			this.asyncRepositoryAircraftPrice = asyncRepositoryAircraftPrice;
		}
		public  async Task<Aircraft> AddAircraft(Aircraft aircraft)
		{
			if(aircraft==null || aircraft.Id == 0)
			{
				return null;
			}
			else
			{
				return  await aircraftRepository.AddAsync(aircraft);
			}
			
		}

		

		public async Task<List<AircraftPrice>> GetAircraftPrices()
		{
			var a =  (await asyncRepositoryAircraftPrice.ListAllAsync()) as List<AircraftPrice>;
			return a;
		}

		public async Task<List<Aircraft>> GetAllAircraftsInClub(string clubName)
		{
			if(clubName == string.Empty)
			{
				var aircrafts =  await aircraftRepository.ListAllAsync();
				return aircrafts as List<Aircraft>;
			}
			else
			{
				return await aircraftRepository.GetAllInClub(clubName);
			}
		}

		public async Task<List<Aircraft>> GetAllAircraftsNotInClub()
		{
			return await aircraftRepository.GetAllNotInClub("");
		}

		public async Task<bool> RemoveAircraft(int id)
		{
			Aircraft aircraft = await aircraftRepository.GetByIdAsync(id);
			if (aircraft == null)
				return await Task.FromResult(false);
			await aircraftRepository.DeleteAsync(aircraft);
			return await Task.FromResult(true);
		}

		public async  Task<bool> UpdateAircraft(Aircraft aircraft)
		{
			await aircraftRepository.UpdateAsync(aircraft);
			return  await Task.FromResult(true);
		}

		public async Task<bool> UpdateAircraftPrice(AircraftPrice aircraftPrice)
		{
			if(aircraftPrice == null || aircraftPrice.Id == 0)
				return await Task.FromResult(false);
			await asyncRepositoryAircraftPrice.UpdateAsync(aircraftPrice);
			return await Task.FromResult(true);
		}
	}
}
