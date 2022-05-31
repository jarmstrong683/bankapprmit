using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using A3_MiBank_App.Data;
using A3_MiBank_App.Models;
using A3_MiBank_App.ViewModels;
using A3_MiBank_App.Attributes;
using Newtonsoft.Json;
using X.PagedList;

namespace A3_MiBank_App.Controllers
{
    public class AccountsController : Controller
    {
        private readonly MiBankContext _context;
        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        public const string AccountSessionKey = "_Account";


        public AccountsController(MiBankContext context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            var miBankContext = _context.Accounts.Include(a => a.Customer);
            return View(await miBankContext.ToListAsync());
        }



        // TO STATEMENT  - first serialise Json string and create a session variable to pass along pages
        public async Task<IActionResult> CustomerAccountsToStatement(int accountNumber)
        {
            var account = await _context.Accounts.FindAsync(accountNumber);
            if (account == null)
                return NotFound();

            //store complex OBJECT in the session
            var accountJson = JsonConvert.SerializeObject(account);
            HttpContext.Session.SetString(AccountSessionKey, accountJson);


            return RedirectToAction(nameof(Statement));
        }


        // ACCOUNT STATEMENT
        [AuthoriseCustomer]             // this authorisation attribute prevents unauthorised access (see Attributes folder)
        public async Task<IActionResult> Statement(int? page = 1)
        {

            // get the serialised Json account object from session
            var accountJson = HttpContext.Session.GetString(AccountSessionKey);
            if (accountJson == null)
                return BadRequest();
            //return RedirectToAction("CustomerAccounts", "Customers");

            // deserialise the json object then place in viewbag
            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            ViewBag.Account = account;

            //Page the account transactions, maximum 4 per page
            const int pageSize = 4;
            //var pagedList = await _context.Transactions.Where


            //LINQ query, method syntax, retrieves transactions matching the accountNumber from the context
            // then orders descending by date. (so most recent first)
            var pagedTransactionsList =  await _context.Transactions
                .Where(t => t.AccountNumber == account.AccountNumber)
                .OrderByDescending(t => t.ModifyDate).ToPagedListAsync(page,pageSize);

            
            //supply the view with a loaded up StatementViewModel object
            return View(new StatementViewModel
            {
                AccountNumber = account.AccountNumber,
                Account = account,
                PagedTransactionsList = pagedTransactionsList,

            });
        }






        // TO BILLPAY DISPLAY - but First serialise Json string and create a session variable to pass along pages
        public async Task<IActionResult> CustomerAccountsToBillPayDisplay(int accountNumber)
        {
            var account = await _context.Accounts.FindAsync(accountNumber);
            if (account == null)
            { return NotFound(); }

            //store complex OBJECT in the session
            var accountJson = JsonConvert.SerializeObject(account);
            HttpContext.Session.SetString(AccountSessionKey, accountJson);

            return RedirectToAction(nameof(BillPayDisplay));
        }



        //DISPLAY SCHEDULED BILLPAY 
        //[AuthoriseCustomer]             // this authorisation attribute prevents unauthorised access (see Attributes folder)
        public async Task<IActionResult> BillPayDisplay(int? page = 1)
        {

            // get the serialised Json account object from session
            var accountJson = HttpContext.Session.GetString(AccountSessionKey);
            if (accountJson == null)
                return BadRequest();
            //return RedirectToAction("CustomerAccounts", "Customers");

            // deserialise the json object then place in viewbag
            var account = JsonConvert.DeserializeObject<Account>(accountJson);
            ViewBag.Account = account;

            //Page the account transactions, maximum 4 per page
            const int pageSize = 4;

            //LINQ query, method syntax, retrieves transactions matching the accountNumber from the context
            // then orders descending by date. (so most recent first)

            var pagedBillPayList = await _context.BillPay
                .Where(b => b.AccountNumber == account.AccountNumber)// && b.ScheduleDate >= DateTime.UtcNow)
                .OrderBy(b => b.ScheduleDate).ToPagedListAsync(page, pageSize);

         


            //var payee = await _context.Payees
            //    .Include("BillPays")
            //    .Where(p => p.PayeeID == p.BillPa)

            //supply the view with a loaded up StatementViewModel object
            return View(new BillPayDisplayViewModel
            {
                AccountNumber = account.AccountNumber,
                Account = account,
                PagedBillPayList = pagedBillPayList
                
            });
        }




        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }






        // GET: Accounts/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AccountNumber,AccountType,CustomerID,Balance,ModifyDate,DestinationAccountNumber")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AccountNumber,AccountType,CustomerID,Balance,ModifyDate,DestinationAccountNumber")] Account account)
        {
            if (id != account.AccountNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.AccountNumber))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", account.CustomerID);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .Include(a => a.Customer)
                .FirstOrDefaultAsync(m => m.AccountNumber == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountNumber == id);
        }
    }
}
