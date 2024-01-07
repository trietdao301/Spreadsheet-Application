using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// The TextChangeCommand class implements the ICommand interface, allowing it to participate in a command history. 
    /// It contains two strings representing the old and new text, a Cell object for undo and redo implementation. 
    /// </summary>
    public class TextChangeCommand : ICommand
    {
        private Cell cell;
        public string oldText { get; set; }
        public string newText { get; set; }

        // Constructor 
        public TextChangeCommand(Cell Cell, string? oldText, string newText)
        {
            this.cell = Cell;
            this.oldText = oldText;
            this.newText = newText;
        }

        // Redo cell text to its early state
        public void Redo()
        {
            this.cell.Text = this.newText;
        }

        // Undo cell text to its previous state
        public void Undo()
        {
            this.cell.Text = this.oldText;
        }
    }
}
