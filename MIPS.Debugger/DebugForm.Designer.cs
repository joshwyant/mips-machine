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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSingleStep = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonNext = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonResume = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelExecutingMethod = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelIPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewRegisters = new System.Windows.Forms.ListView();
            this.columnHeaderRegister = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.buttonEnter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageEdit = new System.Windows.Forms.TabPage();
            this.textBoxProgram = new System.Windows.Forms.TextBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButtonLanguage = new System.Windows.Forms.ToolStripDropDownButton();
            this.cToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mIPSAssemblerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonCompile = new System.Windows.Forms.ToolStripButton();
            this.tabPageDisassembly = new System.Windows.Forms.TabPage();
            this.panelDisassembly = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDisassembly = new System.Windows.Forms.Label();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.tabPageMemory = new System.Windows.Forms.TabPage();
            this.hexBox1 = new Be.Windows.Forms.HexBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonUpdateRange = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timerIPS = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageEdit.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.tabPageDisassembly.SuspendLayout();
            this.panelDisassembly.SuspendLayout();
            this.tabPageMemory.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1259, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(114, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonRun,
            this.toolStripButtonPause,
            this.toolStripButtonSingleStep,
            this.toolStripButtonNext,
            this.toolStripButtonResume,
            this.toolStripButtonStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1259, 27);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonRun.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRun.Image")));
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(38, 24);
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
            this.toolStripButtonPause.Size = new System.Drawing.Size(51, 24);
            this.toolStripButtonPause.Text = "Pause";
            this.toolStripButtonPause.Click += new System.EventHandler(this.toolStripButtonPause_Click);
            // 
            // toolStripButtonSingleStep
            // 
            this.toolStripButtonSingleStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonSingleStep.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSingleStep.Image")));
            this.toolStripButtonSingleStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSingleStep.Name = "toolStripButtonSingleStep";
            this.toolStripButtonSingleStep.Size = new System.Drawing.Size(88, 24);
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
            this.toolStripButtonNext.Size = new System.Drawing.Size(44, 24);
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
            this.toolStripButtonResume.Size = new System.Drawing.Size(65, 24);
            this.toolStripButtonResume.Text = "Resume";
            this.toolStripButtonResume.Click += new System.EventHandler(this.toolStripButtonResume_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonStop.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonStop.Image")));
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(44, 24);
            this.toolStripButtonStop.Text = "Stop";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelExecutingMethod,
            this.toolStripStatusLabelIPS});
            this.statusStrip1.Location = new System.Drawing.Point(0, 793);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1259, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelExecutingMethod
            // 
            this.toolStripStatusLabelExecutingMethod.Name = "toolStripStatusLabelExecutingMethod";
            this.toolStripStatusLabelExecutingMethod.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelIPS
            // 
            this.toolStripStatusLabelIPS.Name = "toolStripStatusLabelIPS";
            this.toolStripStatusLabelIPS.Size = new System.Drawing.Size(0, 17);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewRegisters);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(996, 55);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 738);
            this.panel1.TabIndex = 3;
            // 
            // listViewRegisters
            // 
            this.listViewRegisters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderRegister,
            this.columnHeaderValue});
            this.listViewRegisters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewRegisters.Location = new System.Drawing.Point(0, 0);
            this.listViewRegisters.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listViewRegisters.Name = "listViewRegisters";
            this.listViewRegisters.Size = new System.Drawing.Size(263, 738);
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
            this.splitter1.Location = new System.Drawing.Point(993, 55);
            this.splitter1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 738);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.textBox1);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 479);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(993, 314);
            this.panel2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.DimGray;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(0, 17);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(993, 275);
            this.textBox1.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBoxInput);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 292);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(993, 22);
            this.panel4.TabIndex = 2;
            // 
            // textBoxInput
            // 
            this.textBoxInput.BackColor = System.Drawing.Color.DimGray;
            this.textBoxInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxInput.Enabled = false;
            this.textBoxInput.Font = new System.Drawing.Font("Courier New", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInput.ForeColor = System.Drawing.Color.White;
            this.textBoxInput.Location = new System.Drawing.Point(0, 0);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(913, 21);
            this.textBoxInput.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.Control;
            this.panel5.Controls.Add(this.buttonEnter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(913, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(80, 22);
            this.panel5.TabIndex = 1;
            // 
            // buttonEnter
            // 
            this.buttonEnter.Enabled = false;
            this.buttonEnter.Location = new System.Drawing.Point(4, 0);
            this.buttonEnter.Name = "buttonEnter";
            this.buttonEnter.Size = new System.Drawing.Size(75, 23);
            this.buttonEnter.TabIndex = 0;
            this.buttonEnter.Text = "Enter";
            this.buttonEnter.UseVisualStyleBackColor = true;
            this.buttonEnter.Click += new System.EventHandler(this.buttonEnter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Console";
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter2.Location = new System.Drawing.Point(0, 477);
            this.splitter2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(993, 2);
            this.splitter2.TabIndex = 6;
            this.splitter2.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 55);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(993, 422);
            this.panel3.TabIndex = 7;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageEdit);
            this.tabControl1.Controls.Add(this.tabPageDisassembly);
            this.tabControl1.Controls.Add(this.tabPageMemory);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(993, 422);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageEdit
            // 
            this.tabPageEdit.Controls.Add(this.textBoxProgram);
            this.tabPageEdit.Controls.Add(this.toolStrip3);
            this.tabPageEdit.Location = new System.Drawing.Point(4, 25);
            this.tabPageEdit.Margin = new System.Windows.Forms.Padding(4);
            this.tabPageEdit.Name = "tabPageEdit";
            this.tabPageEdit.Padding = new System.Windows.Forms.Padding(4);
            this.tabPageEdit.Size = new System.Drawing.Size(985, 393);
            this.tabPageEdit.TabIndex = 2;
            this.tabPageEdit.Text = "Edit";
            this.tabPageEdit.UseVisualStyleBackColor = true;
            // 
            // textBoxProgram
            // 
            this.textBoxProgram.AcceptsReturn = true;
            this.textBoxProgram.AcceptsTab = true;
            this.textBoxProgram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxProgram.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProgram.Location = new System.Drawing.Point(4, 31);
            this.textBoxProgram.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxProgram.Multiline = true;
            this.textBoxProgram.Name = "textBoxProgram";
            this.textBoxProgram.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxProgram.Size = new System.Drawing.Size(977, 358);
            this.textBoxProgram.TabIndex = 0;
            this.textBoxProgram.WordWrap = false;
            this.textBoxProgram.TextChanged += new System.EventHandler(this.textBoxProgram_TextChanged);
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButtonLanguage,
            this.toolStripButtonCompile});
            this.toolStrip3.Location = new System.Drawing.Point(4, 4);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(977, 27);
            this.toolStrip3.TabIndex = 1;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripSplitButtonLanguage
            // 
            this.toolStripSplitButtonLanguage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButtonLanguage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cToolStripMenuItem,
            this.mIPSAssemblerToolStripMenuItem});
            this.toolStripSplitButtonLanguage.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButtonLanguage.Image")));
            this.toolStripSplitButtonLanguage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButtonLanguage.Name = "toolStripSplitButtonLanguage";
            this.toolStripSplitButtonLanguage.Size = new System.Drawing.Size(103, 24);
            this.toolStripSplitButtonLanguage.Text = "Language: C";
            // 
            // cToolStripMenuItem
            // 
            this.cToolStripMenuItem.Name = "cToolStripMenuItem";
            this.cToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.cToolStripMenuItem.Text = "C";
            this.cToolStripMenuItem.Click += new System.EventHandler(this.cToolStripMenuItem_Click);
            // 
            // mIPSAssemblerToolStripMenuItem
            // 
            this.mIPSAssemblerToolStripMenuItem.Name = "mIPSAssemblerToolStripMenuItem";
            this.mIPSAssemblerToolStripMenuItem.Size = new System.Drawing.Size(152, 24);
            this.mIPSAssemblerToolStripMenuItem.Text = "ASM";
            this.mIPSAssemblerToolStripMenuItem.Click += new System.EventHandler(this.mIPSAssemblerToolStripMenuItem_Click);
            // 
            // toolStripButtonCompile
            // 
            this.toolStripButtonCompile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonCompile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCompile.Image")));
            this.toolStripButtonCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCompile.Name = "toolStripButtonCompile";
            this.toolStripButtonCompile.Size = new System.Drawing.Size(38, 24);
            this.toolStripButtonCompile.Text = "Run";
            this.toolStripButtonCompile.Click += new System.EventHandler(this.toolStripButtonCompile_Click);
            // 
            // tabPageDisassembly
            // 
            this.tabPageDisassembly.Controls.Add(this.panelDisassembly);
            this.tabPageDisassembly.Controls.Add(this.checkedListBox1);
            this.tabPageDisassembly.Location = new System.Drawing.Point(4, 25);
            this.tabPageDisassembly.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageDisassembly.Name = "tabPageDisassembly";
            this.tabPageDisassembly.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageDisassembly.Size = new System.Drawing.Size(985, 393);
            this.tabPageDisassembly.TabIndex = 0;
            this.tabPageDisassembly.Text = "Disassembly";
            this.tabPageDisassembly.UseVisualStyleBackColor = true;
            // 
            // panelDisassembly
            // 
            this.panelDisassembly.AutoScroll = true;
            this.panelDisassembly.Controls.Add(this.label2);
            this.panelDisassembly.Controls.Add(this.labelDisassembly);
            this.panelDisassembly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDisassembly.Location = new System.Drawing.Point(3, 2);
            this.panelDisassembly.Name = "panelDisassembly";
            this.panelDisassembly.Size = new System.Drawing.Size(780, 389);
            this.panelDisassembly.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(338, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Step through the machine code to view disassembly.";
            // 
            // labelDisassembly
            // 
            this.labelDisassembly.AutoSize = true;
            this.labelDisassembly.Location = new System.Drawing.Point(4, 7);
            this.labelDisassembly.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDisassembly.Name = "labelDisassembly";
            this.labelDisassembly.Size = new System.Drawing.Size(0, 17);
            this.labelDisassembly.TabIndex = 1;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(783, 2);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(199, 389);
            this.checkedListBox1.TabIndex = 2;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChanged);
            // 
            // tabPageMemory
            // 
            this.tabPageMemory.Controls.Add(this.hexBox1);
            this.tabPageMemory.Controls.Add(this.label3);
            this.tabPageMemory.Controls.Add(this.toolStrip2);
            this.tabPageMemory.Location = new System.Drawing.Point(4, 25);
            this.tabPageMemory.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageMemory.Name = "tabPageMemory";
            this.tabPageMemory.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPageMemory.Size = new System.Drawing.Size(985, 393);
            this.tabPageMemory.TabIndex = 1;
            this.tabPageMemory.Text = "Memory";
            this.tabPageMemory.UseVisualStyleBackColor = true;
            // 
            // hexBox1
            // 
            this.hexBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.hexBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hexBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexBox1.LineInfoForeColor = System.Drawing.Color.Empty;
            this.hexBox1.Location = new System.Drawing.Point(3, 29);
            this.hexBox1.Name = "hexBox1";
            this.hexBox1.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox1.Size = new System.Drawing.Size(979, 362);
            this.hexBox1.TabIndex = 1;
            this.hexBox1.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(329, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Step through the machine code to inspect memory.";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripTextBox2,
            this.toolStripButtonUpdateRange});
            this.toolStrip2.Location = new System.Drawing.Point(3, 2);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(979, 27);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 24);
            this.toolStripLabel1.Text = "Range: ";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(15, 24);
            this.toolStripLabel2.Text = "-";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripButtonUpdateRange
            // 
            this.toolStripButtonUpdateRange.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonUpdateRange.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpdateRange.Image")));
            this.toolStripButtonUpdateRange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpdateRange.Name = "toolStripButtonUpdateRange";
            this.toolStripButtonUpdateRange.Size = new System.Drawing.Size(62, 24);
            this.toolStripButtonUpdateRange.Text = "Update";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Source Files|*.asm;*.s;*.c;*.cxx";
            // 
            // timerIPS
            // 
            this.timerIPS.Enabled = true;
            this.timerIPS.Interval = 1000;
            this.timerIPS.Tick += new System.EventHandler(this.timerIPS_Tick);
            // 
            // DebugForm
            // 
            this.AcceptButton = this.buttonEnter;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1259, 815);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "DebugForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MIPS Debugger";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugForm_FormClosing);
            this.Load += new System.EventHandler(this.DebugForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DebugForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageEdit.ResumeLayout(false);
            this.tabPageEdit.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.tabPageDisassembly.ResumeLayout(false);
            this.panelDisassembly.ResumeLayout(false);
            this.panelDisassembly.PerformLayout();
            this.tabPageMemory.ResumeLayout(false);
            this.tabPageMemory.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelExecutingMethod;
        private Be.Windows.Forms.HexBox hexBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button buttonEnter;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpdateRange;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panelDisassembly;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripButton toolStripButtonCompile;
        private System.Windows.Forms.ToolStripDropDownButton toolStripSplitButtonLanguage;
        private System.Windows.Forms.ToolStripMenuItem cToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mIPSAssemblerToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelIPS;
        private System.Windows.Forms.Timer timerIPS;
    }
}