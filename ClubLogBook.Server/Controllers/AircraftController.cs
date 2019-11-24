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
        //[Route("api/Aircraft/AircraftGet")]
        public async  Task<List<AircraftViewModel>> AircraftGet()
        {
            CancellationToken ct = new CancellationToken();
            List<Aircraft> air = await aircraftManagerService.GetAllAircraftsInClub("BAZ");
            List<Aircraft> aircraftAll = await aircraftManagerService.GetAllAircraftsInClub("");
            List<AircraftPrice> aircraftPrices = await aircraftManagerService.GetAircraftPrices();
            List<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
            mapper.Map(aircraftAll, aircraftViewModel);
            try
            {
                var list= await mediator.Send(new GetAircraftListQuery(), ct);
            }
            catch(Exception ex)
            {

            }
            return aircraftViewModel;
            return new List<AircraftViewModel>();
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

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
