using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Entities;
using Microsoft.Extensions.Logging;
using MediatR;
using ClubLogBook.Application.AircraftManager.Queries;
using ClubLogBook.Application.Infrastructure;
using System;
using System.Threading;
using ClubLogBook.Application.AircraftManager.Commands;
using ClubLogBook.Application.AircraftManager.Commands.DeleteAircraft;
using ClubLogBook.Application.AircraftManager.Commands.UpdateAircraft;
using ClubLogBook.Application.Common.Models;

namespace ClubLogBook.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AircraftController : Controller
    {
       
       
        IMediator mediator;
        public AircraftController( IMediator mediator)
        {
            
            this.mediator = mediator;
            // this.logger = logger;
        }
        // GET: api/Aircraft
        [HttpGet]
        //[Route("api/Aircraft/AircraftGet")]
        public async  Task<AircraftListModel> GetAllAircraft()
        {
            CancellationToken ct = new CancellationToken();
            //List<Aircraft> air = await aircraftManagerService.GetAllAircraftsInClub("BAZ");
            //List<Aircraft> aircraftAll = await aircraftManagerService.GetAllAircraftsInClub("");
            //List<AircraftPrice> aircraftPrices = await aircraftManagerService.GetAircraftPrices();
            //List<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
            //mapper.Map(aircraftAll, aircraftViewModel);
            try
            {
                return  await mediator.Send(new GetAircraftListQuery(), ct);
            }
            catch(Exception ex)
            {
                return new AircraftListModel();
            }
            //return aircraftViewModel;
            //return new List<AircraftViewModel>();
        }
        [HttpGet]
        //[Route("api/Aircraft/AircraftGet")]
        public async Task<AircraftListModel> GetClubAircraft(string clubName)
        {
            CancellationToken ct = new CancellationToken();
            //List<Aircraft> air = await aircraftManagerService.GetAllAircraftsInClub("BAZ");
            //List<Aircraft> aircraftAll = await aircraftManagerService.GetAllAircraftsInClub("");
            //List<AircraftPrice> aircraftPrices = await aircraftManagerService.GetAircraftPrices();
            //List<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
            //mapper.Map(aircraftAll, aircraftViewModel);
            try
            {
                return await mediator.Send(new GetClubAircraftListQuery(QueryBy.Name,clubName,0) { PageIndex=1,PageSize=30}, ct);
            }
            catch (Exception ex)
            {

            }
            //return aircraftViewModel;
            return new AircraftListModel();
        }
        [HttpPut]
        public async Task<ActionResult> CreateAircraft([FromBody] AircraftModel airCraftViewModel )
        {
            var result = await mediator.Send(new CreateAircraftCommand() { aircraftViewModel = airCraftViewModel });
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> DeleteAircraft([FromBody] int id)
        {
            var result = await mediator.Send(new DeleteAircraftCommand(id) );
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateAircraft([FromBody] AircraftModel aircraftViewModel)
        {
            var result =  await mediator.Send(new UpdateAircraftCommand(aircraftViewModel));
            return Ok(result);
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
