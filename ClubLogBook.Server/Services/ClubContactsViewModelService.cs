using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Application.ViewModels;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Core.Entities;
using ClubLogBook.Infrastructure;
using ClubLogBook.Application.Extenttions;
using AutoMapper;
namespace ClubLogBook.Server.Services
{
	public class ClubContactsViewModelService : IClubContactsViewModelService
	{
		private readonly IClubService _clubService;
		private readonly IMemberService _memberService;
		private readonly IMapper _mapper;
		public ClubContactsViewModelService(IClubService clubService,IMemberService memberService,IMapper mapper)
		{
			_clubService = clubService;
			_memberService = memberService;
			_mapper = mapper;
		}
		public async Task<IEnumerable<ClubContactsViewModel>> GetPilotsNotInAnyClub()
		{
			
			List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			try
			{

				IEnumerable<Pilot> pilots = await _memberService.GetAllPilotNotInClub();
				foreach (var p in pilots)
				{
					ClubContactsViewModel ccVM = new ClubContactsViewModel();
					_mapper.Map<Pilot, ClubContactsViewModel>(p, ccVM);
					//ccVM.CopyPilot(p);
					ccVM.Id = p.Id; ccVM.IdNumber = p.IdNumber; ccVM.FullName = $"{p.FirstName} {p.LastName}";
					ccVM.Gender = (Gender)p.Gender;
					ccVM.DateOfBirth = p.DateOfBirth == null ? DateTime.Now : p.DateOfBirth;
					


					contacts.Add(ccVM);
				}

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return contacts;

		}
		public async Task<List<ClubContactsViewModel>> GetAllPilot()
		{

			List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			try
			{

				IEnumerable<Pilot> pilots = await _memberService.GetAllPilot();
				foreach (var p in pilots)
				{
					if (p.UserId == null) p.UserId = string.Empty;
					ClubContactsViewModel ccVM = new ClubContactsViewModel();
					_mapper.Map<Pilot, ClubContactsViewModel>(p, ccVM);
					//ccVM.CopyPilot(p);
					ccVM.Id = p.Id; ccVM.IdNumber = p.IdNumber; ccVM.FullName = $"{p.FirstName} {p.LastName}";
					ccVM.Gender = (Gender)p.Gender;
					ccVM.DateOfBirth = p.DateOfBirth == null ? DateTime.Now : p.DateOfBirth;
					if (p.Contact != null)
					{
						_mapper.Map<ICollection<Address>, List<AddressViewModel>>(p.Contact.Addresses, ccVM.Addresses);
						_mapper.Map<ICollection<Phone>, List<PhoneViewModel>>(p.Contact.Phones, ccVM.Phones);
						_mapper.Map<ICollection<EMAIL>, List<EMAILVieModel>>(p.Contact.EMAILs, ccVM.Emails);

					}

					if (ccVM.Phones.Count == 0)
					{
						ccVM.Phones.Add(new PhoneViewModel());
					}
					if (ccVM.Emails.Count == 0)
					{
						ccVM.Emails.Add(new EMAILVieModel());
					}
					if (ccVM.Addresses.Count == 0)
					{
						ccVM.Addresses.Add(new AddressViewModel());
					}
					contacts.Add(ccVM);
				}

			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return contacts;

		}
		public async Task<IEnumerable<ClubContactsViewModel>> GetOrCreateClubContact(string clubName , bool include)
		{
			List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			try
			{
							
				var pilots = await _clubService.GetClubMembers(clubName);
				foreach (var p in pilots)
				{
					if (p.UserId == null) p.UserId = string.Empty;
					ClubContactsViewModel ccVM = new ClubContactsViewModel();
					_mapper.Map<Pilot, ClubContactsViewModel>(p, ccVM);
					//ccVM.CopyPilot(p);
					ccVM.Id = p.Id; ccVM.IdNumber = p.IdNumber; ccVM.FullName = $"{p.FirstName} {p.LastName}";
					ccVM.Gender = (Gender)p.Gender;
					ccVM.DateOfBirth = p.DateOfBirth == null ? DateTime.Now : p.DateOfBirth;
					ccVM.UserId = p.UserId;
					if(p.Contact != null)
					{
						_mapper.Map<ICollection<Address>, List<AddressViewModel>>(p.Contact.Addresses, ccVM.Addresses);
						_mapper.Map<ICollection<Phone>, List<PhoneViewModel>>(p.Contact.Phones, ccVM.Phones);
						_mapper.Map<ICollection<EMAIL>, List<EMAILVieModel>>(p.Contact.EMAILs, ccVM.Emails);
						
					}
					
					if(ccVM.Phones.Count == 0)
					{
						ccVM.Phones.Add(new PhoneViewModel());
					}
					if (ccVM.Emails.Count == 0)
					{
						ccVM.Emails.Add(new EMAILVieModel());
					}
					if (ccVM.Addresses.Count == 0)
					{
						ccVM.Addresses.Add(new AddressViewModel());
					}
					contacts.Add(ccVM);
				}
				
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
			return contacts;
		}
		//public async Task<bool> UpdateOrCreateClubContactMember(string clubName, ClubContactsViewModel clubContactUpdateViewModel)
		//{
		//	List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
		//	//var pilot = await _clubService.GetPilotById(clubContactUpdateViewModel.Id);
		//	Pilot pilot = clubContactUpdateViewModel as Pilot;
		//	//var members = await _clubService.GetClubMembers(clubName);
		//	/*var exist = _memberService.IsExist(pilot);
		//	if(exist.Result == false)
		//	{
		//		var p = await _memberService.AddPilot(pilot);
		//	}
		//	*/
		//	await _clubService.AddContactToClub(clubName, pilot);
		//	return await Task.FromResult<bool>(true);
		//}
		public async Task<bool> UpdateOrCreateClubContactMember(string clubName, ClubContactsViewModel objects)
		{
			List<ClubContactsViewModel> contacts = new List<ClubContactsViewModel>();
			//var pilot = await _clubService.GetPilotById(clubContactUpdateViewModel.Id);
			Pilot pilot = new Pilot();
			_mapper.Map<ClubContactsViewModel,Pilot>(objects,pilot);
			_mapper.Map<List<AddressViewModel>,ICollection<Address>>(objects.Addresses , pilot.Contact.Addresses);
			_mapper.Map< List<PhoneViewModel>,ICollection<Phone>>(objects.Phones, pilot.Contact.Phones);
			_mapper.Map<List<EMAILVieModel>,ICollection<EMAIL>>(objects.Emails, pilot.Contact.EMAILs);
			//var members = await _clubService.GetClubMembers(clubName);
			/*var exist = _memberService.IsExist(pilot);
			if(exist.Result == false)
			{
				var p = await _memberService.AddPilot(pilot);
			}
			*/
			if(pilot.Id == 0)
			{
				await _memberService.AddPilot(pilot);
			}
			await _clubService.AddContactToClub(clubName, pilot);
			return await Task.FromResult<bool>(true);
		}
	}
}
