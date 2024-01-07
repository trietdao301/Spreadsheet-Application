using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    class ConstantNode : Node
    {
        private double value;
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }
        public override double? Eval() {
            return value;
        }
        public ConstantNode(double value)
        {
            this.value = value;
        }
        public override string toString()
        {
            return value.ToString();
        }
    }
}
