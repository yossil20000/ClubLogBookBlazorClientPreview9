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
using ClubLogBook.Application.Extenttions;
using AutoMapper;
namespace ClubLogBook.Server.Controllers
{
	//[Route("api/[controller]")]
	
	public class ClubContactsController : Controller
	{
		IClubService _clubService;
		IClubContactsViewModelService _clubContactsViewModelService;
		private IMapper _mapper;
		private IMemberService _memberService;
		public ClubContactsController(IMemberService memberService, IClubContactsViewModelService clubContactsViewModelService, IClubService clubService, IMapper mapper)
		{
			_clubService = clubService;
			_clubContactsViewModelService = clubContactsViewModelService;
			_mapper = mapper;
			_memberService = memberService;
		}

		[HttpGet]
		[Route("api/ClubContacts/Pilots")]
		public async Task<List<ClubContactsViewModel>> Pilots()
		{
			List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			//Account account = new Account();
			//AircraftPrice ap = new AircraftPrice();
			//AircraftPriceViewModel apvm = new AircraftPriceViewModel();
			//Invoice invoice = new Invoice();
			//InvoiceViewModel invoiceViewModel = new InvoiceViewModel();
			//AccountViewModel accountViewModel = new AccountViewModel();
			//Transaction transaction = new Transaction();
			//TransactionViewModel transactionViewModel = new TransactionViewModel();
			//_mapper.Map(ap, apvm);
			//invoice.Amount = 90;
			//invoice.InvoiceState = InvoiceStateVieModel.Deleted;
			//transaction.Invoice = invoice;
			//account.Transactions.Add(transaction);
			//account.Transactions.Add(transaction);
			//_mapper.Map(invoice, invoiceViewModel);
			//_mapper.Map(transaction, transactionViewModel);
			//_mapper.Map(account, accountViewModel);
			var pilots = await  _clubContactsViewModelService.GetOrCreateClubContact("BAZ");
			return pilots.ToList();
		}
		[HttpGet]
		[Route("api/ClubContacts/Members")]
		public async Task<List<ClubContactsViewModel>> Members()
		{
			//List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			var contacts = await _clubContactsViewModelService.GetAllPilot();
			return contacts;
		}
		[HttpGet]
		[Route("api/ClubContacts/PilotsNotInClub")]
		public async Task<IEnumerable<ClubContactsViewModel>> PilotsNotInClub()
		{
			List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			return await _clubContactsViewModelService.GetPilotsNotInAnyClub();
			
		}
		// GET: ClubContacts/Details/5
		[HttpGet]
		[Route("api/ClubContacts/Details/{id}")]
		public ClubContactsViewModel Details(int id)
		{
			var club = _clubService.GetClubMembers("BAZ");
			Pilot pilot = club.Result.Where(i => i.Id == id).FirstOrDefault();
			UserInfo userInfo = new UserInfo(pilot);
			string user = userInfo.GetJason();
			ClubContactsViewModel pMode = new ClubContactsViewModel();
			_mapper.Map<Pilot, ClubContactsViewModel>(pilot, pMode);
			
			return pMode;
		}

		[HttpGet]
		[Route("api/ClubContacts/DetailsForDelete/{id}")]
		public ClubContactsViewModel DetailsForDelete(int id)
		{
			var club = _clubService.GetClubMembers("BAZ");
			Pilot p = club.Result.Where(i => i.Id == id).FirstOrDefault();
			ClubContactsViewModel pMode = new ClubContactsViewModel();
			//pMode.CopyPilot(p);
			return pMode;
		}
		
		[HttpPut]
		[Route("api/ClubContacts/Create")]
		public async Task Create([FromBody] ClubContactsViewModel clubContactUpdateViewModel)
		{
			if (ModelState.IsValid)
			{
				Pilot pilot = new Pilot();
				_mapper.Map<ClubContactsViewModel, Pilot>(clubContactUpdateViewModel, pilot);
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
		public async Task Edit([FromBody] ClubContactsViewModel clubContactUpdateViewModel)
		{
			if (ModelState.IsValid)
			{
				Pilot pilot = await _memberService.GetPilotById(clubContactUpdateViewModel.Id);
				_mapper.Map<ClubContactsViewModel, Pilot>(clubContactUpdateViewModel, pilot);
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
		public async Task Update([FromBody] ClubContactsViewModel clubContactViewModel)
		{
			//if (ModelState.IsValid)
			{


				await _clubContactsViewModelService.UpdateOrCreateClubContactMember("BAZ", clubContactViewModel);
				System.Diagnostics.Debug.WriteLine($"ClubContactsController::Update contact.GetFullName()");
				
				
			}

		}
	}
}