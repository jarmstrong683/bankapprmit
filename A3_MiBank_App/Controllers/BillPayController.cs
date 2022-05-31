using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using A3_MiBank_App.Data;
using A3_MiBank_App.Models;
using System.Globalization;
using A3_MiBank_App.ViewModels;
using A3_MiBank_App.Utilities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace A3_MiBank_App.Controllers
{
    public class BillPayController : Controller
    {
        private readonly MiBankContext _context;
        BillPay _billPay = new BillPay();
        public decimal[] ServiceCharge = { 0.1M, 0.2M };
        public CultureInfo _culture = new CultureInfo("en-au");

        public BillPayController(MiBankContext context)
        {
            _context = context;
          
        }



        //BILLPAY - GET --> accountNo into ViewModel
        [HttpGet]
        public async Task<IActionResult> BillPay()
        {

            var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            var AccountsList = await _context.Accounts.Where(a => a.CustomerID == customerID).ToListAsync();

            return View(
                new BillPayViewModel
                {
                    AccountNumbersList = AccountsList.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.AccountNumber.ToString(),
                                      Text = a.AccountNumber.ToString() + " " + a.AccountType.ToString() + " : Balance " + a.Balance.ToString("C2", _culture),
                                  }).ToList(),
                    ScheduleDate = DateTime.UtcNow.ToLocalTime(),
                    PayeeNameOptions = _context.Payees.Select(p =>
                                  new SelectListItem
                                  {
                                      Value = p.PayeeName.ToString(),
                                      Text = p.PayeeName
                                  }).ToList(),
                    ModifyDate = DateTime.UtcNow.ToLocalTime()
                });
        }



        //BILLPAY - POST --> BillPayViewModel into View
        [HttpPost]
        public async Task<IActionResult> BillPay(BillPayViewModel viewModel)
        {
            // get Account
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);
            // assign current balance
            viewModel.Balance = viewModel.Account.Balance;
            viewModel.AccountType = viewModel.Account.AccountType;

            // assign the payee ID of the PayeeName chosen by the user
            Payee payee = _context.Payees.FirstOrDefault(p => p.PayeeName == viewModel.PayeeName);
            viewModel.PayeeID = payee.PayeeID;


            if (viewModel.Amount <= 0 || viewModel.Amount > 99999999999999)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be more than 0, and less than 99,999,999,999,999.");
                RefreshBillPayOnError(viewModel);
                return View(viewModel);
            }
            if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
                RefreshBillPayOnError(viewModel);
                return View(viewModel);
            }
            if (!_billPay.HasSufficientFunds(viewModel.AccountType, viewModel.Balance, viewModel.Amount, ServiceCharge[1]))
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Insufficient Funds. Minimum Balance Required for: Checking $200, Savings $0.");
                RefreshBillPayOnError(viewModel);
                return View(viewModel);
            }

            _billPay.ScheduleBillPay(viewModel, ServiceCharge[1], _context);

            await _context.SaveChangesAsync();

            return RedirectToAction("CustomerAccounts", "Customers");

        }

        // ADD ANOTHER YEAR TO ANNUAL PAYMENT
        internal void AddAnotherYearBillPay(BillPay billPay)
        {
            _billPay.AddAnotherYearBillPay(billPay);
        }

        // ADD ANOTHER QUARTER TO QUARTERLY PAYMENT
        internal void AddAnotherQuarterBillPay(BillPay billPay)
        {
            _billPay.AddAnotherQuarterBillPay(billPay);
        }

        // ADD ANOTHER MINUTE TO MUNUTELY PAYMENT
        internal void AddAnotherMinuteBillPay(BillPay billPay)
        {
            _billPay.AddAnotherMinuteBillPay(billPay);
        }


        //BILLPAY - GET --> MODIFY BillPayID into ViewModel
        [HttpGet]
        public async Task<IActionResult> ModifyBillPay(int billPayID)
        {
            var billPayList = await _context.BillPay.Where(b => b.BillPayID == billPayID).ToListAsync();
            BillPay billPay = billPayList.Find(b => b.BillPayID == billPayID);
            int accountNumber = billPay.AccountNumber;

            var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            var AccountsList = await _context.Accounts.Where(a => a.CustomerID == customerID).ToListAsync();

            Account account = _context.Accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            AccountType accountType = account.AccountType;

            Payee payee = _context.Payees.FirstOrDefault(p => p.PayeeID == billPay.PayeeID);


            return View(new BillPayViewModel
            {
                BillPayID = billPayID,
                AccountNumber = accountNumber,
                AccountNumbersList = AccountsList.Select(a =>
                              new SelectListItem
                              {
                                  Value = a.AccountNumber.ToString(),
                                  Text = a.AccountNumber.ToString() + " " + a.AccountType.ToString() + " : Balance " + a.Balance.ToString("C2", _culture)
                              }).ToList(),
                PayeeNameOptions = _context.Payees.Select(p =>
                              new SelectListItem
                              {
                                  Value = p.PayeeName.ToString(),
                                  Text = p.PayeeName
                              }).ToList(),
                ModifyDate = DateTime.Now.ToLocalTime(),
                Account = account,
                AccountType = accountType,
                Amount = billPay.Amount,
                Balance = account.Balance,
                PayeeID = payee.PayeeID,
                PayeeName = payee.PayeeName,
                Payee = payee,
                ScheduleDate = billPay.ScheduleDate.ToLocalTime(),
                Period = billPay.PaymentPeriod,
                Paid = false
            });
        }


        // POST --> MODIFY BILLPAY
        [HttpPost]
        public async Task<IActionResult> ModifyBillPay([Bind] BillPayViewModel viewModel)
        {

            // get Account
            viewModel.Account = await _context.Accounts.FindAsync(viewModel.AccountNumber);
            // assign current balance
            viewModel.Balance = viewModel.Account.Balance;

            //get BillPay
            var billPay = await _context.BillPay.FirstOrDefaultAsync(b => b.BillPayID == viewModel.BillPayID);


            // assign the payee ID of the PayeeName chosen by the user
            Payee payee = _context.Payees.FirstOrDefault(p => p.PayeeName == viewModel.PayeeName);
            viewModel.PayeeID = payee.PayeeID;


            if (viewModel.Amount <= 0 || viewModel.Amount > 99999999999999)
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount must be more than 0, and less than 99,999,999,999,999.");
                RefreshBillPayOnError(viewModel);
                return View(viewModel);
            }
            if (viewModel.Amount.HasMoreThanTwoDecimalPlaces())
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Amount cannot have more than 2 decimal places.");
                RefreshBillPayOnError(viewModel);
                return View(viewModel);
            }
            if (!_billPay.HasSufficientFunds(viewModel.AccountType, viewModel.Balance, viewModel.Amount, ServiceCharge[1]))
            {
                ModelState.AddModelError(nameof(viewModel.Amount), "Insufficient Funds. Minimum Balance Required for: Checking $200, Savings $0.");
                RefreshBillPayOnError(viewModel);
                return View(viewModel);
            }

            billPay.BillPayID = viewModel.BillPayID;
            billPay.Amount = viewModel.Amount;
            billPay.AccountNumber = viewModel.AccountNumber;
            billPay.ModifyDate = DateTime.UtcNow;
            billPay.PayeeID = viewModel.PayeeID;
            billPay.PaymentPeriod = viewModel.Period;
            billPay.ScheduleDate = viewModel.ScheduleDate;
            billPay.Account = viewModel.Account;
            billPay.Payee = viewModel.Payee;
            billPay.Paid = false;


            Console.WriteLine(billPay.BillPayID);
            Console.WriteLine(billPay.Amount);
            Console.WriteLine(billPay.AccountNumber);
            Console.WriteLine(billPay.ModifyDate);
            Console.WriteLine(billPay.PayeeID);
            Console.WriteLine(billPay.PaymentPeriod.ToString());
            Console.WriteLine(billPay.ScheduleDate);
            Console.WriteLine(billPay.Account);
            Console.WriteLine(billPay.Payee);

            //  UPDATE DATABASE WITH MODIFIED RECORD

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billPay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillPayExists(billPay.BillPayID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CustomerAccounts", "Customers");
            }
            return RedirectToAction("CustomerAccounts", "Customers");
        }



        public BillPayViewModel RefreshBillPayOnError(BillPayViewModel viewModel)
        {
            var customerID = HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            var AccountsList = _context.Accounts.Where(a => a.CustomerID == customerID).ToList();

            viewModel.AccountNumbersList = AccountsList.Select(a =>
                                 new SelectListItem
                                 {
                                     Value = a.AccountNumber.ToString(),
                                     Text = a.AccountNumber.ToString() + " " + a.AccountType.ToString() + " : Balance " + a.Balance.ToString("C2", _culture)
                                 }).ToList();

            viewModel.PayeeNameOptions = _context.Payees.Select(p =>
                                          new SelectListItem
                                          {
                                              Value = p.PayeeName.ToString(),
                                              Text = p.PayeeName
                                          }).ToList();
            return viewModel;
        }



        public async Task PayBillImmediately(int billPayID)
        {
            BillPay billPay = await _context.BillPay.FindAsync(billPayID);

            await _billPay.PayBillPay(billPay, _context);

        }

     

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionID == id);
        }


        private bool BillPayExists(int id)
        {
            return _context.BillPay.Any(e => e.BillPayID == id);
        }






















        // GET: BillPay
        public async Task<IActionResult> Index()
        {
            return View(await _context.BillPay.ToListAsync());
        }

        // GET: BillPay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPay
                .FirstOrDefaultAsync(m => m.BillPayID == id);
            if (billPay == null)
            {
                return NotFound();
            }

            return View(billPay);
        }

        // GET: BillPay/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillPay/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BillPayID,AccountNumber,PayeeID,Amount,ScheduleDate,PaymentPeriod,ModifyDate")] BillPay billPay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billPay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(billPay);
        }

        // GET: BillPay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billPay = await _context.BillPay.FindAsync(id);
            if (billPay == null)
            {
                return NotFound();
            }
            return View(billPay);
        }

       

    }
}
