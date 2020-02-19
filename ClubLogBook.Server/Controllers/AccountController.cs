using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using ClubLogBook.Application.Accounts.Queries.GetAccountsList;
using ClubLogBook.Application.Interfaces;
using ClubLogBook.Application.Accounts.Commands.CreateAccount;
using ClubLogBook.Application.Accounts.Commands.UpdateAccount;
using ClubLogBook.Application.Accounts.Queries.GetAccount;

namespace ClubLogBook.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IAppLogger<AccountController> logger;
        public AccountController(IMediator mediator, IAppLogger<AccountController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;

        }
        // GET: api/Account
        [HttpGet]
        public async Task<AccountListViewModel> GetAccountsList()
        {
            CancellationToken ct = new CancellationToken();
            AccountListViewModel list = new AccountListViewModel();
            list.Accounts = new List<AccountLookupModel>();
            list.Accounts.Add(new AccountLookupModel() { Id = 1 });
            try
            {
                CreateAccountCommand createAccountCommand = new CreateAccountCommand();
                createAccountCommand.MemeberId = 1;
                createAccountCommand.MemberInfo = "Azrie";
                //int id = await mediator.Send(createAccountCommand, ct);
                list  = await mediator.Send(new GetAccountsListQuery(), ct);
            }
            catch(Exception ex)
            {
                logger.LogWorning(ex.Message);
            }
            return list;
        }
        [HttpGet("{id}")]
        public async Task<AccountLookupModel> GetAccountByMemberId(int  id)
        {
            GetAccountByMemberIdQuery getAccountByMemberId = new GetAccountByMemberIdQuery(id);
            var result = await mediator.Send(getAccountByMemberId);
            return result;
        }
       

        // POST: api/Account
       
        public async Task<IActionResult> PostCreateAccount(int memeberId, string memberInfo, string description)
        {
            CancellationToken ct = new CancellationToken();
            CreateAccountCommand createAccountCommand = new CreateAccountCommand();
            createAccountCommand.MemeberId = memeberId;
            createAccountCommand.MemberInfo = memberInfo;
            createAccountCommand.Description = description;
            var result =  await mediator.Send(createAccountCommand, ct);
            return Ok(result);
        }
        public async Task<IActionResult> PostUpdateAccountInfo(int memeberId, string memberInfo , string description)
        {
            CancellationToken ct = new CancellationToken();
            UpdateAccountCommand updateAccountCommand = new UpdateAccountCommand();
            updateAccountCommand.MemeberId = memeberId;
            updateAccountCommand.MemberInfo = memberInfo;
            updateAccountCommand.Description = description;
            var result = await mediator.Send(updateAccountCommand, ct);
            return Ok(result);
        }
        public async Task<IActionResult> PostUpdateAccountBalance(int memeberId, Decimal flightBalance, Decimal cashBalance )
        {
            CancellationToken ct = new CancellationToken();
            UpdateAccountBalanceCommand updateAccountBalanceCommand = new UpdateAccountBalanceCommand
            {
                MemeberId = memeberId,
                FlightBalance = flightBalance,
                CashBalance = cashBalance
            };
            //createAccountCommand.MemberInfo = memberInfo;
            var account = await mediator.Send(updateAccountBalanceCommand, ct);
            return Ok(account);
        }
        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
