using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;
namespace ATM_Case_Study
{
    class Withdrawal : Transaction
    {
        decimal amount;
        Keypad keypad;
        CashDispenser cashDispenser;

        const int CANCELED = 7; // Kullanıcının çekmek istediği para miktarını kendisinin belirlemesi için bir seçenek daha eklendi.(Ek özellik)

        public Withdrawal(int userAccount, Screen screen, 
              BankDatabase bankDatabase, Keypad keypad, CashDispenser cashDispenser) :base(userAccount, bankDatabase, screen)
        {
            this.keypad = keypad;
            this.cashDispenser = cashDispenser;
        }
        int displayMenu()
        {
            int userChoice = 0;
            Screen screen = this.screen;

            int[] amounts = {0, 20, 40, 60, 100, 200 };
            while(userChoice == 0)
            {
                Clear();
                screen.DisplayMessageLine("WITHDRAWAL MENU: " 
                                    + "\n\n1 - 20" 
                                    + "\n2 - 40" 
                                    + "\n3 - 60" 
                                    + "\n4 - 100" 
                                    + "\n5 - 200"
                                    + "\n6 - Insert an amount"
                                    + "\n7 - Cancel transaction"
                                    + "\n\nChoose a withdrawal amount: ");

                int choise = keypad.GetInput();
                switch( choise )
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5: userChoice = amounts[choise];break; //1'den 5'e kadarki değerler kadar hesaptan para çekilir.
                    case 6:
                        screen.DisplayMessageLine("How much money do you want to withdraw?: ");//Girilen değer kadar hesaptan para çekilir.
                        userChoice = keypad.GetInput();
                        break;
                    case CANCELED: userChoice = CANCELED; break;
                    default: screen.DisplayMessageLine("Invalid selection. Try again. ");
                        Sleep(2000); break;
                }
            }
            return userChoice;
        }
        public override void Execute()
        {
            bool isCashDispensed = false;
            decimal availableBalance;

            BankDatabase bankDatabase = base.bankDatabase;
            Screen screen = base.screen;

            do
            {
                amount = (decimal)displayMenu();//Method integer değer döndürdüğü için type cast yapıldı.

                if (amount != CANCELED)
                {
                    availableBalance = bankDatabase.getAvailableBalance(AccountNumber);
                    if (amount <= availableBalance)
                    {
                        if (cashDispenser.isSufficiantCashAvailable(amount))
                        {
                            bankDatabase.Debit(AccountNumber, amount);
                            cashDispenser.DispenseCash(amount);
                            isCashDispensed = true;

                            screen.DisplayMessageLine("\nYour cash has been dispensed. Please take your cash now.");
                        }
                        else screen.DisplayMessageLine("\nInsufficient cash available in the ATM.\n\nPlease choose a smaller amount.");
                    }
                    else screen.DisplayMessage("\n Insufficient funds in your account.\n\n Please choose a smaller amount.");
                    Sleep(2000);
                }
                else
                {
                    screen.DisplayMessageLine("\nCancelling transaction...");
                    Sleep(2000);
                    return;
                }
            } while (!isCashDispensed);
        }
    }
}
