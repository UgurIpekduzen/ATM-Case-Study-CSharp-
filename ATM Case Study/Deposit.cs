using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Threading.Thread;
namespace ATM_Case_Study
{
    class Deposit : Transaction
    {
        private decimal amount;
        private Keypad keypad;
        private DepositSlot depositSlot;
        private const int CANCELED = 0;
   
        public Deposit(int userAccountNumber, Screen atmScreen,
            BankDatabase atmBankDatabase, Keypad atmKeypad, DepositSlot atmDepositSlot): base(userAccountNumber, atmBankDatabase, atmScreen)
        {
            keypad = atmKeypad;
            depositSlot = atmDepositSlot;
        }
        public override void Execute()
        {
            amount = PromptForDepositAmount();

            if (amount != CANCELED) //Miktar 0'dan farklı bir değer ise, blok içindeki işlemleri gerçekleştirir.
            {
                screen.DisplayMessageLine("Please insert a deposit envelope containing" 
                                    + "\n$" + amount + " in the deposit slot.");

                bool envelopeReceived = depositSlot.IsDepositEnvelopeReceived(amount); //Yatırılmak istenen değerin kabul edilip edilmediğinin bilgisini tutar.

                if (envelopeReceived)
                {
                    screen.DisplayMessageLine(
                        "Your envelope has been received.\n" +
                        "The money just deposited will not be available " +
                        "until we \nverify the amount of any " +
                        "enclosed cash, and any enclosed checks clear.");

                    bankDatabase.Credit(AccountNumber, amount); 
                }
                else screen.DisplayMessageLine("You did not insert an envelope, so the ATM has canceled your transaction.");
            }
            else screen.DisplayMessageLine("Canceling transaction.");
            Sleep(2000);
        }
        decimal PromptForDepositAmount()
        {
            screen.DisplayMessageLine("Please input a deposit amount in CENTS (or 0 to cancel): ");
            int input = keypad.GetInput();
            return (input == CANCELED) ? CANCELED : input / 100M;//Centi dolara çevirilmiş biçimde döndür.
        }
    }
}

