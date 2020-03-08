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
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using ClubLogBook.Application.Accounts.Queries.GetAccount;
using ClubLogBook.Application.Flights.Queries;
using ClubLogBook.Application.Flights.Commands;
using ClubLogBook.Application.Extenttions;

namespace ClubLogBook.Server.Controllers
{
	
    [Route("api/[controller]")]
    [ApiController]
    public class ClubFlightController : ControllerBase
    {
     
		private readonly IMediator _mediator;
		IAppLogger<AccountController> _logger;
		private IMapper _mapper;
		IClubService _clubService;
		IRecordViewModelService<FlightRecordIndexViewModel> _flightRecordViewModelService;
		IAircraftManagerService aircraftManagerService;
		public FlightRecordIndexViewModel FlightRecordIndexViewModel { get; set; }
		public ClubFlightController(IMapper mapper, IClubService clubService, IRecordViewModelService<FlightRecordIndexViewModel> flightRecordViewModelService,IAircraftManagerService aircraftManagerService,
			IMediator mediator, IAppLogger<AccountController> logger)
		{
			//MapperUtils.MapperInit(typeof(ClubFlightViewModel), typeof(Flight));
			_mapper = mapper;
			_clubService = clubService;
			_flightRecordViewModelService = flightRecordViewModelService;
			//Mapper.Initialize(cfg => cfg.CreateMap<ClubFlightViewModel2, ClubFlightViewModel1>());
			this.aircraftManagerService = aircraftManagerService;
			_mediator = mediator;
			_logger = logger;
		}
		[HttpGet]
		[Route("FlightWithFilter")]
		public async Task<FlightRecordIndexViewModel> FlightWithFilter()
		{
			_logger.LogInformation("FlightWithFilter");
			FlightRecordIndexViewModel flightRecordIndexViewModel = new FlightRecordIndexViewModel();

			GetAllFlightsQuery getAllFlights = new GetAllFlightsQuery();
			flightRecordIndexViewModel = await _mediator.Send(getAllFlights);
			flightRecordIndexViewModel.FilterViewModel = await FilterViewModelPut(flightRecordIndexViewModel.FilterViewModel);
			flightRecordIndexViewModel.FlightRecords.MarkNonValidFlight();
			return flightRecordIndexViewModel; 
			
			//return await _flightRecordViewModelService.GetRecord(0, 10,null, null);
		}

		[HttpPost]
		[Route("FilterViewModelPut")]
		public async Task<FilterViewModel> FilterViewModelPut([FromBody] FilterViewModel filterViewModel)
		{
			_logger.LogInformation("FilterViewModelPut");
			int? clubId = filterViewModel.ClubFilterApplied;
			IEnumerable<Aircraft> aircrafts;
			IEnumerable<Pilot> pilots;
			IEnumerable <Club> clubs = await ((clubId == null || clubId == 0) ? _clubService.GetClubs() : _clubService.GetClubById((int)clubId));
			if (clubs == null || clubs.Count() == 0)
			{
				aircrafts = await _clubService.GetClubAircrafts("");
				pilots = await _clubService.GetClubMembers("");
			}
			else
			{
				aircrafts = await _clubService.GetClubAircrafts(clubs.FirstOrDefault().Name);
				pilots = await _clubService.GetClubMembers(clubs.FirstOrDefault().Name);
			}
			filterViewModel.ClubSelects =  clubs.Select(c => new ClubSelectViewModel() { Id = c.Id, ClubName = c.Name });
			filterViewModel.AirplaneSelects.Add(new AirplaneSelectViewModel() { Id = 0, TailNumber = "All Airplanes" });
			filterViewModel.AirplaneSelects.AddRange(aircrafts.Select(ar => new AirplaneSelectViewModel() { Id = ar.Id, TailNumber = ar.TailNumber }).ToList());

			filterViewModel.PilotSelects.Add(new PilotSelectViewModel() { Id = 0, FirstName = "All Pilots" });
			filterViewModel.PilotSelects.AddRange(pilots.Select(p => new PilotSelectViewModel() {Id = p.Id, FirstName = p.FirstName, LastName = p.LastName, IdNumber = p.IdNumber }).ToList());
			filterViewModel.ClubFilterApplied = filterViewModel.ClubSelects.FirstOrDefault().Id;
			filterViewModel.AirplaneFilterApplied = filterViewModel.AirplaneSelects.FirstOrDefault().Id;
			filterViewModel.PilotFilterApplied = filterViewModel.PilotSelects.FirstOrDefault().Id;

			return filterViewModel;

		}

		[HttpPut]
		[Route("Put")]
		public async Task<FlightRecordIndexViewModel> Put([FromBody] FlightRecordIndexViewModel flightRecordIndexViewModel)
		{
			
			GetFilteredFlightsQuery getFilteredFlightsQuery = new GetFilteredFlightsQuery();
			getFilteredFlightsQuery.flightRecordIndexView = flightRecordIndexViewModel;
			var result = await _mediator.Send(getFilteredFlightsQuery);
			flightRecordIndexViewModel.FlightRecords.MarkNonValidFlight();
			return result;
			
		}
		// GET: api/ClubFlight
		[HttpGet]
        public async Task<IEnumerable<ClubFlightViewModel>> Get()
        {
			ICollection<Flight> flight;
			Flight f = (new Flight(DateTime.Now.AddHours(1), "LLSD", 2345, 2346));
			flight = await _clubService.GetClubAircraftFlight("BAZ", 8);
			ICollection<ClubFlightViewModel> ienumerableDest;

			ienumerableDest =  _mapper.Map<ICollection<Flight>, IList<ClubFlightViewModel>>(flight);
			return ienumerableDest;
        }

		[HttpGet]
		[Route("Aircraft")]
		public async Task<IEnumerable<AirplaneSelectViewModel>> Aircraft()
		{
			IEnumerable<AirplaneSelectViewModel> airplaneSelectViewModels;
			var clubAircraft = await _clubService.GetClubAircrafts("BAZ");
			airplaneSelectViewModels = _mapper.Map<IEnumerable<AirplaneSelectViewModel>>(clubAircraft);
			return airplaneSelectViewModels;
		}
		
		// GET: api/ClubFlight/5
		[HttpGet("{id}", Name = "Get")]
        public async Task<ClubFlightViewModel> Get(int id)
        {
			Flight flight;
			ClubFlightViewModel clubFlightViewModel;
			var flightlist = await _clubService.GetClubAircraftFlight("BAZ", 8);
			flight = flightlist.Where(i => i.Id == id).FirstOrDefault();
			clubFlightViewModel = _mapper.Map<Flight, ClubFlightViewModel>(flight);
			if (clubFlightViewModel.Routh == null)
				clubFlightViewModel.Routh = "NULL";
			return clubFlightViewModel;
			
        }

        // POST: api/ClubFlight
        [HttpPost]
        public async Task Post([FromBody] ClubFlightViewModel value)
        {
			UpdateFlightCommand updateFlightCommand = new UpdateFlightCommand();
			updateFlightCommand.ClubFlightViewModel = value;
			var ressult = _mediator.Send(updateFlightCommand);
			return ;
		}
		[HttpPut]
		public async Task   Put([FromBody] ClubFlightViewModel value)
		{
			CreateFlightCommand createFlightCommand = new CreateFlightCommand();
			createFlightCommand.ClubFlightViewModel = value;
			createFlightCommand.ClubId = 1;
			var result = _mediator.Send(createFlightCommand);

		}
        // PUT: api/ClubFlight/5
  //      [HttpPut]
		
		//public void Edit([FromBody] ClubFlightViewModel value)
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
