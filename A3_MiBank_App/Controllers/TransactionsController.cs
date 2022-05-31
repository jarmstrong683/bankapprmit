using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A3_MiBank_App.Data;
using A3_MiBank_App.Models;
using A3_MiBank_App.ViewModels;
using A3_MiBank_App.Utilities;
using Microsoft.AspNetCore.Http;

namespace A3_MiBank_App.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly MiBankContext _context;
        Transaction _transaction = new Transaction();
        public decimal[] ServiceCharge = { 0.1M, 0.2M };
        private Customer _customer = new Customer();
        CultureInfo _culture = new CultureInfo("en-AU");


        public TransactionsController(MiBankContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var miBankContext = _context.Transactions.Include(t => t.Account).Include(t => t.DestinationAccount);
            return View(await miBankContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.DestinationAccount)
                .FirstOrDefaultAsync(m => m.TransactionID == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // ATM GET --> 
        [HttpGet]
        public async Task<IActionResult> ATM()
        {
            var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            var AccountsList = await _context.Accounts.Where(a => a.CustomerID == customerID).ToListAsync();


            return View(new ATMViewModel
            {
                AccountNumbersList = AccountsList.Select(a =>
                              new SelectListItem
                              {
                                  Value = a.AccountNumber.ToString(),
                                  Text = a.AccountNumber.ToString() + " " + a.AccountType.ToString() + " : Balance " + a.Balance.ToString("C2",_culture)
                              }).ToList()
            });
        }


        //ATM - POST --> transferViewModel into View
        [HttpPost]
        public async Task<IActionResult> ATM(ATMViewModel viewModel)
        {

            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);
            viewModel.Balance = viewModel.Account.Balance;
            decimal serviceCharge = _transaction.DetermineServiceCharge(viewModel.TransactionType);
            bool applyServiceCharge = _transaction.MoreThan4FreeTransaction(_context);
            if (!applyServiceCharge) { serviceCharge = 0.0M; }

            Account DestinationAccount = await _context.Accounts.FindAsync(viewModel.DestinationAccountNumber);
            viewModel.DestinationAccount = DestinationAccount;


            if (viewModel.Amount <= 0 || viewModel.Amount > 99999999999999)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be more than 0, and less than 99,999,999,999,999.");
                viewModel = RefreshATMonError(viewModel);
                return View(viewModel);
            }
            if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
                viewModel = RefreshATMonError(viewModel);
                return View(viewModel);
            }

            if (viewModel.TransactionType == TransactionType.Transfer && viewModel.DestinationAccountNumber == viewModel.AccountNumber)
            {
                ModelState.AddModelError(nameof(viewModel.DestinationAccountNumber), "Choose Destination Account.");
                viewModel = RefreshATMonError(viewModel);
                return View(viewModel);
            }


            //check if sufficient funds
            if (viewModel.TransactionType == TransactionType.Withdraw || viewModel.TransactionType == TransactionType.Transfer)
            {
                if (!_transaction.HasSufficientFunds(viewModel.Account.AccountType, viewModel.Account.Balance, viewModel.Amount, serviceCharge))
                {
                    ModelState.AddModelError(nameof(viewModel.Amount), "Insufficient Funds (including Service Charges). Minimum Balance Required for: Checking $200, Savings $0.");
                    viewModel = RefreshATMonError(viewModel);
                    return View(viewModel);
                }
            }

            _transaction.CreateATMTransaction(viewModel, _context);

            Console.WriteLine("DEBUGGING ");
            await _context.SaveChangesAsync();

            return RedirectToAction("CustomerAccounts", "Customers");

        }



        public ATMViewModel RefreshATMonError(ATMViewModel viewModel)
        {
            var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            var AccountsList = _context.Accounts.Where(a => a.CustomerID == customerID).ToList();

            viewModel.AccountNumbersList = AccountsList.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.AccountNumber.ToString(),
                                     Text = a.AccountNumber.ToString() + " " + a.AccountType.ToString() + " : Balance " + a.Balance.ToString("C2", _culture)
                                 }).ToList();
            return viewModel;
        }



        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }
    

        private bool BillPayExists(int id)
        {
            return _context.BillPay.Any(e => e.BillPayID == id);
        }

}
}