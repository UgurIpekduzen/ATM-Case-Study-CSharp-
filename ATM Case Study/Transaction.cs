using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Case_Study
{
    abstract class Transaction
    {
        public int AccountNumber { get; set; }
        public BankDatabase bankDatabase { get; set; }
        public Screen screen { get; set; }
        
        public Transaction(int AccountNumber, BankDatabase bankDatabase, Screen screen)
        {
            this.AccountNumber = AccountNumber;
            this.bankDatabase = bankDatabase;
            this.screen = screen;
        }
        public abstract void Execute();
    }
}
