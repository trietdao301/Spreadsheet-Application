using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    class VariableNode : Node
    {
        private string name;
     
        private Dictionary <string, double?> variables;
        public string Variables { get; set; }
        public string Name { get; set; }
        public VariableNode(string name, ref Dictionary<string, double?> variables)
        {
            this.name = name;
            this.variables = variables;
        }
        public override double? Eval()
        {
            double? value = 0;        
            if (this.variables.ContainsKey(this.name))
            {
                if (this.variables[this.name] == 0)
                {
                    value = 0;
                    //throw new ArgumentException($"Variable is not set.");   
                }
                else
                {
                    value = (double?)variables[this.name];
                }
                
            }
            return value;

        }
        public override string toString()
        {
            return name;
        }
    }
    
}
