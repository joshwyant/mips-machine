namespace MIPS.Debugger
{
    partial class DebugForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSingleStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResume = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewRegisters = new System.Windows.Forms.ListView();
            this.columnHeaderRegister = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageEdit = new System.Windows.Forms.TabPage();
            this.textBoxProgram = new System.Windows.Forms.TextBox();
            this.tabPageDisassembly = new System.Windows.Forms.TabPage();
            this.labelDisassembly = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPageMemory = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageEdit.SuspendLayout();
            this.tabPageDisassembly.SuspendLayout();
            this.tabPageMemory.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.debugToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(634, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRun,
            this.toolStripButtonPause,
            this.toolStripButtonSingleStep,
            this.toolStripButtonNext,
            this.toolStripButtonResume});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(634, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRun.Image")));
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(32, 22);
            this.toolStripButtonRun.Text = "Run";
            this.toolStripButtonRun.Click += new System.EventHandler(this.toolStripButtonRun_Click);
            // 
            // toolStripButtonPause
            // 
            this.toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonPause.Enabled = false;
            this.toolStripButtonPause.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPause.Image")));
            this.toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPause.Name = "toolStripButtonPause";
            this.toolStripButtonPause.Size = new System.Drawing.Size(42, 22);
            this.toolStripButtonPause.Text = "Pause";
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripButtonSingleStep
            // 
            this.toolStripButtonSingleStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSingleStep.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSingleStep.Image")));
            this.toolStripButtonSingleStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSingleStep.Name = "toolStripButtonSingleStep";
            this.toolStripButtonSingleStep.Size = new System.Drawing.Size(69, 22);
            this.toolStripButtonSingleStep.Text = "Single Step";
            this.toolStripButtonSingleStep.Click += new System.EventHandler(this.toolStripButtonSingleStep_Click);
            // 
            // toolStripButtonNext
            // 
            this.toolStripButtonNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonNext.Enabled = false;
            this.toolStripButtonNext.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonNext.Image")));
            this.toolStripButtonNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNext.Name = "toolStripButtonNext";
            this.toolStripButtonNext.Size = new System.Drawing.Size(35, 22);
            this.toolStripButtonNext.Text = "Next";
            this.toolStripButtonNext.Click += new System.EventHandler(this.toolStripButtonNext_Click);
            // 
            // toolStripButtonResume
            // 
            this.toolStripButtonResume.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonResume.Enabled = false;
            this.toolStripButtonResume.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonResume.Image")));
            this.toolStripButtonResume.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonResume.Name = "toolStripButtonResume";
            this.toolStripButtonResume.Size = new System.Drawing.Size(53, 22);
            this.toolStripButtonResume.Text = "Resume";
            this.toolStripButtonResume.Click += new System.EventHandler(this.toolStripButtonResume_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 478);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStrip1.Size = new System.Drawing.Size(634, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewRegisters);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(470, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(164, 429);
            this.panel1.TabIndex = 3;
            // 
            // listViewRegisters
            // 
            this.listViewRegisters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRegister,
            this.columnHeaderValue});
            this.listViewRegisters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRegisters.Location = new System.Drawing.Point(0, 0);
            this.listViewRegisters.Margin = new System.Windows.Forms.Padding(2);
            this.listViewRegisters.Name = "listViewRegisters";
            this.listViewRegisters.Size = new System.Drawing.Size(164, 429);
            this.listViewRegisters.TabIndex = 0;
            this.listViewRegisters.UseCompatibleStateImageBehavior = false;
            this.listViewRegisters.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderRegister
            // 
            this.columnHeaderRegister.Text = "Register";
            this.columnHeaderRegister.Width = 82;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "Value";
            this.columnHeaderValue.Width = 125;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(468, 49);
            this.splitter1.Margin = new System.Windows.Forms.Padding(2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 429);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 302);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(468, 176);
            this.panel2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 13);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(468, 163);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Output";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 300);
            this.splitter2.Margin = new System.Windows.Forms.Padding(2);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(468, 2);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 49);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(468, 251);
            this.panel3.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageEdit);
            this.tabControl1.Controls.Add(this.tabPageDisassembly);
            this.tabControl1.Controls.Add(this.tabPageMemory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(468, 251);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageEdit
            // 
            this.tabPageEdit.Controls.Add(this.textBoxProgram);
            this.tabPageEdit.Location = new System.Drawing.Point(4, 22);
            this.tabPageEdit.Name = "tabPageEdit";
            this.tabPageEdit.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEdit.Size = new System.Drawing.Size(460, 225);
            this.tabPageEdit.TabIndex = 2;
            this.tabPageEdit.Text = "Edit";
            this.tabPageEdit.UseVisualStyleBackColor = true;
            // 
            // textBoxProgram
            // 
            this.textBoxProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxProgram.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProgram.Location = new System.Drawing.Point(3, 3);
            this.textBoxProgram.Multiline = true;
            this.textBoxProgram.Name = "textBoxProgram";
            this.textBoxProgram.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxProgram.Size = new System.Drawing.Size(454, 219);
            this.textBoxProgram.TabIndex = 0;
            this.textBoxProgram.WordWrap = false;
            // 
            // tabPageDisassembly
            // 
            this.tabPageDisassembly.Controls.Add(this.labelDisassembly);
            this.tabPageDisassembly.Controls.Add(this.label2);
            this.tabPageDisassembly.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisassembly.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDisassembly.Name = "tabPageDisassembly";
            this.tabPageDisassembly.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageDisassembly.Size = new System.Drawing.Size(460, 225);
            this.tabPageDisassembly.TabIndex = 0;
            this.tabPageDisassembly.Text = "Disassembly";
            this.tabPageDisassembly.UseVisualStyleBackColor = true;
            // 
            // labelDisassembly
            // 
            this.labelDisassembly.AutoSize = true;
            this.labelDisassembly.Location = new System.Drawing.Point(9, 6);
            this.labelDisassembly.Name = "labelDisassembly";
            this.labelDisassembly.Size = new System.Drawing.Size(0, 13);
            this.labelDisassembly.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Step through the machine code to view disassembly.";
            // 
            // tabPageMemory
            // 
            this.tabPageMemory.Controls.Add(this.label3);
            this.tabPageMemory.Location = new System.Drawing.Point(4, 22);
            this.tabPageMemory.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageMemory.Name = "tabPageMemory";
            this.tabPageMemory.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageMemory.Size = new System.Drawing.Size(460, 225);
            this.tabPageMemory.TabIndex = 1;
            this.tabPageMemory.Text = "Memory";
            this.tabPageMemory.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(247, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Step through the machine code to inspect memory.";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Assembly Files|*.asm;*.s";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 500);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DebugForm";
            this.Text = "MIPS Debugger";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageEdit.ResumeLayout(false);
            this.tabPageEdit.PerformLayout();
            this.tabPageDisassembly.ResumeLayout(false);
            this.tabPageDisassembly.PerformLayout();
            this.tabPageMemory.ResumeLayout(false);
            this.tabPageMemory.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewRegisters;
        private System.Windows.Forms.ColumnHeader columnHeaderRegister;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonNext;
        private System.Windows.Forms.ToolStripButton toolStripButtonResume;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDisassembly;
        private System.Windows.Forms.TabPage tabPageMemory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelDisassembly;
        private System.Windows.Forms.ToolStripButton toolStripButtonRun;
        private System.Windows.Forms.ToolStripButton toolStripButtonSingleStep;
        private System.Windows.Forms.TabPage tabPageEdit;
        private System.Windows.Forms.TextBox textBoxProgram;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}