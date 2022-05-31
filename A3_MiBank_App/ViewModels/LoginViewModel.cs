using System;
namespace A3_MiBank_App.ViewModels
{
    public class LoginViewModel
    {

        public string UserID { get; set; }
        public string Password { get; set; }
        public int FailedAttempts { get; set; }
    }
}
