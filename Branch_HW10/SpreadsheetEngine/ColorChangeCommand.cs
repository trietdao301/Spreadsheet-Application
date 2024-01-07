using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// The ColorChangeCommand class implements the ICommand interface, allowing it to participate in a command history. 
    /// It contains two strings representing the old and new color, a list of Cell object for undo and redo implementation. 
    /// </summary>
    public class ColorChangeCommand : ICommand
    {
        private List<Cell> cell;
        public List<uint> oldColor { get; set; }
        public uint newColor { get; set; }

        // Constructor
        public ColorChangeCommand(List<Cell> cell, List<uint> oldColor, uint newColor)
        {
            this.cell = cell;
            this.oldColor = oldColor;
            this.newColor = newColor;
        }

        // Redo color
        public void Redo()
        {
            foreach (Cell cell in this.cell)
            {
                cell.Bgcolor = this.newColor;
            }
            
        }

        // Undo color
        public void Undo()
        {
            int index = 0;
            foreach (Cell cell in this.cell)
            {
                cell.Bgcolor = oldColor[index];
                index++;
            }
        }
    }
}

