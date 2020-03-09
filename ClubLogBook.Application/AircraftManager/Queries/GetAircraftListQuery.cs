using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.Models;
using System.Collections.Generic;
using ClubLogBook.Application.Common.Interfaces;

namespace ClubLogBook.Application.AircraftManager.Queries
{
	
	public class GetAircraftListQuery : IRequest<AircraftListModel>
	{
		public class Handler : IRequestHandler<GetAircraftListQuery, AircraftListModel>
		{
			private readonly IApplicationDbContext context;
			private readonly IMapper mapper;
			public Handler(IApplicationDbContext context, IMapper mapper)
			{
				this.mapper = mapper;
				this.context = context;
			}
			public async Task<AircraftListModel> Handle(GetAircraftListQuery request, CancellationToken cancellationToken)
			{
				var ar = await context.Set<Aircraft>().ToListAsync();
				List<AircraftModel> aircraftViewModel = new List<AircraftModel>();
				mapper.Map(ar, aircraftViewModel);
				return new AircraftListModel
				{
					//AircraftList = await context.Set<Aircraft>().ProjectTo<AircraftViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
					AircraftList = aircraftViewModel
				};
			}
		}
	}
}
