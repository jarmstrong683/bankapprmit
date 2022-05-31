using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using A3_MiBank_App.Models;
using X.PagedList;

namespace A3_MiBank_App.ViewModels
{
    public class StatementViewModel
    {
        
        public int AccountNumber { get; set; }

        public Account Account { get; set; }
        public int DestinationAccount { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Amount { get; set; }

        public string Comment { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime ModifyDate { get; set; }

        public IPagedList<Transaction> PagedTransactionsList { get; internal set; }

        
    }
}
