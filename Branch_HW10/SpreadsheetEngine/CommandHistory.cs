using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetEngine
{
    /// <summary>
    /// The CommandHistory class is designed to keep track of a history of executed commands and allows undoing and redoing 
    /// those commands.
    /// </summary>
    public class CommandHistory
    {
        private Stack<ICommand> RedoStack  = new Stack<ICommand>();
        private Stack<ICommand> UndoStack = new Stack<ICommand>();

        public Stack<ICommand> getUndo()
        {
            return this.UndoStack;
        }
        public Stack<ICommand> getRedo()
        {
            return this.RedoStack;
        }

        // Save command to undo stack. 
        public void SaveCommand(ICommand command)
        {
            UndoStack.Push(command);
        }

        // Undo command in the stack.
        public void Undo()
        {
            if (UndoStack.Count > 0)
            {
                ICommand command = UndoStack.Pop();
                command.Undo();
                RedoStack.Push(command);   
            }
        }

        // Redo command in the stack. 
        public void Redo()
        {
            if (RedoStack.Count > 0)
            {
                ICommand command = RedoStack.Pop();
                command.Redo();
                UndoStack.Push(command);
            }
        }
        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
        }
    }
}
