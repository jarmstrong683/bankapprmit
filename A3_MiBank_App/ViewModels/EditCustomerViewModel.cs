using System;
using A3_MiBank_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace A3_MiBank_App.ViewModels

{
    public class EditCustomerViewModel
    {
     
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string TFN { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public States State { get; set; }
        public int PostCode { get; set; }
        public string Phone { get; set; }

        public Customer Customer { get; internal set; }
        //public Login Login { get; internal set; }

        [BindProperty]
        public string CurrentPassword { get; set; }
        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public string ConfirmNewPassword { get; set; }
        //public string UserID { get; internal set; }
    }
}
