using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Threading.Thread;
namespace ATM_Case_Study
{
    class BalanceInquiry : Transaction
    {
        public BalanceInquiry(int AccountNumber, Screen screen, BankDatabase bankDatabase) :base(AccountNumber, bankDatabase, screen) { }
        public override void Execute()
        {
            BankDatabase bankDatabase = base.bankDatabase;
            Screen screen = base.screen;

            decimal availableBalance = bankDatabase.getAvailableBalance(AccountNumber);
            decimal totalBalance = bankDatabase.getTotalBalance(AccountNumber);

            screen.DisplayMessageLine("Balance Information: "
                                      + "\n - Available balance: " + availableBalance 
                                      + "\n - Total balance: " + totalBalance);
            Sleep(5000);
        }
    }
}
