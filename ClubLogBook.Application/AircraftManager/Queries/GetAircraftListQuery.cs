using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ClubLogBook.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.ViewModels;
using System.Collections.Generic;

namespace ClubLogBook.Application.AircraftManager.Queries
{
	
	public class GetAircraftListQuery : IRequest<AircraftListViewModel>
	{
		public class Handler : IRequestHandler<GetAircraftListQuery, AircraftListViewModel>
		{
			private readonly IClubContext context;
			private readonly IMapper mapper;
			public Handler(IClubContext context, IMapper mapper)
			{
				this.mapper = mapper;
				this.context = context;
			}
			public async Task<AircraftListViewModel> Handle(GetAircraftListQuery request, CancellationToken cancellationToken)
			{
				var ar = await context.Set<Aircraft>().ToListAsync();
				IList<AircraftViewModel> aircraftViewModel = new List<AircraftViewModel>();
				mapper.Map(ar, aircraftViewModel);
				return new AircraftListViewModel
				{
					//AircraftList = await context.Set<Aircraft>().ProjectTo<AircraftViewModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken)
					AircraftList = aircraftViewModel
				};
			}
		}
	}
}
