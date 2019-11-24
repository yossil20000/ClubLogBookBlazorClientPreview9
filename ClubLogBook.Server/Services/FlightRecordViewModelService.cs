using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Services;
using ClubLogBook.Infrastructure;
using Microsoft.Extensions.Logging;
using ClubLogBook.Application.Specifications;
namespace ClubLogBook.Server.Services
{
	public class FlightRecordViewModelService : IRecordViewModelService<FlightRecordIndexViewModel>
	{
		private readonly IAppLogger<FlightRecordViewModelService>  _logger;
		private readonly IAsyncRepository<Club> _clubRepository;
		private readonly IAsyncRepository<Flight> _fligthRepository;
		private readonly IAsyncRepository<Aircraft> _aircraftRepository;
		private readonly IClubService _clubService;
		public FlightRecordViewModelService(IClubService clubService, IAppLogger<FlightRecordViewModelService> loggerFactory,IAsyncRepository<Club> clubRepository,IAsyncRepository<Flight> flightRepository,IAsyncRepository<Aircraft> aircraftRepository)
		{
			_logger = loggerFactory;_clubRepository = clubRepository;_aircraftRepository = aircraftRepository;_fligthRepository = flightRepository;
			_clubService = clubService;
		}
		public async Task<IEnumerable<PilotSelectViewModel>> GetPilots(int? clubId)
		{
			_logger.LogInformation("GetAirplans called");

			List<PilotSelectViewModel> pilotSelectViewModel = new List<PilotSelectViewModel>();
			IReadOnlyList<Club> clubs = await _clubRepository.ListAllAsync();

			
			{
				foreach (var c in clubs)
				{
					var members = await _clubService.GetClubMembers(c.Name);
					foreach (var i in members)
					{
						pilotSelectViewModel.Add(new PilotSelectViewModel() { Id = i.Id, FirstName = i.FirstName, LastName = i.LastName });
					}




				}
			}
			return pilotSelectViewModel;
		}
		public async Task<IEnumerable<AirplaneSelectViewModel>> GetAirplans(int? clubId)
		{
			_logger.LogInformation("GetAirplans called");

			List<AirplaneSelectViewModel> airplaneSelectViewModel = new List<AirplaneSelectViewModel>();
			IReadOnlyList<Club> clubs = await _clubRepository.ListAllAsync();
			
			//if (clubId != null)
			//{
			//	club = clubs.Where(i => i.Id == clubId).FirstOrDefault();
				
			//	foreach (var i in club.Aircrafts)
			//	{
			//		airplaneSelectViewModel.Add(new AirplaneSelectViewModel() { Id = i.Id, TailNumber = i.TailNumber });
			//	}

			//}
			//else
			{
				foreach (var c in clubs)
				{
					var aircraft = await _clubService.GetClubAircrafts(c.Name);
					foreach (var i in aircraft)
					{
						airplaneSelectViewModel.Add(new AirplaneSelectViewModel() { Id = i.Id, TailNumber = i.TailNumber });
					}
					
					
					
					
				}
			}
			return airplaneSelectViewModel;

		}
		public async Task<IEnumerable<ClubSelectViewModel>> GetClubs(int? clubId)
		{
			_logger.LogInformation("GetClubs called");
			List<ClubSelectViewModel> clubSelectViewModel = new List<ClubSelectViewModel>();
			IReadOnlyList<Club> clubs = await _clubRepository.ListAllAsync();
			Club club;
			if (clubId != null)
			{
				club = clubs.Where(i => i.Id == clubId).FirstOrDefault();
				clubSelectViewModel.Add(new ClubSelectViewModel() { Id = club.Id, ClubName = club.Name });
				
			}
			else
			{
				foreach(var c in clubs)
				{

					clubSelectViewModel.Add(new ClubSelectViewModel() { Id = c.Id, ClubName = c.Name });
				}
			}
			return clubSelectViewModel; 
		}

		public async Task<FlightRecordIndexViewModel> GetRecord(int pageIndex, int itemsPage, int? airplaneId,int? pilotId)
		{
			_logger.LogInformation("GetFlightRecord called");
			FlighWithSpecification flightPagingSpec = new FlighWithSpecification(itemsPage * pageIndex, itemsPage, airplaneId, pilotId);
			FlighWithSpecification flightSpec = new FlighWithSpecification(airplaneId, pilotId);

			var flightOnPage = await _fligthRepository.ListAsync(flightPagingSpec);
			var totalFlight = await _fligthRepository.CountAsync(flightSpec);
			var vm = new FlightRecordIndexViewModel()
			{
				FlightRecords = flightOnPage.Select(i => new ClubFlightViewModel(i.Pilot,i.Aircraft)
				{
					Id = i.Id,
					Date = i.Date,
					EngineStart = i.EngineStart,
					EngineEnd = i.EngineEnd,
					HobbsStart = i.HobbsStart,
					HobbsEnd = i.HobbsEnd,
					Routh = i.Routh


				}),
				FilterViewModel = new FilterViewModel() { 
					AirplaneSelects = await GetAirplans(1),
					ClubSelects = await GetClubs(1),
					PilotSelects = await GetPilots(1),
					PilotFilterApplied = pilotId,
					ClubFilterApplied = 1,
					AirplaneFilterApplied = airplaneId,
				},
				PaginationInfo = new PaginationInfoViewModel()
				{
					ActualPage = pageIndex,
					ItemsPerPage = flightOnPage.Count,
					TotalItems = totalFlight,
					TotalPages = int.Parse(Math.Ceiling((decimal)totalFlight / itemsPage).ToString())
				}

			};
			vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage >= vm.PaginationInfo.TotalPages - 1 ) ? "disabled" : "";
			vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";
			return vm;
		}
	}
}
