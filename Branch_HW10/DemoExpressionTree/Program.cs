

using System.Linq.Expressions;
using System.Xml.Linq;
using SpreadsheetEngine;

namespace DemoExpressionTree {
    public class Program
    {
        private static ExpressionTree ExpTree;
        public static void Main(string[] args)
        {
            string userExp = "((2*3)/6)";
            string name = "";
            double value = 0.0;
            ExpTree = new ExpressionTree(userExp);     
            List <string> list = ExpTree.Tokenize(userExp);
            Menu(list, name, value, ExpTree);
        }

        public static void EnterNewExpression(List<string> expression,string previousName, double previousValue, ExpressionTree ExpTree)
        {
            Console.WriteLine("Enter a new expression:");
            string input = Console.ReadLine();
            ExpressionTree tree = new ExpressionTree(input);
            List<string> list = tree.Tokenize(input);
            Menu(list, previousName, previousValue, tree);
        }
        public static void SetVariable(List<string> expression, string previousName, double previousValue, ExpressionTree ExpTree)
        {
            Console.WriteLine("Enter a new name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter a new value:");
            double value = Convert.ToDouble(Console.ReadLine());
            for (int i = 0; i < expression.Count ; i++)
            {
                if (expression[i] == name)
                {
                    expression[i] = value.ToString();
                }
            }
            ExpTree = new ExpressionTree(string.Join("", expression));
            Menu(expression, name, value, ExpTree);
        }
        public static void Evaluate(List<string> expression, string previousName, double previousValue, ExpressionTree ExpTree)
        {
            string name = previousName;
            double value = previousValue;
           
            Console.WriteLine(ExpTree.Evaluate());
            Menu(expression, name, value, ExpTree);
        }
        public static void Quit()
        {
            Environment.Exit(0);
        }
        public static void Menu(List<string> expression, string previousName, double previousValue, ExpressionTree ExpTree)
        {
            
            Console.WriteLine("Menu (current expression= '{0}')", string.Join("", expression));
            Console.WriteLine("1 = Enter a new expression");
            Console.WriteLine("2 = Set a variable value");
            Console.WriteLine("3 = Evaluate tree");
            Console.WriteLine("4 = Quit");
            string name = previousName;
            double value = previousValue;
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    EnterNewExpression(expression, name, value, ExpTree);
                    break;
                case "2":
                    SetVariable(expression, name, value, ExpTree);
                    break;
                case "3":
                    Evaluate(expression, name, value, ExpTree);
                    break;
                case "4":
                    Quit();
                    break;
            }
        }
    }
}
