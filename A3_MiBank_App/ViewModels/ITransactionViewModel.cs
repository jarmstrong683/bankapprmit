using System;
using A3_MiBank_App.Models;

namespace A3_MiBank_App.ViewModels
{
    public interface ITransactionViewModel
    {
        public int AccountNumber { get; set; }
        public Account Account { get; set; }
        public decimal Amount { get; set; }
    }
}
