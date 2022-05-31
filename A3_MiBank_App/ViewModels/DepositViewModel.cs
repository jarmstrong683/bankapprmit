using System;
using A3_MiBank_App.Models;
using System.ComponentModel.DataAnnotations;

namespace A3_MiBank_App.ViewModels
{
    public class DepositViewModel : ITransactionViewModel
    {
        public int AccountNumber { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}

