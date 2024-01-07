using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
      public abstract class Node
    {
        public abstract double? Eval();
        public abstract string toString();
    }
}
