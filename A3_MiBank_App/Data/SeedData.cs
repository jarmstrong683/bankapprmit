using System;
using System.Linq;
using A3_MiBank_App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace A3_MiBank_App.Data
{
    public static class SeedData
    {


        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new MiBankContext(serviceProvider.GetRequiredService<DbContextOptions<MiBankContext>>());

            // Look for customers table 
            if (context.Customers.Any())
                return; // DB has already been seeded.


            // seed Customers table
            context.Customers.AddRange(
                new Customer
                {
                    CustomerID = 2100,
                    CustomerName = "Matthew Bolger",
                    TFN = "12345678910",
                    Address = "123 Fake Street",
                    City = "Melbourne",
                    State = States.VIC,
                    PostCode = 3000,
                    Phone = "(61)23456789"
                },
                new Customer
                {
                    CustomerID = 2200,
                    CustomerName = "Rodney Cocker",
                    TFN = "10987654321",
                    Address = "456 Real Road",
                    City = "Sydney",
                    State = States.NSW,
                    PostCode = 5005,
                    Phone = "(61)12345678"

                },

                new Customer
                {
                    CustomerID = 2300,
                    CustomerName = "Shekhar Kalra",
                    TFN = "10987654322",
                    Address = null,
                    City = null,
                    State = States.VIC,
                    PostCode = 3243,
                    Phone = "0432556432"

                });


            // seed Payees table
            context.Payees.AddRange(
                new Payee
                {
                    PayeeName = "Anytime Fitness",
                    Address = "123 Fake Street",
                    City = "Melbourne",
                    State = 4,
                    PostCode = "3000",
                    Phone = "(61)23456789"
                },
                new Payee
                {
                    PayeeName = "Bubble High Tea",
                    Address = "456 Real Road",
                    City = "Sydney",
                    State = 5,
                    PostCode = "5005",
                    Phone = "(61)12345678"

                },

                new Payee
                {
                    PayeeName = "JB Hifi",
                    Address = "12 Boundary Rd",
                    City = "Chattanooga",
                    State = 6,
                    PostCode = "3000",
                    Phone = "(61)23456789"
                },
                new Payee
                {
                    PayeeName = "Red Energy",
                    Address = "41 Western Ave",
                    City = "Hobart",
                    State = 8,
                    PostCode = "8005",
                    Phone = "(61)12345678"

                },

                new Payee
                {
                    PayeeName = "Optus",
                    Address = "41 Western Ave",
                    City = "Hobart",
                    State = 8,
                    PostCode = "8005",
                    Phone = "(61)12345678"

                });



            // NB: the DateTime.ParseExact method will transform the following inputted date
            // format to the correct UTC formatin the database
            const string format = "dd/MM/yyyy hh:mm:ss tt";

            //seed Logins Table
            context.Logins.AddRange(
                new Login
                {
                    UserID = "12345678",
                    CustomerID = 2100,
                    PasswordHash = "YBNbEL4Lk8yMEWxiKkGBeoILHTU7WZ9n8jJSy8TNx0DAzNEFVsIVNRktiQV+I8d2",
                    ModifyDate = DateTime.ParseExact("05/07/2020 08:00:00 PM", format, null)
                },
                new Login
                {
                    UserID = "38074569",
                    CustomerID = 2200,
                    PasswordHash = "EehwB3qMkWImf/fQPlhcka6pBMZBLlPWyiDW6NLkAh4ZFu2KNDQKONxElNsg7V04",
                    ModifyDate = DateTime.ParseExact("05/07/2020 08:00:00 PM", format, null)
                },

                new Login
                {
                    UserID = "17963428",
                    CustomerID = 2300,
                    PasswordHash = "LuiVJWbY4A3y1SilhMU5P00K54cGEvClx5Y+xWHq7VpyIUe5fe7m+WeI0iwid7GE",
                    ModifyDate = DateTime.ParseExact("05/07/2020 08:00:00 PM", format, null)

                });


            // Seed Accounts table
            context.Accounts.AddRange(
                new Account
                {
                    AccountNumber = 4100,
                    AccountType = AccountType.Savings,
                    CustomerID = 2100,
                    Balance = 100,
                    ModifyDate = DateTime.ParseExact("03/07/2020 08:00:00 PM", format, null),
                    DestinationAccountNumber = 4100
                },
                new Account
                {
                    AccountNumber = 4101,
                    AccountType = AccountType.Checking,
                    CustomerID = 2100,
                    Balance = 500,
                    ModifyDate = DateTime.ParseExact("03/07/2020 08:00:00 PM", format, null),
                    DestinationAccountNumber = 4101
                },
                new Account
                {
                    AccountNumber = 4200,
                    AccountType = AccountType.Savings,
                    CustomerID = 2200,
                    Balance = 500.95m,
                    ModifyDate = DateTime.ParseExact("03/07/2020 08:00:00 PM", format, null),
                    DestinationAccountNumber = 4200
                },

                new Account
                {
                    AccountNumber = 4300,
                    AccountType = AccountType.Checking,
                    CustomerID = 2300,
                    Balance = 1250.50m,
                    ModifyDate = DateTime.ParseExact("03/07/2020 08:00:00 PM", format, null),
                    DestinationAccountNumber = 4300



                });


            const string openingBalance = "Opening balance";

            context.Transactions.AddRange(
                new Transaction
                {
                    //TransactionID = 1000,
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4100,
                    DestinationAccountNumber = null,
                    Amount = 100,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("03/07/2020 08:00:00 PM", format, null)
                },

                 new Transaction
                 {
                     //TransactionID = 1000,
                     TransactionType = TransactionType.Withdraw,
                     AccountNumber = 4100,
                     DestinationAccountNumber = null,
                     Amount = -50,
                     Comment = "Withdraw",
                     ModifyDate = DateTime.ParseExact("04/07/2020 08:00:00 PM", format, null)
                 },

                  new Transaction
                  {
                      //TransactionID = 1000,
                      TransactionType = TransactionType.Deposit,
                      AccountNumber = 4100,
                      DestinationAccountNumber = null,
                      Amount = 100,
                      Comment = "Deposit",
                      ModifyDate = DateTime.ParseExact("05/07/2020 08:00:00 PM", format, null)
                  },

                   new Transaction
                   {
                       //TransactionID = 1000,
                       TransactionType = TransactionType.Withdraw,
                       AccountNumber = 4100,
                       DestinationAccountNumber = null,
                       Amount = -25,
                       Comment = "Withdraw",
                       ModifyDate = DateTime.ParseExact("06/07/2020 08:00:00 PM", format, null)
                   },

                    new Transaction
                    {
                        //TransactionID = 1000,
                        TransactionType = TransactionType.Withdraw,
                        AccountNumber = 4100,
                        DestinationAccountNumber = null,
                        Amount = -25,
                        Comment = "Withdraw",
                        ModifyDate = DateTime.ParseExact("07/07/2020 08:00:00 PM", format, null)
                    },


                new Transaction
                {
                    //TransactionID = 1001,
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4101,
                    DestinationAccountNumber = null,
                    Amount = 500,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("05/07/2020 08:30:00 PM", format, null)
                },

                  new Transaction
                  {
                      //TransactionID = 1001,
                      TransactionType = TransactionType.Deposit,
                      AccountNumber = 4101,
                      DestinationAccountNumber = null,
                      Amount = 500,
                      Comment = openingBalance,
                      ModifyDate = DateTime.ParseExact("05/07/2020 08:30:00 PM", format, null)
                  },
                new Transaction
                {
                    // TransactionID = 1002,
                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4200,
                    DestinationAccountNumber = null,
                    Amount = 500.95m,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("06/07/2020 09:00:00 PM", format, null)

                },

                new Transaction
                {

                    TransactionType = TransactionType.Deposit,
                    AccountNumber = 4300,
                    DestinationAccountNumber = null,
                    Amount = 1250.50m,
                    Comment = openingBalance,
                    ModifyDate = DateTime.ParseExact("08/06/2020 10:00:00 PM", format, null)

                });

            context.SaveChanges();

        }


    }
}