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
	public class GetClubContactByIdQuery : IRequest<ClubContactsModel>
	{
		public int ContactId { get; set; }
		public GetClubContactByIdQuery(int contactId)
		{
			ContactId = contactId;
		}
	}
	public class GetClubContactByIdQueryHandler : IRequestHandler<GetClubContactByIdQuery, ClubContactsModel>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public GetClubContactByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<ClubContactsModel> Handle(GetClubContactByIdQuery request, CancellationToken cancellationToken)
		{
			var pilot =  _context.Set<Pilot>().Find(request.ContactId);
			ClubContactsModel clubContact = new ClubContactsModel();
			if (pilot != null)
			{
				if (pilot.UserId == null) pilot.UserId = string.Empty;
				
				_mapper.Map<Pilot, ClubContactsModel>(pilot, clubContact);
				//ccVM.CopyPilot(p);
				clubContact.Id = pilot.Id; clubContact.IdNumber = pilot.IdNumber; clubContact.FullName = $"{pilot.FirstName} {pilot.LastName}";
				clubContact.Gender = (Gender)pilot.Gender;
				clubContact.DateOfBirth = pilot.DateOfBirth == null ? DateTime.Now : pilot.DateOfBirth;
				clubContact.UserId = pilot.UserId;
				if (pilot.Contact != null)
				{
					_mapper.Map<ICollection<Address>, List<AddressModel>>(pilot.Contact.Addresses, clubContact.Addresses);
					_mapper.Map<ICollection<Phone>, List<PhoneModel>>(pilot.Contact.Phones, clubContact.Phones);
					_mapper.Map<ICollection<EMAIL>, List<EMAILModel>>(pilot.Contact.EMAILs, clubContact.Emails);

				}

				if (clubContact.Phones.Count == 0)
				{
					clubContact.Phones.Add(new PhoneModel());
				}
				if (clubContact.Emails.Count == 0)
				{
					clubContact.Emails.Add(new EMAILModel());
				}
				if (clubContact.Addresses.Count == 0)
				{
					clubContact.Addresses.Add(new AddressModel());
				}
				
			}
			//clubContactsList.ClubContactsModelList = _mapper.Map<List<Pilot>, List<ClubContactsModel>>(pilots);
			return await Task.FromResult(clubContact);
		}
	}
	public class GetClubContactByIdQueryValidator : AbstractValidator<GetClubContactByIdQuery>
	{
		public GetClubContactByIdQueryValidator()
		{
			RuleFor(x => x.ContactId).GreaterThan(0).WithMessage("contact Id Must Be Grater then 0");
		}
	}
}
