using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    public abstract class OperatorNode : Node
    {
        public string Operator { get; set; }

        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Precedence { get; set; }
        public string Associativity { get; set; }


        public OperatorNode()
        {  
        
        }
        public void SetOperator(Node left, Node right)
        {
            Left = left;
            Right = right;
        }
       
    }
}
