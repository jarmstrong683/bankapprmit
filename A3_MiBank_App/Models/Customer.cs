using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using A3_MiBank_App.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace A3_MiBank_App.Models
{

    public enum States
    {
        ACT = 1,
        NSW = 2,
        NT = 3,
        QLD = 4,
        SA = 5,
        TAS = 6,
        VIC = 7,
        WA = 8
    }

    public class Customer
    {

        
        // Class properties ->
        // become database table columns in the Customer entity
        [Required]
        //[StringLength(4,MinimumLength = 4)]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]//  PK allocated by convention -> EF detects 'Entity name' + 'ID' to make PK (CustomerID)
        public int CustomerID { get; set; }

        [Required, StringLength(50)]// data Validation Annotation
        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [StringLength(11,MinimumLength = 11)]
        public string TFN { get; set; }

        [StringLength(50)]// data Validation Annotation
        public string Address { get; set; }

        [StringLength(40)]// data Validation Annotation
        public string City { get; set; }


        public States State { get; set; }

        //[StringLength(4,MinimumLength = 4)]// data Validation Annotation
        [RegularExpression(@"^[1-9]?\d{4}$", ErrorMessage = "Enter 4 digit postcode only please")]
        [Display(Name = "Post Code")]
        public int PostCode { get; set; }

        [Required]
        [RegularExpression(@"^(\(04\)|04|\+614|\(61\)|0[1-8])([ ]?\d){8,18}$", ErrorMessage = "Enter a Valid Australian Fixed or Mobile Phone number")]
        public string Phone { get; set; }

        // establishes one - many relationship (a NAVIGATION PROPERTY)
        [JsonIgnore]
        public virtual List<Account> Accounts { get; set; }


        public string StatesToString(int index)
        {

            string state = Enum.GetName(typeof(States), index);
            return state;
        }

        public async Task<string> GetCustomerNameFromAsync(int accountNumber,MiBankContext context)
        {

            var account = await context.Accounts
                .FirstOrDefaultAsync(m => m.AccountNumber == accountNumber);
            var customer = await context.Customers
                .FirstOrDefaultAsync(c => c.CustomerID == account.CustomerID);
            return customer.CustomerName;
        }

    }
}
