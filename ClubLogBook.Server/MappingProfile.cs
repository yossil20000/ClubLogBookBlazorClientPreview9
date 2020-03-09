using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubLogBook.Core.Entities;
using ClubLogBook.Application.Models;
using ClubLogBook.Shared;
using ClubLogBook.Server.Models;

namespace ClubLogBook.Server
{
	public class MappingProfile : Profile
	{

		public MappingProfile()
		{

			CreateMap<Flight, ClubFlightModel>().PreserveReferences().MaxDepth(4);
			CreateMap<ClubFlightModel,Flight>().PreserveReferences().MaxDepth(4);
			
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
			CreateMap<Aircraft, AirplaneSelectModel>();
			CreateMap<AircraftReservation, FlightReservationModel>().ReverseMap();
			CreateMap<ApplicationUser, AdminUserInfo>();
			CreateMap<AdminUserInfo, ApplicationUser>();
			CreateMap<Address, AddressModel>();
			CreateMap<Phone, PhoneModel>();
			CreateMap<EMAIL, EMAILModel>();
			CreateMap<ClubContactsModel, Pilot>();
			CreateMap<Pilot, ClubContactsModel>();
			CreateMap<AddressModel,Address>();
			CreateMap<PhoneModel,Phone >();
			CreateMap<EMAILModel,EMAIL >();
			CreateMap<Account, AccountModel>().PreserveReferences().MaxDepth(4);
			CreateMap<Invoice, InvoiceModel>().PreserveReferences().MaxDepth(4); ;
			CreateMap<AircraftPrice, AircraftPriceModel>().PreserveReferences().MaxDepth(4); 
			CreateMap<Transaction, TransactionModel>().PreserveReferences().MaxDepth(4);
			//CreateMap<List<Transaction>, List<TransactionViewModel>>();
			CreateMap<Aircraft, AircraftModel>();
			CreateMap<AirCraftModel, AirCraftModelModel>().ReverseMap();
			CreateMap<AircraftClass, AircraftClassModel>().ReverseMap();
			CreateMap<AircraftCategory, AircraftCategoryModel>().ReverseMap();
			CreateMap<AircraftState, AircraftStateModel>().ReverseMap();

		}

	}
	public class AccountManagerProfile : Profile
	{

		public AccountManagerProfile()
		{

		//	CreateMap<Flight, ClubFlightViewModel>().PreserveReferences().MaxDepth(4);
		//	CreateMap<ClubFlightViewModel, Flight>().PreserveReferences().MaxDepth(4);
		//	CreateMap<BaseEntity, BaseEntity>();
		//	CreateMap<Aircraft, Aircraft>();
		//	CreateMap<AirCraftModel, AirCraftModel>();
		//	CreateMap<Pilot, Pilot>();
		//	CreateMap<Contact, Contact>();
		//	CreateMap<Address, Address>();
		//	CreateMap<Phone, Phone>();
		//	CreateMap<EMAIL, EMAIL>();
		//	CreateMap<Endorsment, Endorsment>();
		//	CreateMap<License, License>();
		//	CreateMap<Checkride, Checkride>();
		//	CreateMap<Aircraft, AirplaneSelectViewModel>();
		//	CreateMap<AircraftReservation, FlightReservationViewModel>().ReverseMap();
			CreateMap<ApplicationUser, AdminUserInfo>();
		//	CreateMap<AdminUserInfo, ApplicationUser>();
		//	CreateMap<Address, AddressViewModel>();
		//	CreateMap<Phone, PhoneViewModel>();
		//	CreateMap<EMAIL, EMAILVieModel>();
		//	CreateMap<ClubContactsViewModel, Pilot>();
		//	CreateMap<Pilot, ClubContactsViewModel>();
		//	CreateMap<AddressViewModel, Address>();
		//	CreateMap<PhoneViewModel, Phone>();
		//	CreateMap<EMAILVieModel, EMAIL>();
		//	CreateMap<Invoice, InvoiceViewModel>().PreserveReferences().MaxDepth(4); ;
		//	CreateMap<AircraftPrice, AircraftPriceViewModel>().PreserveReferences().MaxDepth(4); 
		//	CreateMap<Transaction, TransactionViewModel>().PreserveReferences().MaxDepth(4); 


		}

	}
	public static class MapperUtils
	{
		private static AutoMapper.MapperConfiguration mapperConfig;
		private static AutoMapper.IMapper iMapper;
		public static void MapperInit(Type src, Type dst)
		{
			mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
			{
				cfg.CreateMap(src, dst);
				cfg.AllowNullCollections=true;
				cfg.AllowNullDestinationValues = true;
				
			});

			iMapper = mapperConfig.CreateMapper();
			iMapper.ConfigurationProvider.CompileMappings();
		}
		public static void MapperMap<T, T1>(T src, T1 dst)
		{
			if (mapperConfig == null)
			{
				throw new Exception("Must call MapperInit !!");
			}
			iMapper.Map<T, T1>(src, dst);
		}
	}
}
