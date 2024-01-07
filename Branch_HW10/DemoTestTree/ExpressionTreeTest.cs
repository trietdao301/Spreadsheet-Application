using SpreadsheetEngine;
namespace DemoTestTree
{
    public class Tests
    {
        private ExpressionTree ExpTree;
        [SetUp]
        public void Setup()
        {
          
        }
        [Test]
        public void TestEvaluateNormal()        //Test Evaluate normal
        {
            string userExp = "(8 * 2 - 4) + (5 / 5) * ((12 + 6) - (7 * 3) / (4 - 2)) + (9 * 3 - 5) - (20 / 2)";
            ExpTree = new ExpressionTree(userExp);
            double expected = 31.5;
            double? result = ExpTree.Evaluate();
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void TestEvaluateVariable()      //Test Evaluate variable
        {
            string userExp = "c+c+c+d+d+(8 * 2 - 4) + (5 / 5) * ((12 + 6) - (7 * 3) / (4 - 2)) + (9 * 3 - 5) - (20 / 2)+a+b+a+b*2";
            ExpTree = new ExpressionTree(userExp);
            double expected = 31.5;
            double? result = ExpTree.Evaluate();
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestEvaluateUnderFlow()     //Test Evaluate underflow
        {
            string userExp = "(0.01 - 0.0000000000000000001)";
            ExpTree = new ExpressionTree(userExp);
            double? expected = 0.01;
            double? result = ExpTree.Evaluate();
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void TestEvaluateOverFlow()     //Test Evaluate overflow
        {
            string userExp = "(99999999999999999+99999999999999999999999999)";
            ExpTree = new ExpressionTree(userExp);
            double? expected = 1.000000001E+26;
            double? result = ExpTree.Evaluate();
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void TestTokenizeNormalFlow()    //Test Tokenize
        {
            string userExp = "(8 * 2 - 4)+b1";
            ExpTree = new ExpressionTree(userExp);
            List<string> expected = new List<string> { "(","8","*","2","-","4",")","+","b1" } ;
            List<string> result = ExpTree.Tokenize(userExp);
            Assert.AreEqual(expected, result);
        }                                             
        [Test]
        public void TestTokenizeUnderFlow()     //Test Tokenize
        {
            string userExp = "";
            ExpTree = new ExpressionTree(userExp);
            List<string> expected = new List<string> { };
            List<string> result = ExpTree.Tokenize(userExp);
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void TestInfixToPostfixNormalFlow()      //Test InfixToPostFix 
        {
            string userExp = "(8 * 2 - 4)";
            ExpTree = new ExpressionTree(userExp);
            List<string> expected = new List<string> {"8","2","*","4","-"};
            List<string> token = ExpTree.Tokenize(userExp);
            List<string> result = ExpTree.InfixToPostFix(token);
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void TestInfixToPostfixUnderFlow()       //Test InfixToPostFix 
        {
            string userExp = "()";
            ExpTree = new ExpressionTree(userExp);
            List<string> expected = new List<string> {  };
            List<string> token = ExpTree.Tokenize(userExp);
            List<string> result = ExpTree.InfixToPostFix(token);
            Assert.AreEqual(expected, result);
        }
    }
}