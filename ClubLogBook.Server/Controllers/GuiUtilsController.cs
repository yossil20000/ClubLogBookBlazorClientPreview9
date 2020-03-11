using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Models;
using AutoMapper;
using MediatR;
using ClubLogBook.Application.Flights.Queries;
using ClubLogBook.Application.Flights.Commands;
using ClubLogBook.Application.Extenttions;
using ClubLogBook.Application.AircraftManager.Queries;
using ClubLogBook.Application.ClubContact.Queries;
using ClubLogBook.Application.Common.Models;

namespace ClubLogBook.Server.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class GuiUtilsController : ControllerBase
	{

		private readonly IMediator _mediator;
		IAppLogger<AccountController> _logger;
		private IMapper _mapper;

		public GuiUtilsController(IMapper mapper, IMediator mediator, IAppLogger<AccountController> logger)
		{
			_mapper = mapper;
			_mediator = mediator;
			_logger = logger;
		}
		[HttpGet]
		[Route("GetFilters")]
		public async Task<FilterModel> GetFilters(int clubId)
		{
			_logger.LogInformation("GetFilters");
			FilterModel filterModel = new FilterModel();
			IEnumerable<Aircraft> aircrafts;
			IEnumerable<Pilot> pilots;
			GetClubsListQuery getClubsListQuery = new GetClubsListQuery(QueryBy.ID, "", clubId);
			ClubListViewModel clubs = await _mediator.Send(getClubsListQuery); 

			GetClubAircraftListQuery getClubAircraftListQuery = new GetClubAircraftListQuery(QueryBy.ID, "", clubId);
			GetClubMembersListQuery getClubMembersListQuery = new GetClubMembersListQuery(QueryBy.ID, "",clubId);
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
		[HttpPost]
		[Route("FilterModelPut")]
		public async Task<FilterModel> FilterModelPut([FromBody] FilterModel filterModel)
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


	}
}
