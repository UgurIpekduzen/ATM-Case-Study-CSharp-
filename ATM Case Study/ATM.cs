using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Threading.Thread;
namespace ATM_Case_Study
{
    class ATM
    {
        BankDatabase bankDatabase;
        CashDispenser cashDispenser;
        int currentAccountNumber;
        DepositSlot depositSlot;
        Keypad keypad;
        Screen screen;
        bool userAuthenticated;

        public ATM()
        {
            userAuthenticated = false;
            currentAccountNumber = 0;
            depositSlot = new DepositSlot();
            keypad = new Keypad();
            screen = new Screen();
            cashDispenser = new CashDispenser();
            bankDatabase = new BankDatabase();
        }
        void AuthenticateUser()
        {
            Clear();
            screen.DisplayMessageLine("Please enter your account number: ");
            int accountNumber = keypad.GetInput();
            screen.DisplayMessageLine("Enter your PIN: ");
            int pinCode = keypad.GetInput();

            userAuthenticated = bankDatabase.AuthenticateUser(accountNumber, pinCode);
            if (userAuthenticated) currentAccountNumber = accountNumber; // Kimlik doğrulaması doğru ise hesaba erişim sağla.
            else screen.DisplayMessageLine("Invalid account number or PIN. Please try again. ");//Kimlik doğrulaması yanlış ise tekrar dene.
            Sleep(2000);
        }
        Transaction CreateTransaction(MenuOption type)
        {
            Transaction temp = null;
            switch(type)
            {
                case MenuOption.BALANCE_INQUIRY:
                   temp = new BalanceInquiry(currentAccountNumber, screen, bankDatabase);
                    break;
                case MenuOption.WITHDRAWAL:
                    temp = new Withdrawal(currentAccountNumber, screen, bankDatabase, keypad, cashDispenser);
                    break;
                case MenuOption.DEPOSIT:
                    temp = new Deposit(currentAccountNumber, screen, bankDatabase, keypad, depositSlot);
                    break;
            }
            return temp;
        }
        void PerformTransactions() 
        {
            Transaction currentTransaction = null;
            bool isUserExited = false;
           
            while(!isUserExited)
            {
                Clear();
                screen.DisplayMessage("MAIN MENU: "
                                    + "\n\n1 - View my balance"
                                    + "\n2 - Withdraw cash"
                                    + "\n3 - Deposit funds"
                                    + "\n4 - Exit"
                                    + "\nPlease enter a choise: ");

                MenuOption menuSelect = (MenuOption)keypad.GetInput();
  
                switch(menuSelect)
                {
                    case MenuOption.BALANCE_INQUIRY:
                    case MenuOption.WITHDRAWAL:
                    case MenuOption.DEPOSIT:
                        currentTransaction = CreateTransaction(menuSelect);
                        currentTransaction.Execute();
                        break;
                    case MenuOption.EXIT_ATM:
                        screen.DisplayMessageLine("Exitting the system...");
                        isUserExited = true;
                        Sleep(2000);
                        Clear();
                        break;
                        //GetInput methodundan bağımsız olarak, enum değerlerinden farklı bir değer girilirse tekrar dene.
                    default: screen.DisplayMessageLine("You did not enter a valid selection. Try again."); break;
                }
            }
        }
        public void Run()
        {
            while(true)
            {
                while(!userAuthenticated)//Kimlik doğrulaması doğru olana kadar döngü içinde işlem gerçekleştir.
                {
                    screen.DisplayMessageLine("Welcome!");
                    AuthenticateUser();
                }
                PerformTransactions();
                userAuthenticated = false;
                currentAccountNumber = 0;
                screen.DisplayMessageLine("Thank you! Goodbye!");
                Sleep(1000);
            }
        }
    }
    enum MenuOption { BALANCE_INQUIRY = 1, WITHDRAWAL = 2, DEPOSIT = 3, EXIT_ATM = 4};
}
