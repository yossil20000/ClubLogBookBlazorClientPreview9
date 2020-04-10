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
	public class IsReservationValidQuery : IRequest<bool>
	{
		public FlightReservationModel FlightReservationModel { get; set; }
		public IsReservationValidQuery(FlightReservationModel flightReservationModel)
		{
			FlightReservationModel = flightReservationModel;
		}
	}
	public class IsReservationValidQueryHandler : IRequestHandler<IsReservationValidQuery, bool>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public IsReservationValidQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<bool> Handle(IsReservationValidQuery request, CancellationToken cancellationToken)
		{
			
			//request.FlightReservationModel.CombineTime();
			AircraftReservation aircraftReservation = _mapper.Map<FlightReservationModel, AircraftReservation>(request.FlightReservationModel);

			//var reservation = _context.Set<AircraftReservation>().Update(aircraftReservation);
			//if (reservation != null)
			//{
			//	result = await _context.SaveChangesAsync(cancellationToken);
			//}
			//return result;

			var reservations = await _context.Set<AircraftReservation>().ToListAsync();
			var exist = reservations.Where(f => f.Intersect(aircraftReservation)).FirstOrDefault();
			if (exist != null && exist.Id != aircraftReservation.Id)
				return false;
			return true;
		}
	}

	public class IsReservationValidQueryValidator : AbstractValidator<IsReservationValidQuery>
	{
		public IsReservationValidQueryValidator()
		{
			RuleFor(x => x.FlightReservationModel.Id).GreaterThan(0).WithMessage("ReservationId Must Be Greter Then 0");
		}
	}
}
