using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using A3_MiBank_App.Data;
using A3_MiBank_App.Models;
using A3_MiBank_App.ViewModels;
using A3_MiBank_App.Attributes;
using Microsoft.AspNetCore.Http;
using SimpleHashing;


namespace A3_MiBank_App.Controllers
{
    public class CustomersController : Controller
    {
        
        private readonly MiBankContext _context;

        // This code from Week7 tute (Matt)
        // Once the Customer has Succesfully logged on the Login page
        // the CustomerID is passed set using HttpContext.Session.SetInt32 at the Login page
        // and can be retrieved by using  HttpContext.Session.GetInt32 here:

        private int CustomerID => HttpContext.Session.GetInt32(nameof(Customer.CustomerID)).Value;
        


        public CustomersController(MiBankContext context)
        {
            _context = context;
        }


        // GET: Customers
        [AuthoriseCustomer]
        public async Task<IActionResult> CustomerAccounts()
        {
            // Lazy loading.
            // The Customer.Accounts property will be lazy loaded upon demand.
            var customer = await _context.Customers.FindAsync(CustomerID);
         

            // OR
            // Eager loading.
            //var customer = await _context.Customers.Include(x => x.Accounts).
            //    FirstOrDefaultAsync(x => x.CustomerID == _customerID);

            return View(customer);
        }


        public IActionResult Index()
        {

            return View();
        }
    

    // GET: Customers/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }



        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,CustomerName,TFN,Address,City,State,PostCode,Phone")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            //var login = await _context.Logins.FirstOrDefaultAsync(x => x.CustomerID == id);

            EditCustomerViewModel viewModel = new EditCustomerViewModel

            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                TFN = customer.TFN,
                Address = customer.Address,
                City = customer.City,
                State = customer.State,
                PostCode = customer.PostCode,
                Phone = customer.Phone,
                Customer = customer,
            };

            if (customer == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerID,CustomerName,TFN,Address,City,State,PostCode,Phone")] Customer customer, EditCustomerViewModel viewModel)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }


            //CHANGE LOGIN PASSWORD BOX
            var login = await _context.Logins.FirstOrDefaultAsync(x => x.CustomerID == viewModel.CustomerID);

         
            if (viewModel.CurrentPassword != null)

            {
                // Validations of password change box
                if (viewModel.CurrentPassword != "" && !PBKDF2.Verify(login.PasswordHash, viewModel.CurrentPassword))
                {
                    ModelState.AddModelError(nameof(viewModel.CurrentPassword), "The Current Password entered is incorrect, please try again.");
                    
                }

                if (viewModel.CurrentPassword != "" && PBKDF2.Verify(login.PasswordHash, viewModel.CurrentPassword))
                {
                    if (viewModel.NewPassword == null || viewModel.NewPassword == "")
                    {
                        ModelState.AddModelError(nameof(viewModel.NewPassword), "Enter a new password.");
                    }
                }

                if (viewModel.CurrentPassword != "" && PBKDF2.Verify(login.PasswordHash, viewModel.CurrentPassword))
                {
                    if (viewModel.ConfirmNewPassword == null || viewModel.ConfirmNewPassword == "" || viewModel.ConfirmNewPassword != viewModel.NewPassword)
                    {
                        ModelState.AddModelError(nameof(viewModel.ConfirmNewPassword), "Confirm new password.");
                        viewModel.ConfirmNewPassword = null;
                    }
                }

                if (viewModel.NewPassword != null && viewModel.NewPassword != "")
                {
                    string loginHash = PBKDF2.Hash(viewModel.NewPassword);
                    login.PasswordHash = loginHash;
                }
            }

            // UPDATE DATABASE CONTEXT THEN SAVE CHANGES TO DATABASE
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    _context.Update(login);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CustomerAccounts));
            }

            return View(new EditCustomerViewModel
            {
                CustomerName = viewModel.CustomerName,
                TFN = viewModel.TFN,
                Address = viewModel.Address,
                City = viewModel.City,
                State = viewModel.State,
                PostCode = viewModel.PostCode,
                Phone = viewModel.Phone
            });
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
