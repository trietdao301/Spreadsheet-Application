//Name: Dao Minh Triet
//ID: 011753385

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpreadsheetEngine
{  
    /// <summary>
    /// Cell class hold values and texts in each cell of the spread sheet.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        //initialize variables.
        protected string text;
        protected readonly int RowIndex;
        protected readonly int ColumnIndex;
        protected uint bgcolor;
        protected string? value;
        public ExpressionTree ExpressionTreeObject;
        //Property change. 
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        //constructor
        public Cell(int RowIndex, int columnIndex)
        {
            this.RowIndex = RowIndex;
            this.ColumnIndex = columnIndex;
            this.bgcolor = 0xFFFFFFFF;
        }

        //accessory
        public int GetRowIndex()
        {
            return this.RowIndex;
        }

        //accessory
        public int GetColumnIndex()
        {
            return this.ColumnIndex;
        }

        //get set for Text property
        public string? Text
        {
            get { return this.text; }
            set
            {
                if (this.text == value)
                {
                    return;
                }
                else
                {
                    this.text = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        //get set for Value property
        public string Value
        {
            get
            {
                return this.value;
            }
            protected internal set
            {
                if (value == this.value)
                {
                    return;
                }
                else
                {
                    this.value = value ;
                    PropertyChanged(this, new PropertyChangedEventArgs("Value"));
                }
            }
        }

        //get set for Bgcolor property
        public uint Bgcolor
        {
            get
            {
                return this.bgcolor;
            }
            set
            {
                if (bgcolor == value)
                {
                    return;
                }
                else
                {
                    this.bgcolor = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("BGColor"));
                }
            }
        }
        // Event reference to update other related cells when this cell is changed. 
        public void EventRefernce(object sender, PropertyChangedEventArgs e)
        {
            Cell cell = sender as Cell;
            if (cell != this)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Cell"));
            }

            //// if self reference
            //if(cell == this)
            //{
            //    this.Text = "!(self reference)";
            //}
             
        }
        public void setCelltoNull()
        {
            this.Value = null;
        }

        public string CellID
        {
            get
            {
                string cellID = ((char)((char)this.ColumnIndex + 'A')).ToString();
                cellID += this.RowIndex.ToString();
                return cellID;
            }
        }
        public bool HasProperties()
        {
            return this.Text != null || this.Bgcolor != 0xFFFFFFFF; 
        }

        public void clear()
        {
            this.Text = null;
            this.Value = null;
            this.Bgcolor = 0xFFFFFFFF;
        }
        private bool SupportedName(string name)
        {
            // Check if the cellReference is a valid string
            if (string.IsNullOrEmpty(name) || name.Length < 2)
            {
                return false;
            }

            // Extract the column and row from the cellReference
            char columnStr = name[0];
            string rowStr = name.Substring(1);

            // Check if the column is a valid character
            if (char.IsLetter(columnStr) == false || char.IsUpper(columnStr) == false)
            {
                return false;
            }

            // Check if the row is a valid integer within the range
            if (!int.TryParse(rowStr, out int row) || row < 0 || row >= 50)
            {
                return false;
            }
            return true;
        }
        public bool unSupportedCell()
        {
            ExpressionTreeObject = new ExpressionTree(this.Text.Substring(1));
            if (this.ExpressionTreeObject.getVarDict().Count > 0)
            {
                var dict = this.ExpressionTreeObject.getVarDict();
                foreach ((string name, double? value) in dict)
                {
                    if (SupportedName(name))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
