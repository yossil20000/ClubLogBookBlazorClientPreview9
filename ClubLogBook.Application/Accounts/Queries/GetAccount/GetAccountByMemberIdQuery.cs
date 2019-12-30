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

namespace ClubLogBook.Application.Accounts.Queries.GetAccount
{
	public class GetAccountByMemberIdQuery : IRequest<AccountLookupModel>
	{
		public int MemberId { get; set; }
		public GetAccountByMemberIdQuery(int memberId)
		{
			MemberId = memberId;
		}
		public class GetAcoountByMemberIdHandler : IRequestHandler<GetAccountByMemberIdQuery, AccountLookupModel>
		{
			private readonly IApplicationDbContext _applicationDbContext;
			private readonly IMapper _mapper;
			public GetAcoountByMemberIdHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
			{
				_applicationDbContext = applicationDbContext;
				_mapper = mapper;
			}
			public Task<AccountLookupModel> Handle(GetAccountByMemberIdQuery request, CancellationToken cancellationToken)
			{
				AccountLookupModel entity = _applicationDbContext.Set<Account>().ProjectTo<AccountLookupModel>(_mapper.ConfigurationProvider).Where(val => val.MemeberId == request.MemberId).SingleOrDefault();
				return Task.FromResult<AccountLookupModel>(entity);


			}
		}
	}
	public class GetAccountByMemberIdValidator : AbstractValidator<GetAccountByMemberIdQuery>
	{
		public GetAccountByMemberIdValidator()
		{
			RuleFor(val => val.MemberId).GreaterThan(0).WithMessage("MemberId Must Be Grater Then 0");
		}
	}
}
