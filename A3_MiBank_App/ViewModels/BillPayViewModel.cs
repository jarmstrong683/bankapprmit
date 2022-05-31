using System;
using System.Collections.Generic;
using A3_MiBank_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace A3_MiBank_App.ViewModels
{
    public class BillPayViewModel : ITransactionViewModel
    {
            public int BillPayID { get; set; }

            public int AccountNumber { get; set; }
            public Account Account { get; set; }
            public AccountType AccountType { get; set; }

            public decimal Amount { get; set; }
            public decimal Balance { get; set; }

            public int PayeeID { get; set; }
            public Payee Payee { get; set; }

            [BindProperty]
            public string PayeeName { get; set; }

            public DateTime ScheduleDate { get; set; }
            public PaymentPeriod Period { get; set; }
            public DateTime ModifyDate { get; set; }

            public bool Paid { get; set; }

        public IPagedList<BillPay> PagedBillPayList { get; internal set; }
        public List<SelectListItem> PayeeNameOptions { get; set; }
        public List<SelectListItem> AccountNumbersList { get; internal set; }
    }
}


