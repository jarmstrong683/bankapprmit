using System;
using Microsoft.EntityFrameworkCore;
using A3_MiBank_App.Models;

namespace A3_MiBank_App.Data
{
    public class MiBankContext : DbContext
    {
        public MiBankContext(DbContextOptions<MiBankContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Transaction>().
            //   HasKey(t => new { t.AccountNumber, t.DestinationAccountNumber });

            base.OnModelCreating(modelBuilder);
            //These fluent API statements add relationships and add Check Constraints
            // that are not achievable using darta annotations/validations alone.


            modelBuilder.Entity<Login>().HasCheckConstraint("CH_Login_UserID", "len(UserID) = 8").
               HasCheckConstraint("CH_Login_PasswordHash", "len(PasswordHash) = 64");

            modelBuilder.Entity<Account>().HasCheckConstraint("CH_Account_Balance", "Balance >= 0");

            //establishes the x2 one-many relationships of account-transaction and destAccount-transaction
            modelBuilder.Entity<Transaction>().
                HasOne(x => x.Account).WithMany(x => x.Transactions).HasForeignKey(x => x.AccountNumber);

           
            //modelBuilder.Entity<Transaction>().HasCheckConstraint("CH_Transaction_Amount", "Amount > 0");

        }

        // sets tables to database using the Models entities
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Payee> Payees { get; set; }
        public DbSet<BillPay> BillPay { get; set; }
    }

}
