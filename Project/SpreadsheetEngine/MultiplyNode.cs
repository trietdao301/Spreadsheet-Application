using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    class MultiplyNode : OperatorNode
    {
        public MultiplyNode() : base()
        {
            this.Operator = "*";
            this.Precedence = 6;
            this.Associativity = "left";
        }

        public override double?  Eval()
        {
            double? leftValue = (Left).Eval();
            double? rightValue = (Right).Eval();
            return leftValue * rightValue;

        }
        public override string toString()
        {
            return Left.toString() + "*" + Right.toString();
        }
    }
}
