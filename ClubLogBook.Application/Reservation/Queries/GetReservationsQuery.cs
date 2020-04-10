using System.Linq;
using System.Collections.Generic;
using System.Text;
using MediatR;
using ClubLogBook.Application.Accounts.Queries.GetAccountsList;
using System.Threading.Tasks;
using System.Threading;
using ClubLogBook.Application.Common.Interfaces;
using ClubLogBook.Core.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ClubLogBook.Application.Models;
using ClubLogBook.Core.Interfaces;
using System;
namespace ClubLogBook.Application.Reservation.Queries
{
	public class GetReservationsQuery : IRequest<List<FlightReservationModel>>
	{
	}
	public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, List<FlightReservationModel>>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public GetReservationsQueryHandler(IApplicationDbContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<FlightReservationModel>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
		{
			var reservations = await _context.Set<AircraftReservation>().ProjectTo<FlightReservationModel>(_mapper.ConfigurationProvider).ToListAsync();
			foreach (var r in reservations)
			{
				var pilot =  _context.Set<Pilot>().Find(r.PilotId);
				r.UserId = pilot == null ? string.Empty : pilot.UserId;
				r.FirstName = pilot.FirstName; r.LastName = pilot.LastName;
				
			}
			return reservations;
		}
	}

	public class GetReservationsQueryValidator : AbstractValidator<GetReservationsQuery>
	{
		public GetReservationsQueryValidator()
		{

		}
	}
}
