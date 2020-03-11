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
using ClubLogBook.Application.Models;
using ClubLogBook.Server.Services;
using AutoMapper;
using ClubLogBook.Application.Reservation.Queries;
using MediatR;
using ClubLogBook.Application.ClubContact.Queries;
using ClubLogBook.Application.AircraftManager.Queries;
using ClubLogBook.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace ClubLogBook.Server.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class FlightReservationController : ControllerBase
    {
		private IMediator _mediator;
		private ILogger<FlightReservationController> _logger;
		//private IMemberRepository _memberRepository;
		//private IMapper _mapper;
		//private IReservationService _reservationService;
		//private IClubService _clubService;
		private IRecordViewModelService<RecordsViewModel<FlightReservationModel>> _recordViewModelService;
		public FlightReservationController(IMediator mediator, ILogger<FlightReservationController> logger /*,IMemberRepository memberRepository, IMapper mapper,IReservationService reservationService, IClubService clubService*/, IRecordViewModelService<RecordsViewModel<FlightReservationModel>> recordViewModelService)
		{
			_mediator = mediator;
			_logger = logger;
			//_memberRepository = memberRepository;
			//_clubService = clubService;
			//_reservationService = reservationService;
			//_mapper = mapper;
			_recordViewModelService = recordViewModelService;
		}
		// GET: api/FlightReservation
		[HttpGet]
		[Route("api/FlightReservation/Reservation")]
		public async Task<List<FlightReservationModel>> Reservation()
        {
			List<FlightReservationModel> reservations;
			//var reservations = await _reservationService.GetReservation();

			//ienumerableDest = _mapper.Map<IEnumerable<AircraftReservation>, IList<FlightReservationModel>>(reservations);
			//foreach(var r  in ienumerableDest)
			//{
			//	var pilot = await _memberRepository.GetByIdAsync(r.PilotId);
			//	r.UserId = pilot == null ? string.Empty : pilot.UserId;
			//	r.ExtructTime();
			//}
			GetReservationsQuery getReservationsQuery = new GetReservationsQuery();
			reservations = await _mediator.Send(getReservationsQuery);
			return reservations;
		}

		// GET: api/FlightReservation/5
		[HttpGet]
		[Route("api/FlightReservation/Details/{id}")]
		public async Task<FlightReservationModel> Details(int id)
		{
			FlightReservationModel reservation;
			//AircraftReservation reservation;
			//reservation = await _reservationService.GetReservation(id);

			//flightReservation = _mapper.Map<AircraftReservation, FlightReservationModel>(reservation);
			//flightReservation.ExtructTime();
			GetReservationByIdQuery getReservationByIdQuery = new GetReservationByIdQuery(id);
			reservation = await _mediator.Send(getReservationByIdQuery);
			return reservation;
		}
		[HttpPost]
		[Route("api/FlightReservation/FilterViewModelPut")]
		public async Task<FilterModel> FilterViewModelPut([FromBody] FilterModel filterModel)
		{
			_logger.LogInformation("FilterModelPut");
			int? clubId = filterModel.ClubFilterApplied;
			IEnumerable<Aircraft> aircrafts;
			IEnumerable<Pilot> pilots;
			GetClubsListQuery getClubsListQuery = new GetClubsListQuery(QueryBy.ID, "", clubId ?? 0);
			ClubListViewModel clubs = await _mediator.Send(getClubsListQuery); // ((clubId == null || clubId == 0) ? _clubService.GetClubs() : _clubService.GetClubById((int)clubId));

			GetClubAircraftListQuery getClubAircraftListQuery = new GetClubAircraftListQuery(QueryBy.Name, (clubs == null || clubs.ClubViewModels.Count() == 0) ? "" : clubs.ClubViewModels.FirstOrDefault().Name, clubId ?? 0);
			GetClubMembersListQuery getClubMembersListQuery = new GetClubMembersListQuery(QueryBy.Name, (clubs == null || clubs.ClubViewModels.Count() == 0) ? "" : clubs.ClubViewModels.FirstOrDefault().Name, clubId ?? 0);
			var aircraftListModel = await _mediator.Send(getClubAircraftListQuery);
			filterModel.ClubSelects = clubs.ClubViewModels.Select(c => new ClubSelectModel() { Id = c.Id, ClubName = c.Name });
			filterModel.AirplaneSelects.Add(new AirplaneSelectModel() { Id = 0, TailNumber = "All Airplanes" });
			filterModel.AirplaneSelects.AddRange(aircraftListModel.AircraftList.Select(ar => new AirplaneSelectModel() { Id = ar.Id, TailNumber = ar.TailNumber }).ToList());
			var pilotSelectListModel = await _mediator.Send(getClubMembersListQuery);
			filterModel.PilotSelects.Add(new PilotSelectModel() { Id = 0, FirstName = "All Pilots" });
			filterModel.PilotSelects.AddRange(pilotSelectListModel.PilotSelectList);
			filterModel.ClubFilterApplied = filterModel.ClubSelects.FirstOrDefault().Id;
			filterModel.AirplaneFilterApplied = filterModel.AirplaneSelects.FirstOrDefault().Id;
			filterModel.PilotFilterApplied = filterModel.PilotSelects.FirstOrDefault().Id;

			return filterModel;

		}
		[HttpPut]
		[Route("api/FlightReservation/RecordViewModel")]
		public async Task<RecordsViewModel<FlightReservationModel>> RecordViewModel([FromBody] RecordsViewModel<FlightReservationModel> filterViewModel)
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
        public async Task Edit([FromBody] FlightReservationModel reservation)
        {
			if (ModelState.IsValid)
			{
				IsReservationValidQuery isReservationValidQuery = new IsReservationValidQuery(reservation);
				var isValid = await _mediator.Send(isReservationValidQuery);
				if(isValid)
				{
					UpdateReservationCommand updateReservationCommand = new UpdateReservationCommand(reservation);
					var result = _mediator.Send(updateReservationCommand);
					
				}
				//AircraftReservation aircraftReservation;
				//reservationViewModel.CombineTime();
				//aircraftReservation = _mapper.Map<FlightReservationModel, AircraftReservation>(reservationViewModel);
				//await _reservationService.EditReservation(aircraftReservation);
				//bool isValid = await _reservationService.IsValid(aircraftReservation);
				//if (!isValid)
				//{
				//	await _reservationService.DeleteReservation(aircraftReservation.Id);
					
				//}
				//System.Diagnostics.Debug.WriteLine(reservationViewModel.ToString());
			}
		}

		// DELETE: api/ApiWithActions/5
		[HttpPut]
		[Route("api/FlightReservation/Delete")]
		public async Task Delete([FromBody] int id)
        {
			if (ModelState.IsValid)
			{
				DeleteReservationByIdCommand deleteReservationByIdCommand = new DeleteReservationByIdCommand(id);
				var ressult = _mediator.Send(deleteReservationByIdCommand);
				//await _reservationService.DeleteReservation(id);
				System.Diagnostics.Debug.WriteLine(id.ToString());
			}

		}
		[HttpPut]
		[Route("api/FlightReservation/Create")]
		public async Task Create([FromBody] FlightReservationModel reservation)
		{
			if (ModelState.IsValid)
			{
				//AircraftReservation aircraftReservation;
				//reservationViewModel.CombineTime();
				//aircraftReservation = _mapper.Map<FlightReservationModel, AircraftReservation>(reservationViewModel);
				//await _reservationService.AddReservation(aircraftReservation);
				CreateReservationCommand createReservationCommand = new CreateReservationCommand(reservation);
				var result = _mediator.Send(createReservationCommand);
				System.Diagnostics.Debug.WriteLine(reservation?.ToString());
			}

		}
	}
}
