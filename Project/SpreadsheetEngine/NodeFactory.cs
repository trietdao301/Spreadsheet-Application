using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    internal class NodeFactory
    {
        private static Dictionary<string, Type> operators = new Dictionary<string, Type>();
        public NodeFactory()
        {
            TraverseAvailableOperators((op, type) => operators.Add(op, type));
        
        }
        private delegate void OnOperator(string op, Type type);
        private void TraverseAvailableOperators(OnOperator onOperator)
        {
            // get the type declaration of OperatorNode
            Type operatorNodeType = typeof(OperatorNode);
            // Iterate over all loaded assemblies:
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get all types that inherit from our OperatorNode class using LINQ
                IEnumerable<Type> operatorTypes =
                assembly.GetTypes().Where(type => type.IsSubclassOf(operatorNodeType));
                // Iterate over those subclasses of OperatorNode
                foreach (var type in operatorTypes)
                {
                    // for each subclass, retrieve the Operator property 
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        // Get the character of the Operator
                        //object value = operatorField.GetValue(type);
                        // If “Operator” property is not static, you will need to create
                        // an instance first and use the following code instead (or similar):

                        object value = operatorField.GetValue(Activator.CreateInstance(type));
                        if (value is string)
                        {
                            string operatorSymbol = (string)value;
                            // And invoke the function passed as parameter
                            // with the operator symbol and the operator class
                            onOperator(operatorSymbol, type);

                        }
                    }
                }
            }
        }
        public  OperatorNode CreateOperatorNode(string op)
        {
            if (operators.ContainsKey(op))
            {
                object operatorNodeObject = System.Activator.CreateInstance(operators[op]);
                
                if (operatorNodeObject is OperatorNode)
                {
                    return (OperatorNode)operatorNodeObject;
                }
            }
            throw new Exception("Unhandled operator");
        }
    }
}
