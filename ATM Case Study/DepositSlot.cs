using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Case_Study
{
    class DepositSlot
    {
        const decimal SlotCapacity = 1000; //Yatırma yuvasının alabileceği maksimum para miktarı
        //Yatırılacak miktarın yuvanın kapasitesinden fazla olup olmamasına göre true yada false döndür. 
        public bool IsDepositEnvelopeReceived(decimal amount) { return (amount <= SlotCapacity) ? true : false; }
    }
}
