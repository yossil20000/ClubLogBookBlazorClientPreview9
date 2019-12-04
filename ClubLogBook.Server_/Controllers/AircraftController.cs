using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Core.Entities;
using Microsoft.Extensions.Logging;
using MediatR;
using ClubLogBook.Application.AircraftManager.Queries;
using ClubLogBook.Application.AircraftManager.Commands;
using ClubLogBook.Application.Infrastructure;
using System;
using System.Threading;

namespace ClubLogBook.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AircraftController : Controller
    {
        private  IAircraftManagerService aircraftManagerService;
        private  IMapper mapper;
        ILogger<AircraftController> logger;
        IMediator mediator;
        public AircraftController(IAircraftManagerService aircraftManagerService, IMapper mapper, IMediator mediator)
        {
            this.aircraftManagerService = aircraftManagerService;
            this.mapper = mapper;
            this.mediator = mediator;
            // this.logger = logger;
        }
        // GET: api/Aircraft
        [HttpGet]
        //[Route("api/Aircraft/GetAircraft/clubName")]
        public async  Task<AircraftListViewModel> GetAllAircraft( )
        {
            CancellationToken ct = new CancellationToken();
            //List<Aircraft> air = await aircraftManagerService.GetAllAircraftsInClub("BAZ");
            List<Aircraft> aircraftAll = await aircraftManagerService.GetAllAircraftsInClub("");
            //List<AircraftPrice> aircraftPrices = await aircraftManagerService.GetAircraftPrices();
           //List<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
            AircraftListViewModel aircraftListViewModel = new AircraftListViewModel();
            //mapper.Map(aircraftAll, aircraftViewModel);
            try
            {
                aircraftListViewModel =  await mediator.Send(new GetAircraftListQuery(), ct);
                if(aircraftListViewModel == null)
                    aircraftListViewModel=  new AircraftListViewModel();

            }
            catch(Exception ex)
            {

            }
            return aircraftListViewModel;
            //return aircraftViewModel;
            //return new List<AircraftViewModel>();
        }
        [HttpGet]
        public async Task<AircraftListViewModel> GetClubAircraft(string clubName = "")
        {
            CancellationToken ct = new CancellationToken();List<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
            AircraftListViewModel aircraftListViewModel = new AircraftListViewModel();
            try
            {
               
                if (clubName != "")

                {
                    GetClubAircraftListQuery club = new GetClubAircraftListQuery();
                    club.ClubName = clubName;
                    aircraftListViewModel = await mediator.Send(club, ct);
                    if (aircraftListViewModel == null)
                        aircraftListViewModel = new AircraftListViewModel();
                }

            }
            catch (Exception ex)
            {

            }
            return aircraftListViewModel;


        }
        [HttpPut]
        public async Task<AircraftViewModel> GetCreateAircaft([FromBody] AircraftViewModel item)
        {
            CancellationToken ct = new CancellationToken();
            CreateAircraftCommand createAircraftCommand = new CreateAircraftCommand();
            createAircraftCommand.aircraftViewModel = item;
            Unit unit = await mediator.Send(createAircraftCommand, ct);
            return createAircraftCommand.aircraftViewModel;
        }
        ////// GET: api/Aircraft/5
        ////[HttpGet("{id}", Name = "Get")]
        ////public string Get(int id)
        ////{
        ////    return "value";
        ////}

        //// POST: api/Aircraft
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Aircraft/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        [HttpPut]
        public async Task<AircraftViewModel> PutDeleteAircraft([FromBody] int id)
        {
            AircraftViewModel aircraftViewModel = new AircraftViewModel();
            return aircraftViewModel;
        }
    }
}
