using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//test
namespace A3_MiBank_App.Models
{
    public class Payee
    {
        // Class properties ->
        // become database table columns in the Payee entity
        [Required]
        [StringLength(4, MinimumLength = 4)]
        [Range(1000,9999)]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]//  PK allocated by convention -> EF detects 'Entity name' + 'ID' to make PK (CustomerID)
        public int PayeeID { get; set; }

        [Required, StringLength(50)]// data Validation Annotation
        [Display(Name = "Payee Name")]
        public string PayeeName { get; set; }

        [StringLength(50)]// data Validation Annotation
        public string Address { get; set; }

        [StringLength(40)]// data Validation Annotation
        public string City { get; set; }

        //[StringLength(3,MinimumLength = 2)]// data Validation Annotation
        //[RegularExpression(@"^[A-Za-z]{2,3}$", ErrorMessage = "Enter state: ACT,NSW,NT,QLD,SA,TAS,VIC,WA")]
        public int State { get; set; }

        [StringLength(4, MinimumLength = 4)]// data Validation Annotation
        [RegularExpression(@"^[1-9]?\d{4}$", ErrorMessage = "Enter 4 digit postcode only please")]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [RegularExpression(@"^(\(04\)|04|\+614|\(61\)|0[1-8])([ ]?\d){8,18}$", ErrorMessage = "Enter a Valid phone number")]
        public string Phone { get; set; }

        // establishes one - many relationship (a NAVIGATION PROPERTY)
        public virtual List<BillPay> BillPays { get; set; }

    }
}
