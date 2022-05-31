using System;
using System.Collections.Generic;
using A3_MiBank_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A3_MiBank_App.ViewModels
{
    public class ATMViewModel : ITransactionViewModel
    {
        public TransactionType TransactionType { get; set; }

        [BindProperty]
        public int AccountNumber { get; set; }
        public Account Account { get; set; }

        [BindProperty]
        public int? DestinationAccountNumber { get; set; }
        public Account DestinationAccount { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Comment { get; set; }

        public List<SelectListItem> AccountNumbersList { get; set; }
    }
}
