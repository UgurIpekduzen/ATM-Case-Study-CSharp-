using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Case_Study
{
    class BankDatabase
    {
        public Account[] accounts;

        public BankDatabase()
        {
            //Deneme account nesneleri
            accounts = new Account[2];
            accounts[0] = new Account( 1, 2, 10000, 1000);
            accounts[1] = new Account( 3, 4, 2000, 2000);
        }
        public bool AuthenticateUser(int userAccountNumber, int userPin)
        {
            //Şu hesap numarasına kayıtlı kullanıcıyı bul, kullanıcı hesabının null olup olmamasına göre false döndür yada PIN Kodu onayına geç.
            Account userAccount = GetAccount(userAccountNumber);
            return (userAccount != null) ? userAccount.ValidatePin(userPin) : false;
        }
        public void Credit(int userAccountNumber, decimal amount) { GetAccount(userAccountNumber).Credit(amount); }
        public void Debit(int userAccountNumber, decimal amount){ GetAccount(userAccountNumber).Debit(amount); }
        Account GetAccount(int AccountNumber)
        {
            Account current;
            for(int i = 0; i < accounts.Length; i++)
            {
                current = accounts[i];//Constructor içindeki default nesneleri sırayla tutar.
                if (current.AccountNumber == AccountNumber) return current;//Mevcut nesnenin hesap numarası aranan hesap numarasına eşitse, current içinde kayıtlı accounts elemanını döndür.
            }
            return null;//Bulamazsa null değer döndür.
        }
        public decimal getAvailableBalance(int userAccountNumber) { return GetAccount(userAccountNumber).AvailableBalance; }
        public decimal getTotalBalance(int userAccountNumber) { return GetAccount(userAccountNumber).TotalBalance; }
    }
}
