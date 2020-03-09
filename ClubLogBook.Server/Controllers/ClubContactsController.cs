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
using ClubLogBook.Application.Extenttions;
using AutoMapper;
using MediatR;
using ClubLogBook.Application.ClubContact.Queries;

namespace ClubLogBook.Server.Controllers
{
	//[Route("api/[controller]")]
	
	public class ClubContactsController : Controller
	{
		IClubService _clubService;
		IClubContactsViewModelService _clubContactsViewModelService;
		private IMapper _mapper;
		private IMemberService _memberService;
		private IMediator _mediator;
		public ClubContactsController(IMediator mediator, IMemberService memberService, IClubContactsViewModelService clubContactsViewModelService, IClubService clubService, IMapper mapper)
		{
			_mediator = mediator;
			_clubService = clubService;
			_clubContactsViewModelService = clubContactsViewModelService;
			_mapper = mapper;
			_memberService = memberService;
		}

		[HttpGet]
		[Route("api/ClubContacts/Pilots")]
		public async Task<List<ClubContactsModel>> Pilots()
		{
			List<ClubContactsModel> contacts = new List<ClubContactsModel>();
			GetClubContactsQuery getClubContactsQuery = new GetClubContactsQuery("BAZ");
			var contactsList = await _mediator.Send(getClubContactsQuery);
			
			return contactsList.ClubContactsModelList;
		}
		[HttpGet]
		[Route("api/ClubContacts/Members")]
		public async Task<List<ClubContactsModel>> Members()
		{
			//List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			var contacts = await _clubContactsViewModelService.GetAllPilot();
			return contacts;
		}
		[HttpGet]
		[Route("api/ClubContacts/PilotsNotInClub")]
		public async Task<IEnumerable<ClubContactsModel>> PilotsNotInClub()
		{
			List<ClubContactsModel> contacts = new List<ClubContactsModel>();
			return await _clubContactsViewModelService.GetPilotsNotInAnyClub();
			
		}
		// GET: ClubContacts/Details/5
		[HttpGet]
		[Route("api/ClubContacts/Details/{id}")]
		public ClubContactsModel Details(int id)
		{
			var club = _clubService.GetClubMembers("BAZ");
			Pilot pilot = club.Result.Where(i => i.Id == id).FirstOrDefault();
			UserInfo userInfo = new UserInfo(pilot);
			string user = userInfo.GetJason();
			ClubContactsModel pMode = new ClubContactsModel();
			_mapper.Map<Pilot, ClubContactsModel>(pilot, pMode);
			
			return pMode;
		}

		[HttpGet]
		[Route("api/ClubContacts/DetailsForDelete/{id}")]
		public ClubContactsModel DetailsForDelete(int id)
		{
			var club = _clubService.GetClubMembers("BAZ");
			Pilot p = club.Result.Where(i => i.Id == id).FirstOrDefault();
			ClubContactsModel pMode = new ClubContactsModel();
			//pMode.CopyPilot(p);
			return pMode;
		}
		
		[HttpPut]
		[Route("api/ClubContacts/Create")]
		public async Task Create([FromBody] ClubContactsModel clubContactUpdateViewModel)
		{
			if (ModelState.IsValid)
			{
				Pilot pilot = new Pilot();
				_mapper.Map<ClubContactsModel, Pilot>(clubContactUpdateViewModel, pilot);
				await _memberService.UpdatePilot(pilot);
				System.Diagnostics.Debug.WriteLine(pilot.ToString());
				System.Diagnostics.Debug.WriteLine(pilot.ToString());
			}

		}

		[HttpPut]
		[Route("api/ClubContacts/Add")]
		public async Task  Add([FromBody] int id)
		{
			if (ModelState.IsValid)
			{
				await _clubService.AddPilotToClub("BAZ", id);
				System.Diagnostics.Debug.WriteLine(id.ToString());
			}

		}

		[HttpPut]
		[Route("api/ClubContacts/Edit")]
		public async Task Edit([FromBody] ClubContactsModel clubContactUpdateViewModel)
		{
			if (ModelState.IsValid)
			{
				Pilot pilot = await _memberService.GetPilotById(clubContactUpdateViewModel.Id);
				_mapper.Map<ClubContactsModel, Pilot>(clubContactUpdateViewModel, pilot);
				await _memberService.UpdatePilot(pilot);
				System.Diagnostics.Debug.WriteLine(pilot.ToString());
			}

		}

		[HttpPut]
		[Route("api/ClubContacts/Delete/{id}")]
		public async Task Delete(int id,[FromBody] string clubName)
        {
			if(ModelState.IsValid)
			{
				await _clubService.RemoveContactFromClub(clubName, id);
				System.Diagnostics.Debug.WriteLine(id.ToString());
			}
		}
		[HttpPut]
		[Route("api/ClubContacts/DeleteMember")]
		public async Task DeleteMember([FromBody] int id)
		{
			if (ModelState.IsValid)
			{
				await _clubService.RemoveContactFromClub("BAZ", id);
				await _memberService.DeletePilotById(id);
				System.Diagnostics.Debug.WriteLine(id.ToString());
			}
		}
		[HttpPut]
		[Route("api/ClubContacts/Update")]
		public async Task Update([FromBody] ClubContactsModel clubContactViewModel)
		{
			//if (ModelState.IsValid)
			{


				await _clubContactsViewModelService.UpdateOrCreateClubContactMember("BAZ", clubContactViewModel);
				System.Diagnostics.Debug.WriteLine($"ClubContactsController::Update contact.GetFullName()");
				
				
			}

		}
	}
}