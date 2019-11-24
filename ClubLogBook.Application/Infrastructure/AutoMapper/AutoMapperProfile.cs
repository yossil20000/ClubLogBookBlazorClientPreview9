using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using AutoMapper;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.ViewModels;
namespace ClubLogBook.Application.Infrastructure.AutoMapper
{
	public class AutoMapperProfile:Profile
	{
		public AutoMapperProfile()
		{
			LoadStandardMappings();
			LoadCustomMappings();
			LoadConverters();
			MappingProfile();
		}
		public void MappingProfile()
		{

			CreateMap<Flight, ClubFlightViewModel>().PreserveReferences().MaxDepth(4);
			CreateMap<ClubFlightViewModel, Flight>().PreserveReferences().MaxDepth(4);
			CreateMap<BaseEntity, BaseEntity>();
			CreateMap<Aircraft, Aircraft>();
			CreateMap<AirCraftModel, AirCraftModel>();
			CreateMap<Pilot, Pilot>();
			CreateMap<Contact, Contact>();
			CreateMap<Address, Address>();
			CreateMap<Phone, Phone>();
			CreateMap<EMAIL, EMAIL>();
			CreateMap<Endorsment, Endorsment>();
			CreateMap<License, License>();
			CreateMap<Checkride, Checkride>();
			CreateMap<Aircraft, AirplaneSelectViewModel>();
			CreateMap<AircraftReservation, FlightReservationViewModel>().ReverseMap();
			//CreateMap<ApplicationUser, AdminUserInfo>();
			//CreateMap<AdminUserInfo, ApplicationUser>();
			CreateMap<Address, AddressViewModel>();
			CreateMap<Phone, PhoneViewModel>();
			CreateMap<EMAIL, EMAILVieModel>();
			CreateMap<ClubContactsViewModel, Pilot>();
			CreateMap<Pilot, ClubContactsViewModel>();
			CreateMap<AddressViewModel, Address>();
			CreateMap<PhoneViewModel, Phone>();
			CreateMap<EMAILVieModel, EMAIL>();
			CreateMap<Account, AccountViewModel>().PreserveReferences().MaxDepth(4);
			CreateMap<Invoice, InvoiceViewModel>().PreserveReferences().MaxDepth(4); ;
			CreateMap<AircraftPrice, AircraftPriceViewModel>().PreserveReferences().MaxDepth(4);
			CreateMap<Transaction, TransactionViewModel>().PreserveReferences().MaxDepth(4);
			//CreateMap<List<Transaction>, List<TransactionViewModel>>();
			CreateMap<Aircraft, AircraftViewModel>();
			CreateMap<AirCraftModel, AirCraftModelViewModel>().ReverseMap();
			CreateMap<AircraftClass, AircraftClassViewModel>().ReverseMap();
			CreateMap<AircraftCategory, AircraftCategoryViewModel>().ReverseMap();
			CreateMap<AircraftState, AircraftStateViewModel>().ReverseMap();

		}
		private void LoadConverters()
		{

		}
		private void LoadStandardMappings()
		{
			var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());
			foreach(var map in mapsFrom)
			{
				CreateMap(map.Source, map.Destination).ReverseMap();
			}
		}

		private void LoadCustomMappings()
		{
			var mapsFrom = MapperProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());
			foreach(var map in mapsFrom)
			{
				map.CreateMappings(this);
			}
		}
	}
}
