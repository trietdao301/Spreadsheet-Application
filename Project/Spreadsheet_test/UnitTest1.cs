//Name: Dao Minh Triet
//ID: 011753385

using System.Windows.Forms;
using SpreadsheetEngine;

namespace Spreadsheet_test
{
    public class Tests
    {
        private CommandHistory commandHistory = new CommandHistory();

        [SetUp]
        public void Setup()
        {
        }

    [Test]
        public void TestGetCellNormal()
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cell = grid.GetCell(2, 4);
            cell.Text = "hello";
            Assert.AreEqual("hello", grid.GetCell(2,4).Text);
        }
        [Test]
        public void TestGetCellBoundary()
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cell = grid.GetCell(0, 0);
            cell.Text = "hello";
            Assert.AreEqual("hello", grid.GetCell(0, 0).Text);
        }
        [Test]
        public void TestGetCellOverFlow()
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cell = grid.GetCell(11, 0);
            Assert.AreEqual(null, cell);
        }
        [Test]
        public void NormalCellReferenceTest()  // Test normal case for cell ref
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cellA0 = grid.GetCell(0, 0);
            Cell cellB0 = grid.GetCell(0, 1);
            cellA0.Text = "5";
            cellB0.Text = "10";
            Cell cellC0 = grid.GetCell(0, 2);
            cellC0.Text = "=A0+B0";
            double result = 15;
            Assert.AreEqual(result.ToString(), cellC0.Value);
        }
        [Test]
        public void UnderflowCellReferenceTest()  // Test cell exception 
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cellA0 = grid.GetCell(0, 0);
            cellA0.Text = "5";      
            Cell cellC0 = grid.GetCell(0, 2);
            cellC0.Text = "=A0+B0";
            string result = "=A0+B0";
            Assert.AreEqual(result.ToString(), cellC0.Value);
        }
        [Test]
        public void OverflowCellReferenceTest()  // Test overflow case where the core cell is changed.
        {
            List<string> list = new List<string>();
            Spreadsheet grid = new Spreadsheet(10, 10);

            Cell cellA0 = grid.GetCell(0, 0);  // Core cell A0 = 5
            cellA0.Text = "5";

            Cell cellB0 = grid.GetCell(0, 1);  // B0 ref to A0
            cellB0.Text = "=A0*2";

            Cell cellC0 = grid.GetCell(0, 2);  // C0 ref to B0
            cellC0.Text = "=B0*2";

            cellA0.Text = "10";                // Change core cell value.

            double result = 40;
            Assert.AreEqual(result.ToString(), cellC0.Value);
        }
        [Test]
        public void NormalUndoRedoTest()     // Test Normal Undo and Redo
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cellA0 = grid.GetCell(0, 0);
            string oldValue = cellA0.Text;
            cellA0.Text = "4";
            string currentValue = cellA0.Value;
            TextChangeCommand textcommand = new TextChangeCommand(cellA0, oldValue, currentValue);
            commandHistory.SaveCommand(textcommand);
            commandHistory.Undo();
            commandHistory.Redo();
            string result = cellA0.Text;
            Assert.AreEqual(result, "4");
        }
        [Test]
        public void UnderflowUndoRedoTest()   // Test under flow where we over undo. 
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cellA0 = grid.GetCell(0, 0);
            string oldValue = cellA0.Text;
            cellA0.Text = "4";
            string currentValue = cellA0.Value;
            TextChangeCommand textcommand = new TextChangeCommand(cellA0, oldValue, currentValue);
            commandHistory.SaveCommand(textcommand);
            commandHistory.Undo();
            commandHistory.Undo();
            string result = cellA0.Text;
            Assert.AreEqual(result, null);
        }
        [Test]
        public void OverflowUndoRedoTest()   // Test overflow case where we over redo. 
        {
            Spreadsheet grid = new Spreadsheet(10, 10);
            Cell cellA0 = grid.GetCell(0, 0);
            string oldValue = cellA0.Text;
            cellA0.Text = "4";
            string currentValue = cellA0.Value;
            TextChangeCommand textcommand = new TextChangeCommand(cellA0, oldValue, currentValue);
            commandHistory.SaveCommand(textcommand);
            commandHistory.Undo();
            commandHistory.Redo();
            commandHistory.Redo();
            string result = cellA0.Text;
            Assert.AreEqual(result, "4");
        }

        [Test]
        public void SkeletonTestHW9()   // Test overflow case where we over redo. 
        {
    
        }
        [Test]
        public void SkeletonTestHW10()   // Test overflow case where we over redo. 
        {

        }
    }
}