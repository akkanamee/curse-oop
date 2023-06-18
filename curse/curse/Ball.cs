using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse
{
    internal class Ball : MapElement
    {
        private readonly String symbol = "o";

        public override string ToString()
        {
            return symbol;
        }
    }
}
