using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wypozyczalnia
{
    public class DataIncorrect : ApplicationException
    {
        public DataIncorrect()
            : base()
        {
            
        }

        public DataIncorrect(string message)
            : base(message)
        {

        }
    }
}
