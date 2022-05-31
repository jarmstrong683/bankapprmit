//Code for this file sourced from Week 7 tutorial (Matt Bolger)
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using A3_MiBank_App.Data;
using A3_MiBank_App.Models;
using SimpleHashing;
using A3_MiBank_App.ViewModels;

namespace A3_MiBank_App.Controllers
{
    [Route("/A3_MiBank_App/SecureLogin")]
    public class LoginController : Controller
    {
        private readonly MiBankContext _context;

        public LoginController(MiBankContext context) => _context = context;


        [HttpGet]
        public IActionResult Login()
        {

            return View(new LoginViewModel
            {
                FailedAttempts = 0,

            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            int failCount;
            try
            {
                var login = await _context.Logins.FindAsync(viewModel.UserID);


                if (login == null || !PBKDF2.Verify(login.PasswordHash, viewModel.Password))
                {
                    viewModel.FailedAttempts += 1;
                    failCount = viewModel.FailedAttempts;

                    ModelState.AddModelError("LoginFailed", failCount + " failed Login Attempts (Total of 3 Allowed).");

                    if (failCount == 3)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    return View(new LoginViewModel { UserID = viewModel.UserID , FailedAttempts = failCount });
                }
                // Login customer.
                HttpContext.Session.SetInt32(nameof(Customer.CustomerID), login.CustomerID);
                HttpContext.Session.SetString(nameof(Customer.CustomerName), login.Customer.CustomerName);
                return RedirectToAction("CustomerAccounts", "Customers");
            }
            catch(Exception)
            {
                ModelState.AddModelError("LoginFailed", "Login failed, please check internet connection or that the database has been updated, then try again.");
                return View();
            }
        }

        [Route("LogoutNow")]
        public IActionResult Logout()
        {
            // Logout customer.
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


    }
}
