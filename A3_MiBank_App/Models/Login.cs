using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SimpleHashing;

namespace A3_MiBank_App.Models
{
    public class Login
    {
        [Required]
        [Key]
        [StringLength(8)]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required]
        [MinLength(4), MaxLength(4)]
        [Display(Name = "Customer ID")]
        public int CustomerID { get; set; } // 1-1 relationship BY NAMING CONVENTION THIS 'CUSTOMERID' IS ASSOCIATED WITH THE 'CUSTOMER' ON THE NEXT LINE
        public virtual Customer Customer { get; set; } // TO FORM A FOREIGN KEY!!

        [Required, StringLength(64)]  // should this be length 20?
        public string PasswordHash { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime ModifyDate { get; set; }

    

    }

}
