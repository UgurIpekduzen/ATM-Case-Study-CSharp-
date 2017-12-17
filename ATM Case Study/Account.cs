using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Case_Study
{
    class Account
    {
        public int AccountNumber { get; set; }
        public decimal AvailableBalance { get; set; }
        int Pin { get; set; }
        public decimal TotalBalance { get; set; }
        public Account(int AccountNumber, int Pin, decimal TotalBalance, decimal AvailableBalance)
        {
            this.AccountNumber = AccountNumber;
            this.Pin = Pin;
            this.TotalBalance = TotalBalance;
            this.AvailableBalance = AvailableBalance;
        }
        public void Credit(decimal amount) { TotalBalance += amount; }
        public void Debit(decimal amount)
        {
            AvailableBalance -= amount;
            TotalBalance -= amount;
        }
        public bool ValidatePin(int userPin) { return (userPin == Pin) ? true : false; }
    }
}
