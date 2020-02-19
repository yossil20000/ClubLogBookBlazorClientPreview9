using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;

using ClubLogBook.Core.Entities;
using Microsoft.EntityFrameworkCore;
using ClubLogBook.Application.Common.Interfaces;

namespace ClubLogBook.Application.Accounts.Queries.GetAccountsList
{
	public class GetAccountsListQuery : IRequest<AccountListViewModel>
	{
		public class Handler : IRequestHandler<GetAccountsListQuery, AccountListViewModel>
		{
			private readonly IApplicationDbContext context;
			private readonly IMapper mapper;
			public Handler(IApplicationDbContext context,IMapper mapper)
			{
				this.mapper = mapper;
				this.context = context;
			}
			public async Task<AccountListViewModel> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
			{
				return new AccountListViewModel 
				{
					Accounts = await context.Set<Account>().ProjectTo<AccountLookupModel>(mapper.ConfigurationProvider).ToListAsync(cancellationToken) 
				};
			}
		}
	}
}
