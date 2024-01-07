namespace Spreadsheet_Triet_Dao
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            button1 = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveFileToolStripMenuItem = new ToolStripMenuItem();
            loadFileToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoTextChangeToolStripMenuItem = new ToolStripMenuItem();
            redoTextChangeToolStripMenuItem = new ToolStripMenuItem();
            cellToolStripMenuItem = new ToolStripMenuItem();
            changeBackgroundColorToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 31);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(948, 500);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellBeginEdit += dataGridView1_CellBeginEdit;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            // 
            // button1
            // 
            button1.Location = new Point(425, 537);
            button1.Name = "button1";
            button1.Size = new Size(144, 29);
            button1.TabIndex = 1;
            button1.Text = "Demo";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, cellToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(976, 28);
            menuStrip1.TabIndex = 2;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveFileToolStripMenuItem, loadFileToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveFileToolStripMenuItem
            // 
            saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            saveFileToolStripMenuItem.Size = new Size(152, 26);
            saveFileToolStripMenuItem.Text = "Save File";
            saveFileToolStripMenuItem.Click += saveFileToolStripMenuItem_Click;
            // 
            // loadFileToolStripMenuItem
            // 
            loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            loadFileToolStripMenuItem.Size = new Size(152, 26);
            loadFileToolStripMenuItem.Text = "Load File";
            loadFileToolStripMenuItem.Click += loadFileToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoTextChangeToolStripMenuItem, redoTextChangeToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(49, 24);
            editToolStripMenuItem.Text = "Edit";
            editToolStripMenuItem.Click += editToolStripMenuItem_Click;
            // 
            // undoTextChangeToolStripMenuItem
            // 
            undoTextChangeToolStripMenuItem.Name = "undoTextChangeToolStripMenuItem";
            undoTextChangeToolStripMenuItem.Size = new Size(131, 26);
            undoTextChangeToolStripMenuItem.Text = "Undo";
            undoTextChangeToolStripMenuItem.Click += undoTextChangeToolStripMenuItem_Click;
            // 
            // redoTextChangeToolStripMenuItem
            // 
            redoTextChangeToolStripMenuItem.Name = "redoTextChangeToolStripMenuItem";
            redoTextChangeToolStripMenuItem.Size = new Size(131, 26);
            redoTextChangeToolStripMenuItem.Text = "Redo ";
            redoTextChangeToolStripMenuItem.Click += redoTextChangeToolStripMenuItem_Click;
            // 
            // cellToolStripMenuItem
            // 
            cellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { changeBackgroundColorToolStripMenuItem });
            cellToolStripMenuItem.Name = "cellToolStripMenuItem";
            cellToolStripMenuItem.Size = new Size(48, 24);
            cellToolStripMenuItem.Text = "Cell";
            // 
            // changeBackgroundColorToolStripMenuItem
            // 
            changeBackgroundColorToolStripMenuItem.Name = "changeBackgroundColorToolStripMenuItem";
            changeBackgroundColorToolStripMenuItem.Size = new Size(274, 26);
            changeBackgroundColorToolStripMenuItem.Text = "Change background Color...";
            changeBackgroundColorToolStripMenuItem.Click += changeBackgroundColorToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(976, 574);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem cellToolStripMenuItem;
        private ToolStripMenuItem changeBackgroundColorToolStripMenuItem;
        private ToolStripMenuItem undoTextChangeToolStripMenuItem;
        private ToolStripMenuItem redoTextChangeToolStripMenuItem;
        private ToolStripMenuItem saveFileToolStripMenuItem;
        private ToolStripMenuItem loadFileToolStripMenuItem;
    }
}