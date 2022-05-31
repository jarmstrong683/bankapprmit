using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;




namespace A3_MiBank_App.Models
{

        public enum AccountType
        {
            Checking = 1,
            Savings = 2
        }

        public class Account
        {

            [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
            [Range(1000,9999)]
            [Display(Name = "Account Number")]
            public int AccountNumber { get; set; }

            [Display(Name = "Type")]    
            [Required,MinLength(1),MaxLength(8)]
            public AccountType AccountType { get; set; }

            [Required]
            public int CustomerID { get; set; }
            public virtual Customer Customer { get; set; }

            [Range(0,99999999999999)]
            [DataType(DataType.Currency)]
            [Column(TypeName = "money")]
            //[Column(TypeName = "money")]
            public decimal Balance { get; set; }   // ?is this required  

            [Required, DataType(DataType.DateTime)]
            [DisplayFormat(DataFormatString = "{dd/MM/yyyy HH:mm:ss}")]
            public DateTime ModifyDate { get; set; }

            [Display(Name = "Destination Account No")]
            public int DestinationAccountNumber { get; set; }

        // a NAVIGATION PROPERTY
        [JsonIgnore]
        public virtual List<Transaction> Transactions { get; set; } // forms 1-many relationship
        [JsonIgnore]
        public virtual List<BillPay> BillPays { get; set; }
    }
    
    
}
