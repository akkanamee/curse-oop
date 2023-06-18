using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curse
{
    internal class BackSlashWall : MapElement
    {
        private readonly String symbol = "\\";

        public override string ToString()
        {
            return symbol;
        }
    }
}
