using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using A3_MiBank_App.Models;
using X.PagedList;


namespace A3_MiBank_App.ViewModels
{
    public class BillPayDisplayViewModel
    {
        public int BillPayID { get; set; }

        public int AccountNumber { get; set; }
        public Account Account { get; set; }
        public AccountType AccountType { get; set; }

        public decimal Amount { get; set; }
        public decimal Balance { get; set; }

        public int PayeeID { get; set; }
        public Payee Payee { get; set; }

        
        public string PayeeName { get; set; }

        public DateTime ScheduleDate { get; set; }
        public int Period { get; set; }
        public DateTime ModifyDate { get; set; }

        public IPagedList<BillPay> PagedBillPayList { get; internal set; }
    }
}
