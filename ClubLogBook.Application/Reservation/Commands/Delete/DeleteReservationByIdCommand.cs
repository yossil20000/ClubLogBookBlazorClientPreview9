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
	public class DeleteReservationByIdCommand : IRequest<int>
	{
		public int ReservationId { get; set; }
		public DeleteReservationByIdCommand(int id)
		{
			ReservationId = id;
		}
	}
	public class DeleteReservationByIdCommandHandler : IRequestHandler<DeleteReservationByIdCommand, int>
	{
		IApplicationDbContext _context;
		IMapper _mapper;
		public DeleteReservationByIdCommandHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<int> Handle(DeleteReservationByIdCommand request, CancellationToken cancellationToken)
		{
			int result = 0;
			
			

			var reservation = _context.Set<AircraftReservation>().Find(request.ReservationId);
			if (reservation != null)
			{
				_context.Set<AircraftReservation>().Remove(reservation);
				result = await _context.SaveChangesAsync(cancellationToken);
			}
			return result;
		}
	}

	public class DeleteReservationByIdCommandValidator : AbstractValidator<DeleteReservationByIdCommand>
	{
		public DeleteReservationByIdCommandValidator()
		{
			RuleFor(x => x.ReservationId).GreaterThan(0).WithMessage("ReservationId Must Be Greter Then 0");
		}
	}
}
