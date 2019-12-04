using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClubLogBook.Application.Services;
using ClubLogBook.Core.Entities;
using ClubLogBook.Infrastructure.Data;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Server.Services;
using AutoMapper;

namespace ClubLogBook.Server.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class FlightReservationController : ControllerBase
    {
		private IMemberRepository _memberRepository;
		private IMapper _mapper;
		private IReservationService _reservationService;
		private IClubService _clubService;
		private IRecordViewModelService<RecordsViewModel<FlightReservationViewModel>> _recordViewModelService;
		public FlightReservationController(IMemberRepository memberRepository, IMapper mapper,IReservationService reservationService, IClubService clubService, IRecordViewModelService<RecordsViewModel<FlightReservationViewModel>> recordViewModelService)
		{
			_memberRepository = memberRepository;
			_clubService = clubService;
			_reservationService = reservationService;
			_mapper = mapper;
			_recordViewModelService = recordViewModelService;
		}
		// GET: api/FlightReservation
		[HttpGet]
		[Route("api/FlightReservation/Reservation")]
		public async Task<IEnumerable<FlightReservationViewModel>> Reservation()
        {
			ICollection<FlightReservationViewModel> ienumerableDest;
			var reservations = await _reservationService.GetReservation();
			
			ienumerableDest = _mapper.Map<IEnumerable<AircraftReservation>, IList<FlightReservationViewModel>>(reservations);
			if (ienumerableDest == null)
				ienumerableDest = new List<FlightReservationViewModel>();
			foreach (var r  in ienumerableDest)
			{
				var pilot = await _memberRepository.GetByIdAsync(r.PilotId);
				r.UserId = pilot == null ? string.Empty : pilot.UserId;
				r.ExtructTime();
			}
			return ienumerableDest;
		}

		// GET: api/FlightReservation/5
		[HttpGet]
		[Route("api/FlightReservation/Details/{id}")]
		public async Task<FlightReservationViewModel> Details(int id)
		{
			FlightReservationViewModel flightReservation;
			AircraftReservation reservation;
			reservation = await _reservationService.GetReservation(id);

			flightReservation = _mapper.Map<AircraftReservation, FlightReservationViewModel>(reservation);
			flightReservation.ExtructTime();
			return flightReservation;
		}
		[HttpPost]
		[Route("api/FlightReservation/FilterViewModelPut")]
		public async Task<FilterViewModel> FilterViewModelPut([FromBody] FilterViewModel filterViewModel)
		{
			int? clubId = filterViewModel.ClubFilterApplied;
			IEnumerable<Aircraft> aircrafts;
			IEnumerable<Pilot> pilots;
			IEnumerable<Club> clubs = await (clubId == null ? _clubService.GetClubs() : _clubService.GetClubById((int)clubId));
			if (clubs == null || clubs.Count() > 1)
			{
				aircrafts = await _clubService.GetClubAircrafts("");
				pilots = await _clubService.GetClubMembers("");
			}
			else
			{
				aircrafts = await _clubService.GetClubAircrafts(clubs.FirstOrDefault().Name);
				pilots = await _clubService.GetClubMembers(clubs.FirstOrDefault().Name);
			}
			filterViewModel.ClubSelects = clubs.Select(c => new ClubSelectViewModel() { Id = c.Id, ClubName = c.Name });
			filterViewModel.AirplaneSelects = aircrafts.Select(ar => new AirplaneSelectViewModel() { Id = ar.Id, TailNumber = ar.TailNumber });
			filterViewModel.PilotSelects = pilots.Select(p => new PilotSelectViewModel() { Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, IdNumber = p.IdNumber,UserId = p.UserId });

			return filterViewModel;

		}
		[HttpPut]
		[Route("api/FlightReservation/RecordViewModel")]
		public async Task<RecordsViewModel<FlightReservationViewModel>> RecordViewModel([FromBody] RecordsViewModel<FlightReservationViewModel> filterViewModel)
		{
			int? clubId = filterViewModel.FilterViewModel.ClubFilterApplied;
			
			int ActualPage = filterViewModel.PaginationInfo.ActualPage;
			switch (filterViewModel.PaginationInfo.PageCommand)
			{
				case PageCommand.MoveNext:
					ActualPage = filterViewModel.PaginationInfo.ActualPage + 1;
					break;
				case PageCommand.MovePrevious:
					ActualPage = filterViewModel.PaginationInfo.ActualPage - 1;
					break;
			}
			var reservations = await _recordViewModelService.GetRecord(ActualPage, 10, filterViewModel.FilterViewModel.AirplaneFilterApplied, filterViewModel.FilterViewModel.PilotFilterApplied);

			//ienumerableDest = _mapper.Map<IEnumerable<AircraftReservation>, IList<FlightReservationViewModel>>(reservations);
			return reservations;

		}
		// POST: api/FlightReservation
		[HttpPost]
        public void Post([FromBody] string value)
        {
        }
		// PUT: api/FlightReservation/5
		[HttpPut]
		[Route("api/FlightReservation/Edit")]
        public async Task Edit([FromBody] FlightReservationViewModel reservationViewModel)
        {
			if (ModelState.IsValid)
			{
				AircraftReservation aircraftReservation;
				reservationViewModel.CombineTime();
				aircraftReservation = _mapper.Map<FlightReservationViewModel, AircraftReservation>(reservationViewModel);
				await _reservationService.EditReservation(aircraftReservation);
				bool isValid = await _reservationService.IsValid(aircraftReservation);
				if (!isValid)
				{
					await _reservationService.DeleteReservation(aircraftReservation.Id);
					
				}
				System.Diagnostics.Debug.WriteLine(reservationViewModel.ToString());
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpPut]
		[Route("api/FlightReservation/Delete")]
		public async Task Delete([FromBody] int id)
        {
			if (ModelState.IsValid)
			{
				await _reservationService.DeleteReservation(id);
				System.Diagnostics.Debug.WriteLine(id.ToString());
			}

		}
		[HttpPut]
		[Route("api/FlightReservation/Create")]
		public async Task Create([FromBody] FlightReservationViewModel reservationViewModel)
		{
			if (ModelState.IsValid)
			{
				AircraftReservation aircraftReservation;
				reservationViewModel.CombineTime();
				aircraftReservation = _mapper.Map<FlightReservationViewModel, AircraftReservation>(reservationViewModel);
				await _reservationService.AddReservation(aircraftReservation);
				System.Diagnostics.Debug.WriteLine(reservationViewModel?.ToString());
			}

		}
	}
}
