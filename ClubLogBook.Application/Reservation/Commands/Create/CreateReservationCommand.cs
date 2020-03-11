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
	public class CreateReservationCommand : IRequest<int>
	{
		public FlightReservationModel FlightReservationModel { get; set; }
		public CreateReservationCommand(FlightReservationModel flightReservationModel)
		{
			FlightReservationModel = flightReservationModel;
		}
	}
	public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public CreateReservationCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
		{
			int result = 0;
			request.FlightReservationModel.CombineTime();
			AircraftReservation aircraftReservation = _mapper.Map<FlightReservationModel, AircraftReservation>(request.FlightReservationModel);

			var reservation = _context.Set<AircraftReservation>().Update(aircraftReservation);
			if (reservation != null)
			{
				result = await _context.SaveChangesAsync(cancellationToken);
			}
			return result;
		}
	}

	public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
	{
		public CreateReservationCommandValidator()
		{
			RuleFor(x => x.FlightReservationModel.Id).Equal(0).WithMessage("ReservationId Must  0");
		}
	}
}
