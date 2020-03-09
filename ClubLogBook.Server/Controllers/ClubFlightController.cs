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
    public class ClubFlightController : ControllerBase
    {
     
		private readonly IMediator _mediator;
		IAppLogger<AccountController> _logger;
		private IMapper _mapper;
	
		public ClubFlightController(IMapper mapper, IMediator mediator, IAppLogger<AccountController> logger)
		{
			_mapper = mapper;
			_mediator = mediator;
			_logger = logger;
		}
		[HttpGet]
		[Route("FlightWithFilter")]
		public async Task<FlightRecordIndexModel> FlightWithFilter()
		{
			_logger.LogInformation("FlightWithFilter");
			FlightRecordIndexModel flightRecordIndexModel = new FlightRecordIndexModel();

			GetAllFlightsQuery getAllFlights = new GetAllFlightsQuery();
			flightRecordIndexModel = await _mediator.Send(getAllFlights);
			flightRecordIndexModel.FilterModel = await FilterModelPut(flightRecordIndexModel.FilterModel);
			flightRecordIndexModel.FlightRecords.MarkNonValidFlight();
			return flightRecordIndexModel; 
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
			
			GetClubAircraftListQuery getClubAircraftListQuery = new GetClubAircraftListQuery(QueryBy.Name, (clubs == null || clubs.ClubViewModels.Count() == 0) ? "" : clubs.ClubViewModels.FirstOrDefault().Name , clubId ?? 0);
			GetClubMembersListQuery getClubMembersListQuery = new GetClubMembersListQuery(QueryBy.Name, (clubs == null || clubs.ClubViewModels.Count() == 0) ? "" : clubs.ClubViewModels.FirstOrDefault().Name, clubId ?? 0);
			var aircraftListModel = await _mediator.Send(getClubAircraftListQuery);
			filterModel.ClubSelects =  clubs.ClubViewModels.Select(c => new ClubSelectModel() { Id = c.Id, ClubName = c.Name });
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
		[Route("Put")]
		public async Task<FlightRecordIndexModel> Put([FromBody] FlightRecordIndexModel flightRecordIndexModel)
		{
			
			GetFilteredFlightsQuery getFilteredFlightsQuery = new GetFilteredFlightsQuery();
			getFilteredFlightsQuery.flightRecordIndexView = flightRecordIndexModel;
			var result = await _mediator.Send(getFilteredFlightsQuery);
			flightRecordIndexModel.FlightRecords.MarkNonValidFlight();
			return result;
			
		}
		// GET: api/ClubFlight
		[HttpGet]
        public async Task<IEnumerable<ClubFlightModel>> Get()
        {
			//ICollection<Flight> flight;
			//Flight f = (new Flight(DateTime.Now.AddHours(1), "LLSD", 2345, 2346));
			//flight = await _clubService.GetClubAircraftFlight("BAZ", 8);
			ICollection<ClubFlightModel> ienumerableDest = new HashSet<ClubFlightModel>();

			//ienumerableDest =  _mapper.Map<ICollection<Flight>, IList<ClubFlightModel>>(flight);
			return ienumerableDest;
        }

		[HttpGet]
		[Route("Aircraft")]
		public async Task<IEnumerable<AirplaneSelectModel>> Aircraft()
		{
			IEnumerable<AirplaneSelectModel> airplaneSelectModels;
			GetClubAircraftListQuery getClubAircraftListQuery = new GetClubAircraftListQuery(QueryBy.Name, "BAZ",1);
			var aircraftListModel = await _mediator.Send(getClubAircraftListQuery);
			
			airplaneSelectModels = aircraftListModel.AircraftList.Select(ar => new AirplaneSelectModel() { Id = ar.Id, TailNumber = ar.TailNumber });
			return airplaneSelectModels;
		}
		
		// GET: api/ClubFlight/5
		[HttpGet("{id}", Name = "Get")]
        public async Task<ClubFlightModel> Get(int id)
        {
			Flight flight;
			ClubFlightModel clubFlightModel = new ClubFlightModel();
			//var flightlist = await _clubService.GetClubAircraftFlight("BAZ", 8);
			//flight = flightlist.Where(i => i.Id == id).FirstOrDefault();
			//clubFlightModel = _mapper.Map<Flight, ClubFlightModel>(flight);
			//if (clubFlightModel.Routh == null)
			//	clubFlightModel.Routh = "NULL";
			return clubFlightModel;
			
        }

        // POST: api/ClubFlight
        [HttpPost]
        public async Task Post([FromBody] ClubFlightModel value)
        {
			UpdateFlightCommand updateFlightCommand = new UpdateFlightCommand();
			updateFlightCommand.ClubFlightViewModel = value;
			var ressult = _mediator.Send(updateFlightCommand);
			return ;
		}
		[HttpPut]
		public async Task   Put([FromBody] ClubFlightModel value)
		{
			CreateFlightCommand createFlightCommand = new CreateFlightCommand();
			createFlightCommand.ClubFlightModel = value;
			createFlightCommand.ClubId = 1;
			var result = _mediator.Send(createFlightCommand);

		}
        // PUT: api/ClubFlight/5
  //      [HttpPut]
		
		//public void Edit([FromBody] ClubFlightModel value)
  //      {
  //      }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
		//[Route("api/ClubFlight/Delete")]
		//[HttpPut("{id}", Name = "Delete")]
		public async Task Delete([FromBody] int id)
        {
			if (ModelState.IsValid)
			{
				System.Diagnostics.Debug.WriteLine(id.ToString());
				DeleteFlightCommand deleteFlightCommand = new DeleteFlightCommand(id);
				var result = await  _mediator.Send(deleteFlightCommand);

				return;
			}
		}
	}
}
