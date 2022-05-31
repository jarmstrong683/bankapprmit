using System;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using A3_MiBank_App.ViewModels;
using A3_MiBank_App.Data;
using System.Threading.Tasks;

namespace A3_MiBank_App.Models
{

    public enum PaymentPeriod
    {
        Minutely = 1,
        Quarterly = 2,
        Annually = 3,
        Single_Payment = 4

    }
    

    public class BillPay
    {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillPayID { get; set; }

        [Required]
        [ForeignKey("Account")]
        [Display(Name = "Account Number")]
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        [Required]
        [ForeignKey("Payee")]
        public int PayeeID { get; set; }
        public virtual Payee Payee { get; set; }

        [Range(0, 99999999999999)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public PaymentPeriod PaymentPeriod { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime ModifyDate { get; set; }

        public bool Paid { get; set; }





        // CREATE BILLPAY TRANSACTION OBJECT
        public void ScheduleBillPay(BillPayViewModel viewModel, decimal serviceCharge, MiBankContext context)
        {
        

            viewModel.Account.BillPays.Add(
                    new BillPay
                    {
                        AccountNumber = viewModel.AccountNumber,
                        Amount = viewModel.Amount,
                        PayeeID = viewModel.PayeeID,
                        ModifyDate = DateTime.UtcNow,
                        ScheduleDate = viewModel.ScheduleDate,
                        PaymentPeriod = viewModel.Period
                    });
        }

        // PAY OFF A SCHEDULE BILL PAY
        public async Task PayBillPay(BillPay billPay, MiBankContext context)
        {

            if (HasSufficientFunds(billPay.Account.Balance, billPay.Amount))
            {

                billPay.Account.Balance -= billPay.Amount;
                billPay.Paid = true;

                Payee payee = await context.Payees.FindAsync(billPay.PayeeID);

                billPay.Account.Transactions.Add(
                    new Transaction
                    {
                        TransactionType = TransactionType.BillPay,
                        AccountNumber = billPay.AccountNumber,
                        DestinationAccount = null,
                        Amount = -billPay.Amount,
                        Comment = "BillPay to Payee " + payee.PayeeName,
                        ModifyDate = DateTime.UtcNow
                    });


                Console.WriteLine("ADDED  to MiBankContext-  Bill Amount: " + billPay.Amount + "deducted from Account: " + billPay.Account.AccountNumber);
                Console.WriteLine("Amount: " + billPay.Amount + " paid from Account: " + billPay.Account.AccountNumber + " to: " + billPay.Payee.PayeeName);
            }

            else
            { Console.WriteLine("Insufficent Funds - Bill Pay payment did not proceed"); }
        }


        // MODIFY BILLPAY SCHEDULE  OBJECT
        public void ModifyScheduleBillPay(BillPayViewModel viewModel, decimal serviceCharge, MiBankContext context)
        {
            // subtract Balance - ???? need to do TIMING
            viewModel.Account.Balance -= viewModel.Amount;


            viewModel.Account.Transactions.Add(
                new Transaction
                {
                    TransactionType = TransactionType.BillPay,
                    AccountNumber = viewModel.AccountNumber,
                    DestinationAccount = null,
                    Amount = -viewModel.Amount,
                    Comment = "BillPay to Payee " + viewModel.PayeeName,
                    ModifyDate = DateTime.UtcNow
                });

            viewModel.Account.BillPays.Add(
                    new BillPay
                    {
                        AccountNumber = viewModel.AccountNumber,
                        Amount = viewModel.Amount,
                        PayeeID = viewModel.PayeeID,
                        ModifyDate = DateTime.UtcNow,
                        ScheduleDate = viewModel.ScheduleDate,
                        PaymentPeriod = viewModel.Period
                    });
        }





        // ADD ANOTHER YEAR TO ANNUAL PAYMENT
        public void AddAnotherYearBillPay(BillPay billPay)
        {
            DateTime scheduledDate = billPay.ScheduleDate.AddYears(1);

            billPay.Account.BillPays.Add(
                    new BillPay
                    {
                        AccountNumber = billPay.AccountNumber,
                        Amount = billPay.Amount,
                        PayeeID = billPay.PayeeID,
                        ModifyDate = DateTime.UtcNow,
                        ScheduleDate = scheduledDate,
                        PaymentPeriod = billPay.PaymentPeriod
                    });
        }

        // ADD ANOTHER QUARTER TO QUARTERLY PAYMENT
        public void AddAnotherQuarterBillPay(BillPay billPay)
        {
            DateTime scheduledDate = billPay.ScheduleDate.AddDays(90);

            billPay.Account.BillPays.Add(
                   new BillPay
                   {
                       AccountNumber = billPay.AccountNumber,
                       Amount = billPay.Amount,
                       PayeeID = billPay.PayeeID,
                       ModifyDate = DateTime.UtcNow,
                       ScheduleDate = scheduledDate,
                       PaymentPeriod = billPay.PaymentPeriod
                   });
        }

        // ADD ANOTHER MINUTE TO MUNUTELY PAYMENT
        public void AddAnotherMinuteBillPay(BillPay billPay)
        {
            DateTime scheduledDate = billPay.ScheduleDate.AddMinutes(1);

            billPay.Account.BillPays.Add(
                new BillPay
                {
                    AccountNumber = billPay.AccountNumber,
                    Amount = billPay.Amount,
                    PayeeID = billPay.PayeeID,
                    ModifyDate = DateTime.UtcNow,
                    ScheduleDate = scheduledDate,
                    PaymentPeriod = billPay.PaymentPeriod
                });
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

        public bool HasSufficientFunds(decimal balance, decimal amount)
        {
            decimal netBalance = balance - amount;

            if (netBalance < 0)
            {
                return false;
            }
            return true;
        }



    }
}
