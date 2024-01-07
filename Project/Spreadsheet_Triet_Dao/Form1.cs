//Name: Dao Minh Triet
//ID: 011753385

using System;
using System.ComponentModel;
using System.Data.Common;
using System.Windows.Forms;
using System.Xml.Linq;
using SpreadsheetEngine;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static SpreadsheetEngine.Spreadsheet;

namespace Spreadsheet_Triet_Dao
{
    public partial class Form1 : Form
    {
        private Spreadsheet spreadsheet;
        private string cellInput = "";
        private CommandHistory commandHistory = new CommandHistory();
        public Form1()
        {
            this.spreadsheet = new Spreadsheet(50, 26);
            spreadsheet.CellPropertyChanged += UpdateForm;
            InitializeComponent();
            InitializeDataGrid();

        }

        private void UpdateForm(object sender, PropertyChangedEventArgs e)
        {
            // only change the form if the cell's text changed
            if ("CellChanged" == e.PropertyName)
            {
                // get the cell that we need to update
                Cell cellToUpdate = sender as Cell;
                if (cellToUpdate != null)
                {
                    // find its row and column
                    int cellRow = cellToUpdate.GetRowIndex();
                    int cellColumn = cellToUpdate.GetColumnIndex();

                    // update that cell's value in the form
                    dataGridView1.Rows[cellRow].Cells[cellColumn].Value = cellToUpdate.Value;

                    // update that cell's color in the form
                    Color color = Color.FromArgb((int)cellToUpdate.Bgcolor);
                    dataGridView1.Rows[cellRow].Cells[cellColumn].Style.BackColor = color;

                }
            }
        }
        // Add A to Z columns.
        private void InitializeDataGrid()
        {
            // Initialize the ascii number of A and Z.
            int ascii = 65;
            int zascii = 90;
            bool isRunning = true;
            // Loop to add A to Z columns.
            for (int i = ascii; i < zascii + 1; i++)
            {
                DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                column.HeaderText = string.Format("{0}", (char)i);
                dataGridView1.Columns.Add(column);
            }

            // Loop to add 50 rows with their indexes. 
            int numrow = 50;
            for (int i = 0; i < numrow; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                dataGridView1.Rows.Add(row);
                dataGridView1.Rows[i].HeaderCell.Value = (i).ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // When Demo button got clicked, this function executes. 
        private void button1_Click(object sender, EventArgs e)
        {
            // Set the text in about 50 random cells to a text string.
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                int ranrow = random.Next(0, 50);
                int rancol = random.Next(0, 26);
                Cell currentCell = this.spreadsheet.GetCell(ranrow, rancol);
                currentCell.Text = "Hello Worlj";
            }

            // set the text in every cell in column B to “This is cell B#”
            DataGridViewColumn targetColumn = null;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.HeaderText == "B")
                {
                    targetColumn = column;
                    string columnName = targetColumn.HeaderText;
                    int columnIndex = targetColumn.Index;
                    for (int i = 0; i < 50; i++)
                    {
                        Cell currentCell = this.spreadsheet.GetCell(i, columnIndex);
                        currentCell.Text = string.Format("This is cell B{0}", i);
                    }
                }
                // set the text in every cell in column B to “This is cell B#”
            }
            for (int i = 0; i < 50; i++)
            {
                // set every cell in column A (column 0) to column B's value in the same row
                Cell currentCell = this.spreadsheet.GetCell(i, 0);
                currentCell.Text = "=B" + i;
            }
        }

        // End editing mode. 
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Get the current Cell in the spreadsheet. 
            Cell currentCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);
            string? oldValue = currentCell.Value;
            string newValue;
            // If the cell has input.
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                // Get the edited value at specific cell. 
                cellInput = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                // Set Text data of this current cell. 
                currentCell.Text = cellInput;

                // Evaluate the input formula 
                if (currentCell.Text[0] == '=')
                {
                    // Set this data grid cell to the current cell's value. 
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentCell.Value;
                }
            }

            // If there's no value in the cell. 
            else if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                currentCell.setCelltoNull();
                currentCell.Text = null;
            }

            // Create text command and save in stack for further functionality. 
            TextChangeCommand textcommand = new TextChangeCommand(currentCell, oldValue, currentCell.Value);
            commandHistory.SaveCommand(textcommand);
        }

        // During editing mode of the cell.
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // Get the current Cell in the spreadsheet. 
            Cell currentCell = this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex);


            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = currentCell.Text;
            }
            else
            {
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = null;
            }
        }

        // Update background color of the cell. 
        private void changeBackgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                List<Cell> cellList = new List<Cell>();
                List<uint> oldColor = new List<uint>();
                int index = 0;
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    Cell currentCell = this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex);
                    oldColor.Add(currentCell.Bgcolor);
                    currentCell.Bgcolor = (uint)colorDialog.Color.ToArgb();
                    cellList.Add(currentCell);
                }

                // Create color command and save in stack for further functionality. 
                ColorChangeCommand colorcommand = new ColorChangeCommand(cellList, oldColor, (uint)colorDialog.Color.ToArgb());
                commandHistory.SaveCommand(colorcommand);
            }
        }

        // Undo
        private void undoTextChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandHistory.Undo();
        }

        // Redo
        private void redoTextChangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            commandHistory.Redo();
        }

        // Manipulate the UI text of Edit tab. 
        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (commandHistory.getUndo().Count == 0)
            {
                undoTextChangeToolStripMenuItem.Enabled = false;
            }
            else
            {
                undoTextChangeToolStripMenuItem.Enabled = true;
            }

            if (commandHistory.getRedo().Count == 0)
            {
                redoTextChangeToolStripMenuItem.Enabled = false;
            }
            else
            {
                redoTextChangeToolStripMenuItem.Enabled = true;
            }
        }


        // Save File
        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XDocument doc = this.spreadsheet.Save();
            //MessageBox.Show(doc.ToString());
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "XML File|*.xml";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    doc.Save(myStream);
                    myStream.Close();
                }
            }
        }


        // Load File
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // instantiate a OpenFileDialog to assist with loading
            OpenFileDialog openFileDialogBox = new OpenFileDialog();

            // if the user hits "Ok"
            if (openFileDialogBox.ShowDialog() == DialogResult.OK)
            {

                this.spreadsheet.clear();
                //Read the contents of the file into a stream
                using (var fileStream = openFileDialogBox.OpenFile())
                {
                    this.spreadsheet.load(fileStream);
                    this.commandHistory.Clear();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}