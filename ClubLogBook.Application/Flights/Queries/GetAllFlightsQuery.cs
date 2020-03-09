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

namespace ClubLogBook.Application.Flights.Queries
{
	public class ClubFlightListViewModel
	{
		public List<ClubFlightModel> clubFlightListViewModel { get; set; }
	}
	public class InvoiceToFlightComaprer : IEqualityComparer<Invoice>
	{
		public bool Equals(Invoice x, Invoice y)
		{
			return x.InvoiceReferance == y.InvoiceReferance && x.InvoiceType == InvoiceType.Flight;

		}

		public int GetHashCode(Invoice obj)
		{
			return obj.GetHashCode();
		}
	}
	public class GetAllFlightsQuery : IRequest<FlightRecordIndexModel>
	{

		public class GetAllFlightsQueryHandler : IRequestHandler<GetAllFlightsQuery, FlightRecordIndexModel>
		{
			private readonly IApplicationDbContext _context;
			private readonly IMapper _mapper;
			public GetAllFlightsQueryHandler(IApplicationDbContext context, IMapper mapper) => (_mapper, _context) = (mapper, context);


			public async Task<FlightRecordIndexModel> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
			{
				InvoiceToFlightComaprer comparer = new InvoiceToFlightComaprer();
				FlightRecordIndexModel flightRecordIndexViewModel = new FlightRecordIndexModel();


				var flights = _context.Set<Flight>().ToList();
				
				ClubFlightModel clubFlightViewModel = new ClubFlightModel();
				flightRecordIndexViewModel.FlightRecords = _mapper.Map<List<Flight>, List<ClubFlightModel>>(flights);

				int totalFlight = flights.Count();
				flightRecordIndexViewModel.PaginationInfo.TotalItems = totalFlight;
				flightRecordIndexViewModel.PaginationInfo.ActualPage = 1;

				flightRecordIndexViewModel.PaginationInfo.TotalItems = totalFlight;
				flightRecordIndexViewModel.PaginationInfo.TotalPages = int.Parse(Math.Ceiling((decimal)totalFlight / flightRecordIndexViewModel.PaginationInfo.ItemsPerPage).ToString());
				flightRecordIndexViewModel.PaginationInfo.Next = flightRecordIndexViewModel.PaginationInfo.ActualPage >= flightRecordIndexViewModel.PaginationInfo.TotalPages ? "disabled" : "";
				flightRecordIndexViewModel.PaginationInfo.Previous = flightRecordIndexViewModel.PaginationInfo.ActualPage == 1 ? "disabled" : "";
				
				return flightRecordIndexViewModel;
			}
		}

		public class GetFlightsWithoutInvoiceQueryValidator : AbstractValidator<GetAllFlightsQuery>
		{

		}

	}
}
