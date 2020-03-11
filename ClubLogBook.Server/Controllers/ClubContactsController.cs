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
using ClubLogBook.Application.ClubContact.Commands;
using System.Threading;

namespace ClubLogBook.Server.Controllers
{
	//[Route("api/[controller]")]
	
	public class ClubContactsController : Controller
	{
		//IClubService _clubService;
		//IClubContactsViewModelService _clubContactsViewModelService;

		//private IMemberService _memberService;
		private IMapper _mapper;
		private IMediator _mediator;
		public ClubContactsController(IMediator mediator, /*IMemberService memberService, IClubContactsViewModelService clubContactsViewModelService, IClubService clubService,*/ IMapper mapper)
		{
			_mapper = mapper;
			_mediator = mediator;
			//_clubService = clubService;
			//_clubContactsViewModelService = clubContactsViewModelService;
			
			//_memberService = memberService;
		}

		[HttpGet]
		[Route("api/ClubContacts/Pilots")]
		public async Task<List<ClubContactsModel>> Pilots()
		{
			
			GetClubContactsQuery getClubContactsQuery = new GetClubContactsQuery("BAZ");
			var contactsList = await _mediator.Send(getClubContactsQuery);
			
			return contactsList.ClubContactsModelList;
		}
		[HttpGet]
		[Route("api/ClubContacts/Members")]
		public async Task<List<ClubContactsModel>> Members()
		{
			
			GetAllContactsQuery getAllContactsQuery = new GetAllContactsQuery();
			var clubContactModelList = await _mediator.Send(getAllContactsQuery);

			return clubContactModelList.ClubContactsModelList;
		}
		[HttpGet]
		[Route("api/ClubContacts/PilotsNotInClub")]
		public async Task<IEnumerable<ClubContactsModel>> PilotsNotInClub()
		{
			
			GetContactsNotInClubQuery getContactsNotInClubQuery = new GetContactsNotInClubQuery();
			var contactsList = await _mediator.Send(getContactsNotInClubQuery);

			return contactsList.ClubContactsModelList;

		}
		// GET: ClubContacts/Details/5
		[HttpGet]
		[Route("api/ClubContacts/Details/{id}")]
		public async Task<ClubContactsModel> Details(int id)
		{
			//UserInfo userInfo = new UserInfo(pilot);
			//string user = userInfo.GetJason();
			ClubContactsModel clubContactsModel = new ClubContactsModel();
			GetClubContactByIdQuery getClubContactByIdQuery = new GetClubContactByIdQuery(id);
			clubContactsModel = await _mediator.Send(getClubContactByIdQuery);

			return clubContactsModel;
			
		}

		[HttpGet]
		[Route("api/ClubContacts/DetailsForDelete/{id}")]
		public async Task<ClubContactsModel> DetailsForDelete(int id)
		{
			//UserInfo userInfo = new UserInfo(pilot);
			//string user = userInfo.GetJason();
			ClubContactsModel clubContactsModel = new ClubContactsModel();
			GetClubContactByIdQuery getClubContactByIdQuery = new GetClubContactByIdQuery(id);
			clubContactsModel = await _mediator.Send(getClubContactByIdQuery);

			return clubContactsModel;
		}
		
		[HttpPut]
		[Route("api/ClubContacts/Create")]
		public async Task<OkResult> Create([FromBody] ClubContactsModel clubContactModel)
		{
			ClubContactsModel clubContactsModel = new ClubContactsModel();
			if (true/*ModelState.IsValid*/)
			{

				CreateContactCommand createContactCommand = new CreateContactCommand(clubContactModel);
				clubContactsModel = await _mediator.Send(createContactCommand);
			}
			return clubContactsModel.Id > 0 ? Ok() : Ok();
		}

		[HttpPut]
		[Route("api/ClubContacts/Add")]
		public async Task<int>  Add([FromBody] int id)
		{
			if (true/*ModelState.IsValid*/)
			{
				AddContactToClubCommand addContactToClubCommand = new AddContactToClubCommand("BAZ", id);
				var result = await _mediator.Send(addContactToClubCommand);
				return result;
			}
			return 0;
		}

		[HttpPut]
		[Route("api/ClubContacts/Edit")]
		public async Task<int> Edit([FromBody] ClubContactsModel clubContactModel)
		{
			if (true/*ModelState.IsValid*/)
			{
				UpdateContactCommand updateContactCommand = new UpdateContactCommand(clubContactModel);
				var result = await _mediator.Send(updateContactCommand);
				return result;
			}
			return 0;

		}

		[HttpPut]
		[Route("api/ClubContacts/Delete/{id}")]
		public async Task<int> Delete(int id,[FromBody] string clubName)
        {

			if (ModelState.IsValid)
			{
				DeleteContactCommand deleteContactCommand = new DeleteContactCommand(id);
				var result = await _mediator.Send(deleteContactCommand);
				return result;
			}
			return 0;
		}
		[HttpPut]
		[Route("api/ClubContacts/DeleteMember")]
		public async Task<int> DeleteMember([FromBody] int id)
		{
			if (ModelState.IsValid)
			{
				CancellationToken ct = new CancellationToken();
				DeleteContactFromClubCommand deleteContactFromClubCommand = new DeleteContactFromClubCommand("BAZ", id);
				var result = await _mediator.Send(deleteContactFromClubCommand,ct);
				return await Task.FromResult(result);
			}
			return 0;

		}
		[HttpPut]
		[Route("api/ClubContacts/Update")]
		public async Task<int> Update([FromBody] ClubContactsModel clubContactModel)
		{
			////if (ModelState.IsValid)
			//{


			//	await _clubContactsViewModelService.UpdateOrCreateClubContactMember("BAZ", clubContactViewModel);
			//	System.Diagnostics.Debug.WriteLine($"ClubContactsController::Update contact.GetFullName()");
				
				
			//}
			if (ModelState.IsValid)
			{
				UpdateContactCommand updateContactCommand = new UpdateContactCommand(clubContactModel);
				var result = await _mediator.Send(updateContactCommand);
				return result;
			}
			return 0;
		}
	}
}