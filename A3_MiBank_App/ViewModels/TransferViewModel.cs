using System;
using System.Collections.Generic;
using A3_MiBank_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A3_MiBank_App.ViewModels
{
    public class TransferViewModel : ITransactionViewModel
    {
        [BindProperty]
        public int AccountNumber { get; set; }
        public AccountType AccountType { get; set; }

        public Account Account { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }

        [BindProperty]
        public int DestinationAccountNumber { get; set; }
        public Account DestinationAccount { get; set; }

        public List<SelectListItem> AccountNumbersList { get; set; }
        
    }
    
}
