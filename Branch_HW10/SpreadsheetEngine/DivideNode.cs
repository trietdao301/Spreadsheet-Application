using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    class DivideNode : OperatorNode
    {

        public DivideNode() : base()
        {
            this.Operator = "/";
            this.Precedence = 6;
            this.Associativity = "left";
        }

        public override double? Eval()
        {
            double? leftValue = (Left).Eval();
            double? rightValue = (Right).Eval();
            if (rightValue == 0)
            {
                throw new DivideByZeroException("Division by zero is not allowed.");
            }
            return leftValue / rightValue;


            //else
            //{
            //    // Handle the case where either the left or right node is not a constant.
            //    // You can return a special value or throw an exception, depending on your requirements.
            //    return double.NaN; // or throw an exception
            //}
        }
        public override string toString()
        {
            return Operator.ToString();
        }
    }
}
