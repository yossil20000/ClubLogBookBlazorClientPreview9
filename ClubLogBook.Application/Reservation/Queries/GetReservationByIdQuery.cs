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
	public class GetReservationByIdQuery : IRequest<FlightReservationModel>
	{
		public int ReservationId { get; set; }
		public GetReservationByIdQuery(int reservationId)
		{
			ReservationId = reservationId;
		}
	}
	public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, FlightReservationModel>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public GetReservationByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<FlightReservationModel> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
		{
			FlightReservationModel flightReservationModel = new FlightReservationModel();
			var reservation =  _context.Set<AircraftReservation>().Find(request.ReservationId);
			if (reservation != null)
			{
				flightReservationModel = _mapper.Map<AircraftReservation, FlightReservationModel>(reservation);
				flightReservationModel.ExtructTime();
				
			}
			return flightReservationModel;
		}
	}

	public class GetReservationByIdQueryValidator : AbstractValidator<GetReservationByIdQuery>
	{
		public GetReservationByIdQueryValidator()
		{
			RuleFor(x => x.ReservationId).GreaterThan(0).WithMessage("ReservationId Must Be Greter Then 0");
		}
	}
}
