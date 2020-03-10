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
	public class GetContactsNotInClubQuery : IRequest<ClubContactsList>
	{
		
	}
	public class GetContactsNotInClubQueryHandler : IRequestHandler<GetContactsNotInClubQuery, ClubContactsList>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public GetContactsNotInClubQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<ClubContactsList> Handle(GetContactsNotInClubQuery request, CancellationToken cancellationToken)
		{

			var clubs = _context.Set<Club>().ToList();
			List<Pilot> memeberPilot = _context.Set<Pilot>().ToList();
			List<Pilot> pilots = new List<Pilot>();
			foreach (var p in clubs)
			{
				foreach (var pp in p.Members)
				{
					pilots.Add(pp);
				}
			}

			var membersNotInClub = memeberPilot.Except(pilots);
			ClubContactsList clubContactsList = new ClubContactsList();
			foreach (var p in membersNotInClub)
			{
				ClubContactsModel ccVM = new ClubContactsModel();
				_mapper.Map<Pilot, ClubContactsModel>(p, ccVM);
				//ccVM.CopyPilot(p);
				ccVM.Id = p.Id; ccVM.IdNumber = p.IdNumber; ccVM.FullName = $"{p.FirstName} {p.LastName}";
				ccVM.Gender = (Gender)p.Gender;
				ccVM.DateOfBirth = p.DateOfBirth == null ? DateTime.Now : p.DateOfBirth;



				clubContactsList.ClubContactsModelList.Add(ccVM);
			}
			
			return await Task.FromResult(clubContactsList);
		}
	}
	
}
