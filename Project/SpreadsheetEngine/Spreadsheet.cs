//Name: Dao Minh Triet
//ID: 011753385

using System.ComponentModel;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using static SpreadsheetEngine.Spreadsheet;
using System.Collections.Generic;
using System.Xml;


namespace SpreadsheetEngine
{
    /// <summary>
    /// This Spreadsheet class is a 2D array of type CreateCell.
    /// It takes number of rows and columns as constructor paramaters to construct a spread sheet.
    /// </summary>
    public class Spreadsheet 
    {
        //----------------------------START SPREADSHEET CLASS------------------------//
        
        private readonly int numrow;
        private readonly int numcol;
        private readonly CreateCell[,] cellarray;
        private CellTree celltree = new CellTree();
 
        //Property Changed event.
        public event PropertyChangedEventHandler CellPropertyChanged = delegate { };

        //get set properties
        private int ColumnCount
        {
            get { return this.numcol; }
        }
        //get set properties
        private int RowCount
        {
            get { return this.numrow; }
        }

        // Constructor 
        public Spreadsheet(int numrow, int numcol)
        {
            this.numrow = numrow;
            this.numcol = numcol;
            cellarray = new CreateCell[numrow, numcol];
            for (int row = 0; row < numrow; row++)
            {
                for (int col = 0; col < numcol; col++)
                {
                    //Create new cell of type CreateCell
                    CreateCell cell = new CreateCell(row, col);

                    //Update cell when cell change
                    cell.PropertyChanged += UpdateCell;
                    cell.PropertyChanged += AddTree;
                    cellarray[row, col] = cell;
                }
            }
        }

        //Helper concrete class of Cell to initialize Cell object. 
        private class CreateCell : Cell
        {
            public CreateCell(int RowIndex, int columnIndex) : base(RowIndex, columnIndex)
            {

            }

        }

        //Get Cell at specific position
        public Cell GetCell(int RowIndex, int ColIndex)
        {
            if (RowIndex > numrow || ColIndex > numcol)
            {
                return null;
            }
            else
            {
                return cellarray[RowIndex, ColIndex];
            }
           
        }

        public Cell GetCell(string name)
        {
            try
            {
                int row;
                int column;
                row = int.Parse(name.Substring(1));
                column = (int)char.Parse(name.Substring(0, 1)) - (int)'A';
                return this.GetCell(row, column);
            }
            catch
            {
                throw new Exception("A variable is unidentified.");
            }
        }
        public void AddTree(object sender, PropertyChangedEventArgs e)
        {
            if (sender is CreateCell && e.PropertyName != "BGColor")
            {

            }
        }
        // Update the cell and its referenced cells whenever it's changed. 
        public void UpdateCell(object sender, PropertyChangedEventArgs e)
        {
            if (sender is CreateCell)
            {
                CreateCell expressionCell = (CreateCell)sender;
                if (expressionCell.Text != null)
                {
             
                    // If the cell text starts without '='
                    if (expressionCell.Text[0] != '=')
                    {    
                        expressionCell.Value = expressionCell.Text;
                    }

                    // If the cell text starts with '=', then we either evaluate the formula if there is no references in the cell or evaluate
                    // this cell and other cells that reference this cell. 
                    else
                    {
                 
                        // Handle unsupported cell name
                        if (expressionCell.unSupportedCell() == true)
                        {
                            expressionCell.Value = "!(bad reference)";
                        }
                        else
                        {
                            // Handle Reference Cell here. 
                            try
                            {
                                // Create an expression Tree  
                                expressionCell.ExpressionTreeObject = new ExpressionTree(expressionCell.Text.Substring(1));

                                // Get this cell Dictionary to handle variable reference. 
                                var variables = expressionCell.ExpressionTreeObject.getVarDict();

                                // Iterate through the dictionary to retrieve the name of the refernced cell. For example: "{A0:null}"
                                foreach ((string name, double? value) in variables)
                                {  
                                    // Handle self reference
                                    if (name == expressionCell.CellID)
                                    {
                                        expressionCell.Text = "!(self reference)";
                                     
                                    }
                                    else if (name != expressionCell.CellID)
                                    {
                                        // Set this variable value to the matching cell name value. "{A0:3}"
                                        expressionCell.ExpressionTreeObject.SetVariable(name, double.Parse(this.GetCell(name).Value));
                                        this.GetCell(name).PropertyChanged -= expressionCell.EventRefernce;
                                        this.GetCell(name).PropertyChanged += expressionCell.EventRefernce;
                                        // Finally Evaluate this cell value. 
                                    }
                                }
                                // Handle self reference
                                if (expressionCell.Text == "!(self reference)")
                                {
  
                                }
                                else
                                {
                                    expressionCell.Value = expressionCell.ExpressionTreeObject.Evaluate().ToString();
                                }
                               
                            }

                            // Catch exception when the variable we set has no value yet. 
                            catch
                            {
                                // Create an expression Tree  
                                expressionCell.ExpressionTreeObject = new ExpressionTree(expressionCell.Text.Substring(1));

                                // Get this cell Dictionary to handle variable reference. 
                                var variables = expressionCell.ExpressionTreeObject.getVarDict();
                                foreach ((string name, double? value) in variables)
                                {
                                    // Handle self reference
                                    if (name == expressionCell.CellID)
                                    {
                                        expressionCell.Text = "!(self reference)";

                                    }
                                    else if (name != expressionCell.CellID)
                                    {
                                        this.GetCell(name).PropertyChanged -= expressionCell.EventRefernce;
                                        this.GetCell(name).PropertyChanged += expressionCell.EventRefernce;
                                    }
                                }
                                // Handle self reference
                                if (expressionCell.Text == "!(self reference)")
                                {

                                }
                                else
                                {
                                    expressionCell.Value = expressionCell.ExpressionTreeObject.Evaluate().ToString();
                                }
                                //throw new Exception("Variables are yet to be set");
                            }
                        }
                    }
                }
                else
                {
                    expressionCell.Value = string.Empty;
                }
            }
            CellPropertyChanged(sender, new PropertyChangedEventArgs("CellChanged"));
        }

        // Save Cell objects to XML format.
        public XDocument Save()
        {
            XElement root = new XElement("spreadsheet");
            List<Cell> cellsWithValues = cellarray.Cast<Cell>().Where(cell => cell != null && cell.HasProperties() == true).ToList();
            foreach (Cell cell in cellsWithValues)
            {
                XElement cellElement = new XElement("cell", new XAttribute("name", cell.CellID),
                    new XElement( "text",  cell.Text),
                    new XElement("bgcolor", ((int)cell.Bgcolor).ToString("X")));
                root.Add(cellElement);
            }
            XDocument doc = new XDocument(
            new XComment("This is a comment"),
            root);
            return doc;
        }

        // Load File 
        public void load(Stream doc)
        {
            // load up the XML files from the infile
            XDocument xmlFile = XDocument.Load(doc);

            var cells = xmlFile.Descendants("cell");

            // CLear undo/redo stack.
            this.clear();

            foreach (XElement cell in cells)
            {
                string name = null;
                string bgColor = 0xFFFFFFFF.ToString();
                string formula = null;
                if (cell.Attribute("name").Value != null)
                {
                    name = cell.Attribute("name").Value;
                }
                if (!cell.Element("bgcolor").IsEmpty)
                {
                    bgColor = cell.Element("bgcolor").Value;
                }
                if (!cell.Element("text").IsEmpty)
                {
                    formula = cell.Element("text").Value;
                }
                // Method to process the loaded data
                ProcessCellData(name, bgColor, formula);
            }
            doc.Dispose();
        }
        
        // Clear cell object
        public void clear()
        {
            List<Cell> cellsWithValues = cellarray.Cast<Cell>().ToList();
            foreach (Cell cell in cellsWithValues)
            {
                cell.clear();
            }
        }

        // Upload data from the file to cell objects in spreadsheet
        private void ProcessCellData(string name, string bgcolor, string formula)
        {
            Cell cellToUpdate = GetCell(name);
            cellToUpdate.Bgcolor = uint.Parse(bgcolor, System.Globalization.NumberStyles.HexNumber);
            cellToUpdate.Text =  formula;
        }
    }

}
