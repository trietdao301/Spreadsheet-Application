using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    class AddNode : OperatorNode
    {
   
        public AddNode() : base()
        {
            this.Operator = "+";
            this.Precedence = 7;
            this.Associativity = "left";
        }

        public override double? Eval()
        {
                double? leftValue = (Left).Eval();
                double? rightValue = (Right).Eval();
                return leftValue + rightValue;
            //else
            //{
            //    // Handle the case where either the left or right node is not a constant.
            //    // You can return a special value or throw an exception, depending on your requirements.

            //    return double.NaN; // or throw an exception
            //}
        }
        public override string toString()
        {
            return Left.toString() + "+" + Right.toString();
        }
    }
}

