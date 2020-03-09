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
using ClubLogBook.Application.Accounts.Queries.GetAccountInvoice;
using ClubLogBook.Application.Accounts.Commands.DeleteAccount;
using ClubLogBook.Application.Flights.Queries.GetFlightWithoutInvoice;

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
        public async Task<AccountListModel> GetAccountsList()
        {
            CancellationToken ct = new CancellationToken();
            AccountListModel list = new AccountListModel();
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
        [HttpPost]
        public async Task<IActionResult> PostCreateAccounts()
        {
            CancellationToken ct = new CancellationToken();
            CreateAccountsCommand createAccountsCommand = new CreateAccountsCommand();
            var result = await mediator.Send(createAccountsCommand, ct);
            return Ok(result);
        }

        // POST: api/Account
        [HttpPost]
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
            UpdateAccountInfoCommand updateAccountCommand = new UpdateAccountInfoCommand();
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
        public async Task<IActionResult> PostCreateInvoice()
        {
            CancellationToken ct = new CancellationToken();
            CreateAccountInvoiceCommand createAccountInvoiceCommand = new CreateAccountInvoiceCommand();
            createAccountInvoiceCommand.accountInvoiceModel.Id = 0;
            createAccountInvoiceCommand.accountInvoiceModel.InvoiceState = Core.Interfaces.InvoiceState.Create;
            createAccountInvoiceCommand.accountInvoiceModel.InvoiceType = Core.Interfaces.InvoiceType.Flight;
            createAccountInvoiceCommand.accountInvoiceModel.MemeberId = 8;
            createAccountInvoiceCommand.accountInvoiceModel.Amount = 1.3m;
            var result =  await mediator.Send(createAccountInvoiceCommand, ct);
            if (result > 0)
                return Ok(result);
            else
                return BadRequest(result);

        }
        public async Task<IActionResult> GetInvoiceList()
        {
            CancellationToken ct = new CancellationToken();
            GetAccountInvoiceQuery getAccountInvoiceQuery = new GetAccountInvoiceQuery();
            var result = await mediator.Send(getAccountInvoiceQuery, ct);
           
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);

        }
        public async Task<IActionResult> GetFlightsWithoutInvoiceList()
        {
            CancellationToken ct = new CancellationToken();
            GetFlightsWithoutInvoiceQuery getFlightsWithoutInvoiceQuery = new GetFlightsWithoutInvoiceQuery();
            var result = await mediator.Send(getFlightsWithoutInvoiceQuery, ct);
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);

        }

        public async Task<IActionResult> DeleteInvoice(int id)
        {
            CancellationToken ct = new CancellationToken();
            DeleteAccountInvoiceCommand deleteAccountInvoiceCommand = new DeleteAccountInvoiceCommand() { Id = id };
            var result = await mediator.Send(deleteAccountInvoiceCommand, ct);
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);

        }
        public async Task<IActionResult> UpdateTransaction(int accountId)
        {
            CancellationToken ct = new CancellationToken();
            UpdateAccountTransactionCommand updateAccountTransactionCommand = new UpdateAccountTransactionCommand();
            updateAccountTransactionCommand.AccountTransactionModel.AccountId = accountId;
            updateAccountTransactionCommand.AccountTransactionModel.Invoice.Amount = 1.4m;
            updateAccountTransactionCommand.AccountTransactionModel.Invoice.InvoiceState = Core.Interfaces.InvoiceState.InTransaction;
            updateAccountTransactionCommand.AccountTransactionModel.Invoice.InvoiceType = Core.Interfaces.InvoiceType.ClubFee;
            updateAccountTransactionCommand.AccountTransactionModel.TransactionType = Core.Interfaces.TransactionType.Credit;
            updateAccountTransactionCommand.AccountTransactionModel.Description = "For club FeeFebrueary";
            updateAccountTransactionCommand.AccountTransactionModel.ProcessInvoice();
            var result = await mediator.Send(updateAccountTransactionCommand, ct);

            var invoice = GetInvoiceList();
            if (result != null)
                return Ok(result);
            else
                return BadRequest(result);

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
