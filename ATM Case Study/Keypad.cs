using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ATM_Case_Study
{
    class Keypad
    {
        public int GetInput()
        {
            int output;
            do { /*integer değer almadan döngüden çıkamaz.*/ }
            while (!Int32.TryParse(ReadLine(), out output));
            return output;
        }
    }
}
