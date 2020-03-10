using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.Models;
using System.Collections.Generic;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Application.Common.Models;
using FluentValidation;
using System;

namespace ClubLogBook.Application.ClubContact.Queries
{
	public class GetAllContactsQuery : IRequest<ClubContactsList>
	{

	}
	public class GetAllContactsQueryHandler : IRequestHandler<GetAllContactsQuery, ClubContactsList>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public GetAllContactsQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<ClubContactsList> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
		{
			var pilots = await _context.Set<Pilot>().ToListAsync();
			ClubContactsList clubContactsList = new ClubContactsList();
			foreach (var p in pilots)
			{
				if (p.UserId == null) p.UserId = string.Empty;
				ClubContactsModel ccVM = new ClubContactsModel();
				_mapper.Map<Pilot, ClubContactsModel>(p, ccVM);
				//ccVM.CopyPilot(p);
				ccVM.Id = p.Id; ccVM.IdNumber = p.IdNumber; ccVM.FullName = $"{p.FirstName} {p.LastName}";
				ccVM.Gender = (Gender)p.Gender;
				ccVM.DateOfBirth = p.DateOfBirth == null ? DateTime.Now : p.DateOfBirth;
				ccVM.UserId = p.UserId;
				if (p.Contact != null)
				{
					_mapper.Map<ICollection<Address>, List<AddressModel>>(p.Contact.Addresses, ccVM.Addresses);
					_mapper.Map<ICollection<Phone>, List<PhoneModel>>(p.Contact.Phones, ccVM.Phones);
					_mapper.Map<ICollection<EMAIL>, List<EMAILModel>>(p.Contact.EMAILs, ccVM.Emails);

				}

				if (ccVM.Phones.Count == 0)
				{
					ccVM.Phones.Add(new PhoneModel());
				}
				if (ccVM.Emails.Count == 0)
				{
					ccVM.Emails.Add(new EMAILModel());
				}
				if (ccVM.Addresses.Count == 0)
				{
					ccVM.Addresses.Add(new AddressModel());
				}
				clubContactsList.ClubContactsModelList.Add(ccVM);
			}
			//clubContactsList.ClubContactsModelList = _mapper.Map<List<Pilot>, List<ClubContactsModel>>(pilots);
			return await Task.FromResult(clubContactsList);
		}
	}
}
