using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Case_Study
{
    class CashDispenser
    {
        int billCount;//ATM'den çekilebilecek max miktar.
        const int INITIAL_COUNT = 1000;

        public CashDispenser() { billCount = INITIAL_COUNT; }
        public void DispenseCash(decimal amount) { billCount -= (int)amount; }
        //Çekilmek istenen değerin max değerden büyük olup olmaması sonucuna göre true yada false döndür. 
        public bool isSufficiantCashAvailable(decimal amount) { return (billCount >= (int)amount) ? true : false; }      
    }
}
