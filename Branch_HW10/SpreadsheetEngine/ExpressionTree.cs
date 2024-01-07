using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine
{
    /// <summary>
    /// This ExpressionTree class takes a string of an expression and compile it into an expression tree. 
    /// To compile the string of expression, tokenizing the string and coverting the token string to postfix form need to be 
    /// implemented first, and then finally compiling the postfix string to the expression tree. 
    /// The Expression Tree class also has Evaluate() method to calculate the expression.
    /// The Expression Tree class also can support precedence, associativity and parentheses of a normal expression. 
    /// The Expression Tree also supports variable value. 
    /// </summary>
    public class ExpressionTree
    {
        private Node root;

        private static NodeFactory factory = new NodeFactory();

        List<string> variableList = new List<string>();
        // This variable reference stores variable name for future assignment. 
        private Dictionary<string, double?> variables = new Dictionary<string, double?> {};
       
        // Constructor takes a string of an expression as an input value. 
        public ExpressionTree(string expression)
        {
            root = Compile(InfixToPostFix(Tokenize(expression)));
        }

        // Get Set of root node. 
        public Node Root {get;set;}

        public Dictionary<string, double?> getVarDict()
        {
            return variables;
        }

        // Set variable to a value. 
        public void SetVariable(string variableName, double? variableValue)
        {
            variables[variableName] = variableValue;
        }

        // Check if a character is an operator.
        public static bool isOperatorChar(char ch)
        {
            if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
            {
                return true;
            }
            return false;
        }

        //Remove all spaces and tokenize the expression string to return a string list of number, variable and parentheses. 
        public List<string> Tokenize(string expression)
        {
            // token is our returning result
            List<string> tokens = new List<string>();

            // currentToken acts as a filter to distinguish variable and number in an expression. 
            StringBuilder currentToken = new StringBuilder();

            // Scan from left to right of the expression string.
            for (int i = 0; i < expression.Length; i++)
            {
                char currentChar = expression[i];

                // Skip if we meet an empty space. 
                if (currentChar == ' ')
                {
                    // Skip spaces
                    continue;
                }

                // If it's an operator or a parenthesis, add the current token and the operator/parenthesis to the list
                else if (isOperatorChar(currentChar) || currentChar == '(' || currentChar == ')')
                {
                    // when we meet an operator or parenthesis, we append what are in the currentToken to the token list
                    // because they are either variable or number.
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken.ToString());
                        currentToken.Clear();
                    }
                    // After adding variable and number from the currentToken, we need
                    // to add the currect operator/parenthesis character to the result list.
                    tokens.Add(currentChar.ToString());
                }
                else
                {
                    // If it's not an operator or a parenthesis, add it to the current token
                    currentToken.Append(currentChar);
                }
            }

            // If there's a non-empty token left, add it to the list
            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken.ToString());
            }

            return tokens;
        }

        /// <summary>
        ///  The Compile function is responsible for parsing a postfix expression into an expression tree, where each node in 
        ///  the tree represents either a constant value, a variable, or a binary operation (addition, subtraction, multiplication, 
        ///  or division).
        /// </summary>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public Node Compile(List<string> postfix)
        {
            // Stack for storing operator character. 
            Stack<Node> st = new Stack<Node>();

            // Initialize right, left, temp Node, num.
            Node right, left;
            double num;
            Node temp;
            OperatorNode tempOp;
            // Scan through the postfix expression from left to right. 
            for (int i = 0; i < postfix.Count; i++)
            {
                // If it is not an operator, then it can be either a variable or number, thus we can parse them into either a Constant
                // Node or a Variable Node. 
                if (!isOperator(postfix[i]))
                {
                    // if the string can be converted to a double
                    if (double.TryParse(postfix[i], out num))
                    {
                        // instantiate and return a new ConstNode
                        temp = new ConstantNode(num);
                    }
                    // otherwise instantiate and return a new VarNode
                    else {
                        temp = new VariableNode(postfix[i], ref variables);

                        // Every time we encounter a new variable, add it to the dictionary of 
                        // variable values with a default value of null
                        if (!variables.ContainsKey(postfix[i]))
                        {
                            variables.Add(postfix[i],0);
                        }
                        
                    }
                    st.Push(temp);
                }

                // If it's an operator, then we create an operator node according to its character. 
                else
                {
                    try
                    {
                        right = st.Pop();
                        left = st.Pop();
                        tempOp = factory.CreateOperatorNode(postfix[i]);
                        tempOp.SetOperator(left, right);
                        st.Push(tempOp);
                    }
                    catch
                    {
                        throw new Exception("An Operator needs 2 numbers or variables to make its computation.");
                    }
                }
            }

            // Pop the remaining Node in the stack which is the root and return it. 
            while(st.Count > 0)
            {
                temp = st.Pop();
                return temp;
            }
            return null;
        }

        // Evaluate Helper. 
        private double? Evaluate(Node node)
        {
            if (node == null)
            {
                return 0;
            }  
            return node.Eval();   
        }

        // Evaluate the tree.
        public double? Evaluate()
        {
            return Evaluate(root);
        }



        // Print out the entire tree 
        public string toString()
        {
            return root.toString();
        }

        //  InfixToPostFix function converts a regular expression to postfix order expression. 
        public List<string> InfixToPostFix(List<string> expression)
        {
            Stack<string> opstack = new Stack<string>(); 
            List <string> result = new List<string>();

            // Scanning each character from left.
            for (int i = 0; i < expression.Count; i++) {

                // if character is an operand
                if (isOperand(expression[i]))
                {
                    result.Add(expression[i]);
                }

                // If character is operator, pop two elements from stack, perform operation and push the result back. 
                else if (isOperator(expression[i])) {

                    // If opstack is not empty and it's not an opening parentheses and it's a lower precedence
                    // Then we pop the opstack and add that operator string to the result list.
                    while (opstack.Count != 0 && HasHigherPrecedence(opstack.Peek(), expression[i])
                                              && !isOpeningParentheses(opstack.Peek())
                        )
                    {
                        string inStackOp = opstack.Pop();
                        result.Add(inStackOp);
                    }
                    opstack.Push(expression[i]);
                }

                // If character is an opening parentheses, we push it to the opstack
                else if (isOpeningParentheses(expression[i]))
                {
                    opstack.Push(expression[i]);
                }

                // If character is a closing parentheses, we pop 1 value in the opstack then add it to the result list
                else if (expression[i] == ")")
                {
                    while(opstack.Count != 0 && !isOpeningParentheses(opstack.Peek())){
                        result.Add(opstack.Pop());
                    }
                    
                    // Extra Pop the opening parentheses
                    opstack.Pop();
                }
            }

            // Add the remaining character in the opstack after the loop. 
            while(opstack.Count != 0)
            {
                result.Add(opstack.Pop());
            }
            return result;
        }

        // Check if it's an opening parentheses
        public bool isOpeningParentheses(string s)
        {
            if (s == "(")
            {
                return true;
            }
            return false;
        }

        // Check if it's a string of operator.
        public bool isOperator(string c)
        {
            if (c == "+" || c == "-" || c == "*" || c == "/")
            {
                return true;
            }
            return false;
        }

        // Check if it's an operand.
        public bool isOperand(string c)
        {
            if (isOperator(c) == false && c != "(" && c != ")")
            {
                return true;
            }
            return false;
        }

        // Check if a character is right associative. 
        public bool IsRightAssociative(string op)
        {
            OperatorNode add = factory.CreateOperatorNode("+");
            OperatorNode sub = factory.CreateOperatorNode("-");
            OperatorNode multiply = factory.CreateOperatorNode("*");
            OperatorNode divide = factory.CreateOperatorNode("/");

            string result = "none";

            if (op == add.Operator)
            {
                result = add.Associativity;
            }
            else if (op == sub.Operator)
            {
                result = sub.Associativity;
            }
            else if (op == multiply.Operator)
            {
                result = multiply.Associativity;
            }
            else if (op == divide.Operator)
            {
                result = divide.Associativity;
            }
            switch (result)
            {
                case "none":
                    break;
                case "left":
                    return false;
                    break;
                case "right":
                    return true;
                    break;
            }
            //When there's an invalid operator, throw exception.
            throw new InvalidOperationException("The Associative comparing is comparing invalid operators.");
        }

        // Get operator precedence based on their operator characteristic. 
        public int GetOperatorWeight(string op)
        {
            int weight = -1;
            OperatorNode add = factory.CreateOperatorNode("+");
            OperatorNode sub = factory.CreateOperatorNode("-");
            OperatorNode multiply = factory.CreateOperatorNode("*");
            OperatorNode divide = factory.CreateOperatorNode("/");

            if (op == add.Operator)
            {
                weight = add.Precedence;
            }
            else if (op == sub.Operator)
            {
                weight = sub.Precedence;
            }
            else if (op == multiply.Operator)
            {
                weight = multiply.Precedence;
            }
            else if (op == divide.Operator)
            {
                weight = divide.Precedence;
            }
            return weight;
        }

        // If op1 precedence < op2 precedence, Return true 
        // Higher precedence = lower number, precedence of 1 is the highest.
        public bool HasHigherPrecedence(string op1, string op2)
        {
            int op1Weight = GetOperatorWeight(op1);
            int op2Weight = GetOperatorWeight(op2);

            // If operators have equal precedence, return true if they are left associative. 
            // return false, if right associative. 
            // if operator is left-associative, left one should be given priority. 
            if (op1Weight == op2Weight)
            {
                if (IsRightAssociative(op1)) return false;
                else return true;
            }
            if (op1Weight < op2Weight) return true;
            else return false;
        }

    }
}
