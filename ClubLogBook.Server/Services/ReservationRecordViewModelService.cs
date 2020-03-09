using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.Models;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Services;
using ClubLogBook.Infrastructure;
using Microsoft.Extensions.Logging;
using ClubLogBook.Application.Specifications;

namespace ClubLogBook.Server.Services
{
	public class ReservationRecordViewModelService : IRecordViewModelService<RecordsViewModel<FlightReservationModel>> 
	{
		private readonly IAppLogger<FlightRecordViewModelService> _logger;
		private readonly IAsyncRepository<Club> _clubRepository;
		private readonly IAsyncRepository<AircraftReservation> _reservationRepository;
		private readonly IAsyncRepository<Aircraft> _aircraftRepository;
		private readonly IClubService _clubService;
		public ReservationRecordViewModelService(IClubService clubService, IAppLogger<FlightRecordViewModelService> loggerFactory, IAsyncRepository<Club> clubRepository, IAsyncRepository<AircraftReservation> reservationRepository, IAsyncRepository<Aircraft> aircraftRepository) 
		{
			_logger = loggerFactory; _clubRepository = clubRepository; _aircraftRepository = aircraftRepository; _reservationRepository = reservationRepository;
			_clubService = clubService;
		}

		

		

		public async Task<List<AirplaneSelectModel>> GetAirplans(int? clubId)
		{
			_logger.LogInformation("GetAirplans called");

			List<AirplaneSelectModel> airplaneSelectViewModel = new List<AirplaneSelectModel>();
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
						airplaneSelectViewModel.Add(new AirplaneSelectModel() { Id = i.Id, TailNumber = i.TailNumber });
					}




				}
			}
			return airplaneSelectViewModel;
		}

		public async Task<IEnumerable<ClubSelectModel>> GetClubs(int? clubId)
		{
			_logger.LogInformation("GetClubs called");
			List<ClubSelectModel> clubSelectViewModel = new List<ClubSelectModel>();
			IReadOnlyList<Club> clubs = await _clubRepository.ListAllAsync();
			Club club;
			if (clubId != null)
			{
				club = clubs.Where(i => i.Id == clubId).FirstOrDefault();
				clubSelectViewModel.Add(new ClubSelectModel() { Id = club.Id, ClubName = club.Name });

			}
			else
			{
				foreach (var c in clubs)
				{

					clubSelectViewModel.Add(new ClubSelectModel() { Id = c.Id, ClubName = c.Name });
				}
			}
			return clubSelectViewModel;
		}

		 public async Task<List<PilotSelectModel>> GetPilots(int? clubId)
		{
			_logger.LogInformation("GetAirplans called");

			List<PilotSelectModel> pilotSelectViewModel = new List<PilotSelectModel>();
			IReadOnlyList<Club> clubs = await _clubRepository.ListAllAsync();


			{
				foreach (var c in clubs)
				{
					var members = await _clubService.GetClubMembers(c.Name);
					foreach (var i in members)
					{
						pilotSelectViewModel.Add(new PilotSelectModel() { Id = i.Id, FirstName = i.FirstName, LastName = i.LastName });
					}




				}
			}
			return pilotSelectViewModel;
		}

		public async Task<RecordsViewModel<FlightReservationModel>> GetRecord(int pageIndex, int itemsPage, int? airplaneId, int? pilotId)
		{
			_logger.LogInformation("GetFlightRecord called");
			string idNumber="", tailNumber="";
			if (pilotId != null)
			{
				Pilot pilot = await  _clubService.GetPilotById((int)pilotId);
				if (pilot != null)
					idNumber = pilot.IdNumber;
			}
			if(airplaneId != null)
			{
				Aircraft ar = await _aircraftRepository.GetByIdAsync((int)airplaneId);
				if (ar != null)
					tailNumber = ar.TailNumber;
			}
			ReservationSpecification flightPagingSpec = new ReservationSpecification(itemsPage * pageIndex, itemsPage, tailNumber, idNumber);
			ReservationSpecification flightSpec = new ReservationSpecification(tailNumber, idNumber);

			var flightOnPage = await _reservationRepository.ListAsync(flightPagingSpec);
			var totalFlight = await _reservationRepository.CountAsync(flightSpec);
			var airplanes = await GetAirplans(1);
			airplanes.Add(new AirplaneSelectModel() { Id = 0, TailNumber = "Select All" });
			var vm = new RecordsViewModel<FlightReservationModel>()
			{
				Records = flightOnPage.Select(i => new FlightReservationModel()
				{
					Id = i.Id,
					DateFrom = i.DateFrom,
					DateTo = i.DateTo,
					IdNumber = i.IdNumber,
					TailNumber = i.TailNumber,
					UserInfo = i.ReservationInfo



				}),
				FilterViewModel = new FilterModel()
				{
					
					AirplaneSelects = airplanes,
					ClubSelects = await GetClubs(1),
					PilotSelects = await GetPilots(1),
					PilotFilterApplied = pilotId,
					ClubFilterApplied = 1,
					AirplaneFilterApplied = airplaneId,
				},
				PaginationInfo = new PaginationInfoModel()
				{
					ActualPage = pageIndex,
					ItemsPerPage = flightOnPage.Count,
					TotalItems = totalFlight,
					TotalPages = int.Parse(Math.Ceiling((decimal)totalFlight / itemsPage).ToString())
				}

			};
			vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage >= vm.PaginationInfo.TotalPages - 1) ? "disabled" : "";
			vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "disabled" : "";
			return vm;
		}
	}
}
