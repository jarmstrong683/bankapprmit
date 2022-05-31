using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using A3_MiBank_App.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using A3_MiBank_App.Data;
using Microsoft.EntityFrameworkCore;


namespace A3_MiBank_App.Models
{
    public enum TransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3,
        ServiceCharge = 4,
        BillPay = 5
    }

    public enum ATMTransactionType
    {
        Deposit = 1,
        Withdraw = 2,
        Transfer = 3
    }

    

    public class Transaction
    {

        [Required]
        [Range(1000, 20000)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }

        [Required]
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType { get; set; }
        
        [Required]
        [ForeignKey("Account")]
        public int AccountNumber { get; set; }// THE 'ACCOUNTNUMBER' IS IMPLICITLY ASSOCIATED WITH THE
        public virtual Account Account { get; set; } // ACCOUNT ON THE NEXT LINE TO FORM A FOREIGN KEY

        [ForeignKey("DestinationAccount")]
        [Display(Name = "Destination Acct")]
        public int? DestinationAccountNumber { get; set; } // THE 'DESTINATIONACCOUNTNUMBER' IS EXPLICITLY ASSOCIATED WITH THE
        public virtual Account DestinationAccount { get; set; } //'DESTINATIONACCOUNT' ON THE NEXT LINE, BY THE FOREIGN KEY DATA ANNOTATION

        [Range(0, 99999999999999)]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [StringLength(255)]
        public string Comment { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime ModifyDate { get; set; }



        public void AddAnyTransaction(ATMViewModel viewModel, Account account)
        {
            if (viewModel.TransactionType == TransactionType.Withdraw || viewModel.TransactionType == TransactionType.Deposit || viewModel.TransactionType == TransactionType.ServiceCharge)
            {
                viewModel.DestinationAccountNumber = null;
            }

            account.Transactions.Add(
                   new Transaction
                   {
                       AccountNumber = viewModel.AccountNumber,
                       TransactionType = viewModel.TransactionType,
                       DestinationAccountNumber = viewModel.DestinationAccountNumber,
                       Amount = viewModel.Amount,
                       Comment = viewModel.TransactionType.ToString() + ": " + viewModel.Comment,
                       ModifyDate = DateTime.UtcNow.ToLocalTime()
                   });
        }


        public void CreateATMTransaction(ATMViewModel viewModel, MiBankContext context)
        {
            decimal serviceCharge = 0.0M;
            bool applyServiceCharge;

            switch (viewModel.TransactionType)
            {
                //WITHDRAW
                case TransactionType.Withdraw:

                    applyServiceCharge = MoreThan4FreeTransaction(context);
                    if (applyServiceCharge) { serviceCharge = DetermineServiceCharge(viewModel.TransactionType); }

                    viewModel.Account.Balance -= (viewModel.Amount + serviceCharge);
                    viewModel.Amount = -viewModel.Amount;
                    viewModel.DestinationAccountNumber = null;
                    

                    //ADD WITHDRAW TRANSACTION TO ACCOUNT
                    AddAnyTransaction(viewModel, viewModel.Account);

                    //APPLY ANY SERVICE CHARGE
                    if (applyServiceCharge)
                    {
                        viewModel.Account.Transactions.Add(
                          new Transaction
                          {
                              AccountNumber = viewModel.AccountNumber,
                              TransactionType = TransactionType.ServiceCharge,
                              Amount = -serviceCharge,
                              Comment = "Service Charge for " + viewModel.TransactionType.ToString(),
                              ModifyDate = DateTime.UtcNow.ToLocalTime()
                          }); ;
                    }
                    break;


                // TRANSFER
                case TransactionType.Transfer:

                    applyServiceCharge = MoreThan4FreeTransaction(context);
                    if (applyServiceCharge) { serviceCharge = DetermineServiceCharge(viewModel.TransactionType); }

                    viewModel.Account.Balance -= (viewModel.Amount + serviceCharge);

                    viewModel.DestinationAccount.Balance += viewModel.Amount;
                    viewModel.Amount = -viewModel.Amount;
                    applyServiceCharge = MoreThan4FreeTransaction(context);

                    //add TRANSFER transaction to Account
                    AddAnyTransaction(viewModel, viewModel.Account);

                    //add transaction to Destination Account
                    //AddAnyTransaction(viewModel, viewModel.DestinationAccount);
                    viewModel.DestinationAccount.Transactions.Add(
                       new Transaction
                       {
                           TransactionType = TransactionType.Transfer,
                           AccountNumber = (int)viewModel.DestinationAccountNumber,
                           Amount = viewModel.Amount,
                           Comment = "Transfer From Account" + viewModel.AccountNumber,
                           ModifyDate = DateTime.UtcNow.ToLocalTime()
                       }); ;

                    // apply service charge if necessary
                    if (applyServiceCharge)
                    {
                        viewModel.Account.Transactions.Add(
                          new Transaction
                          {
                              AccountNumber = viewModel.AccountNumber,
                              TransactionType = TransactionType.ServiceCharge,
                              Amount = -serviceCharge,
                              Comment = "Service Charge for " + viewModel.TransactionType.ToString(),
                              ModifyDate = DateTime.UtcNow.ToLocalTime()
                          });
                    }
                    break;


                // DEPOSIT 
                case TransactionType.Deposit:
                    viewModel.Account.Balance += viewModel.Amount;
                    viewModel.DestinationAccountNumber = null;

                    AddAnyTransaction(viewModel, viewModel.Account);

                    break;

                default:
                    break;
            }
        }



        //DETERMINE SERVICE CHARGE
        public decimal DetermineServiceCharge(TransactionType transactionType)
        {

            decimal[] ServiceCharge = { 0.1M, 0.2M };

            if (transactionType == TransactionType.Withdraw)
            {
                return ServiceCharge[0];

            }
            if (transactionType == TransactionType.Transfer)
            {

                return ServiceCharge[1];
            }
            return 0.0M;
        }

        // CALCULATE WHETHER THERE ARE SUFFICIENT FUNDS FOR WITHDRAW/TRANSFER/SCHEDULE PAYMENT
        public bool HasSufficientFunds(AccountType accountType, decimal balance, decimal amount, decimal serviceCharge)
        {
            decimal serviceFee = serviceCharge;
            decimal netBalance = balance - (amount + serviceFee);

            if (accountType == AccountType.Savings && netBalance < 0)
            {
                return false;
            }
            if (accountType == AccountType.Checking && netBalance < 200)
            {
                return false;
            }
            return true;
        }


        //CHECK IF THERE HAVE BEEN MORE THAN 4 FREE WITHDRAW/TRANSFER TRANSACTIONS
        public bool MoreThan4FreeTransaction(MiBankContext context)
        {
            var transactions = context.Transactions;
            //  .Include(t => t.TransactionType);
            transactions.ToList();
            int count = 0;

            foreach (var trans in transactions)
            {
                if ((int)trans.TransactionType == 2 || (int)trans.TransactionType == 3 || (int)trans.TransactionType == 5)
                {
                    count += 1;
                }
            }

            if (count > 4) { return true; }
            return false;
        }




    }
}
