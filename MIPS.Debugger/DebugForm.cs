using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIPS.Architecture;
using System.IO;

namespace MIPS.Debugger
{
    public partial class DebugForm : Form
    {
        #region Helper Enums
        enum Language
        {
            C,
            ASM
        }
        #endregion

        #region props
        public MIPS.Architecture.Debugger Debugger;

        Queue<string> Input = new Queue<string>();
        bool isWaitingForInput = false;
        Action<string> DoInput;

        bool isSingleStepping = false;

        Language EditorLanguage = Language.C;

        public bool AutoRun { get; set; }

        string loadedProgram;

        public long lastElapsed { get; set; }

        public VideoForm VideoForm { get; set; }
        #endregion

        #region Constructors
        public DebugForm(Machine machine, ELFSharp.IELF elf)
        {
            InitializeComponent();

            Debugger = new MIPS.Architecture.Debugger(machine, elf);

            // Add event handlers
            Debugger.Machine.CPU.CPUStep += new EventHandler(CPU_CPUStep);
            Debugger.Machine.CPU.BreakpointHit += new EventHandler(CPU_BreakpointHit);
            Debugger.Machine.CPU.BreakpointHit += new EventHandler(CPU_CPUStep);
            Debugger.Machine.CPU.Paused += new EventHandler(CPU_CPUStep);
            Debugger.Machine.CPU.Syscall += new EventHandler(CPU_Syscall);

            UpdateDebugInfo();

            KeyPreview = true;
        }
        #endregion

        #region CPU Event Handlers
        void CPU_Syscall(object sender, EventArgs e)
        {
            Syscall();
        }

        void CPU_BreakpointHit(object sender, EventArgs e)
        {
            GoToDisassemblyPage();
        }

        void CPU_CPUStep(object sender, EventArgs e)
        {
            hexBox1.Visible = true;

            toolStripButtonResume.Enabled = true;
            toolStripButtonNext.Enabled = true;
            toolStripButtonPause.Enabled = false;

            for (int i = 0; i < 32; i++)
            {
                unsafe
                {
                    if (listViewRegisters.Items.Count == 32)
                    {
                        var str = "0x" + Debugger.Machine.CPU.RF[i].ToString("X8");

                        if (listViewRegisters.Items[i].SubItems[1].Text == str)
                            listViewRegisters.Items[i].ForeColor = Color.Black;
                        else
                            listViewRegisters.Items[i].ForeColor = Color.Red;

                        listViewRegisters.Items[i].SubItems[1].Text = str;
                    }
                    else
                    {
                        listViewRegisters.Items.Add(new ListViewItem(new[] { "$" + Architecture.InstructionSet.RegisterNames[i], "0x" + Debugger.Machine.CPU.RF[i].ToString("X8") }));
                    }
                }
            }

            label2.Text = "";
            labelDisassembly.Text += Debugger.Machine.CPU.IR.ToString() + "\r\n";
            panelDisassembly.VerticalScroll.Value = panelDisassembly.VerticalScroll.Maximum;
            toolStripStatusLabelExecutingMethod.Text = string.Format("Executing method: {0}", Debugger.ExecutingFunction);
        }
        #endregion

        #region Action Buttons
        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            Resume();
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            Pause();
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            NextStep();
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            BuildCode();
        }

        private void toolStripButtonSingleStep_Click(object sender, EventArgs e)
        {
            SingleStepMode();
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
        #endregion

        #region Actions
        private void Resume()
        {
            toolStripButtonResume.Enabled = false;
            toolStripButtonNext.Enabled = false;
            toolStripButtonPause.Enabled = true;

            Debugger.Machine.CPU.Resume();

            GoToDisassemblyPage();

            ExitDisassemblyMode();
        }

        private void Pause()
        {
            toolStripButtonPause.Enabled = false;
            toolStripButtonResume.Enabled = true;

            Debugger.Machine.CPU.Pause();

            GoToDisassemblyPage();
        }

        private void NextStep()
        {
            toolStripButtonNext.Enabled = false;

            Debugger.Machine.CPU.NextStep();

            GoToDisassemblyPage();
        }

        private void Run()
        {
            toolStripButtonRun.Enabled = false;
            toolStripButtonSingleStep.Enabled = false;
            toolStripButtonPause.Enabled = true;

            Debugger.Machine.CPU.SingleStep = false;
            Debugger.Machine.Run();

            GoToDisassemblyPage();
        }

        private void ExitDisassemblyMode()
        {
            toolStripStatusLabelExecutingMethod.Text = "";
            labelDisassembly.Text = "";
            label2.Text = "Step through the program to view disassembly.";
        }

        private void SingleStepMode()
        {
            toolStripButtonRun.Enabled = false;
            toolStripButtonSingleStep.Enabled = false;
            toolStripButtonResume.Enabled = true;

            BuildASMProgram();

            Debugger.Machine.CPU.SingleStep = true;
            Debugger.Machine.Run();

            GoToDisassemblyPage();
        }

        private void GoToDisassemblyPage()
        {
            tabControl1.SelectedTab = tabPageDisassembly;
        }

        private void UpdateDebugInfo()
        {
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(Debugger.MethodSymbols.OrderBy(kvp => kvp.Key).Select(kvp => (object)kvp.Key).ToArray());
        }

        private void Stop()
        {
            // Stop the CPU
            Debugger.Machine.CPU.Stop();

            //if (loadedProgram != null)
            //{
            //    System.IO.File.Delete(loadedProgram);
            //}
            loadedProgram = null;

            // Reset the view
            textBox1.Text = "";
            textBoxInput.Text = "";
            textBoxInput.Enabled = false;
            buttonEnter.Enabled = false;

            toolStripButtonRun.Enabled = true;
            toolStripButtonPause.Enabled = false;
            toolStripButtonSingleStep.Enabled = true;
            toolStripButtonNext.Enabled = false;
            toolStripButtonResume.Enabled = false;

            // Exit disassembly mode
            ExitDisassemblyMode();

            if (VideoForm != null && VideoForm.Created)
            {
                VideoForm.Close();
            }
        }
        #endregion

        #region editor
        private void textBoxProgram_TextChanged(object sender, EventArgs e)
        {
            //toolStripButtonRun.Enabled = true;
            //toolStripButtonSingleStep.Enabled = true;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Stop();

                textBoxProgram.Text = System.IO.File.ReadAllText(openFileDialog.FileName);

                var fi = new FileInfo(openFileDialog.FileName);
                switch (fi.Extension.ToLowerInvariant())
                {
                    case ".asm":
                    case ".s":
                        EditorLanguage = Language.ASM;
                        toolStripSplitButtonLanguage.Text = "Language: ASM";
                        break;
                    case ".c":
                    case ".cxx":
                        EditorLanguage = Language.C;
                        toolStripSplitButtonLanguage.Text = "Language: C";
                        break;
                }

                tabControl1.SelectedTab = tabPageEdit;
            }
        }
        #endregion

        #region Syscall
        private unsafe void Syscall()
        {
            var Machine = Debugger.Machine;
            var RF = Machine.CPU.RF;

            switch (RF[(int)Register.v0])
            {
                // Print Integer, a0 = number to be printed
                case 1:
                    textBox1.Text += ((int)RF[(int)Register.a0]).ToString();
                    textBox1.ScrollToCaret();
                    break;
                // Print Float, a0 = number to be printed
                case 2:
                    throw new NotImplementedException();
                // Print Double, a0 = number to be printed
                case 3:
                    throw new NotImplementedException();
                // Print String, a0 = address of string in memory
                case 4:
                    var addr = RF[(int)Register.a0];
                    while (true)
                    {
                        byte b = Machine.Memory.GetByte((uint)addr);

                        if (b == 0)
                            break;
                        if (b == 10)
                            textBox1.Text += "\r\n";
                        else
                            textBox1.Text += (char)b;
                        textBox1.SelectionStart = textBox1.TextLength;
                        textBox1.ScrollToCaret();
                            
                        addr++;
                    }
                    break;
                // Read Integer, Number returned in v0
                case 5:
                    WaitForInput(str => {
                        int value = 0;
                        int.TryParse(str, out value);
                        RF[(int)Register.v0] = (uint)value;
                    });
                    return;
                // Read Float, Number returned in f0
                case 6:
                    throw new NotImplementedException();
                // Read Double, Number returned in f0
                case 7:
                    throw new NotImplementedException();
                // Read String, a0 = address of input buffer in memory, a1 = length of buffer (n)
                case 8:
                    throw new NotImplementedException();
                // Sbrk, a0 = amount, address returned in v0
                case 9:
                    throw new NotImplementedException();
                // Exit
                case 10:
                    return;
                // Video Syscall
                case 100:
                    var retval = 0;
                    switch (RF[(int)Register.a0])
                    {
                        case 0:
                            VideoForm = new VideoForm();

                            VideoForm.Show();
                            break;
                        case 1:
                            // Get the bytes from memory.
                            uint videomem = 0x80000000;

                            byte[] buffer = new byte[1024 * 768 * 4];

                            for (int i = 0; i < 1024 * 768; i++)
                            {
                                var bytes = BitConverter.GetBytes(Debugger.Machine.Memory.GetWord(videomem + (uint)i * 4));

                                Buffer.BlockCopy(bytes, 0, buffer, i * 4, 4);
                            }

                            VideoForm.UpdateScreen(buffer);

                            break;
                        case 2:
                            var r = new Random(Environment.TickCount);

                            retval = r.Next();
                            break;
                    }

                    RF[(int)Register.v0] = (uint)retval;
                    break;
            }
            Machine.CPU.SyscallSync.Set();
        }

        unsafe private void WaitForInput(Action<string> action)
        {
            isWaitingForInput = true;
            textBoxInput.Enabled = true;
            buttonEnter.Enabled = true;
            textBoxInput.Focus();
            DoInput = action;
            
            if (Input.Count != 0)
            {
                SendInput();
            }
        }
        #endregion

        #region Console
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            Input.Enqueue(textBoxInput.Text);
            textBoxInput.Text = "";

            if (isWaitingForInput)
            {
                SendInput();
            }
        }

        private unsafe void SendInput()
        {
            isWaitingForInput = false;
            textBoxInput.Enabled = false;
            buttonEnter.Enabled = false;

            var str = Input.Dequeue();

            textBox1.Text += str;

            DoInput(str);

            Debugger.Machine.CPU.SyscallSync.Set();
        }
        #endregion

        #region Form Events
        private void DebugForm_Load(object sender, EventArgs e)
        {
            if (AutoRun)
                Run();
        }

        private void DebugForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debugger.Machine.CPU.Stop();
        }
        #endregion

        #region Form Misc
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new AboutBox();

            dialog.ShowDialog();
        }

        private void DebugForm_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.F11:
                    if (Debugger.Machine.CPU.SingleStep || Debugger.Machine.CPU.IsPaused)
                    {
                        NextStep();
                    }
                    else if (Debugger.Machine.CPU.IsRunning)
                    {
                        Pause();
                    }
                    else
                    {
                        SingleStepMode();
                    }
                    break;
                case Keys.F5:
                    if (Debugger.Machine.CPU.IsRunning && (Debugger.Machine.CPU.IsPaused || Debugger.Machine.CPU.SingleStep))
                    {
                        Resume();
                    }
                    else if (!Debugger.Machine.CPU.IsRunning)
                    {
                        BuildCode();
                    }
                    break;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkedItems = checkedListBox1.CheckedItems.Cast<string>();
            var notChecked = checkedListBox1.Items.Cast<string>().Except(checkedItems);

            checkedItems.ToList().ForEach(s => Debugger.BreakAt(s));
            notChecked.ToList().ForEach(s => Debugger.RemoveBreakPoint(s));
        }

        private void timerIPS_Tick(object sender, EventArgs e)
        {
            var currentElapsed = Debugger.Machine.CPU.Elapsed;
            toolStripStatusLabelIPS.Text = string.Format("IPS: {0}", currentElapsed - lastElapsed);
            lastElapsed = currentElapsed;
        }
        #endregion

        #region Build
        private void BuildCode()
        {
            if (!string.IsNullOrWhiteSpace(textBoxProgram.Text))
            {
                try
                {
                    Stop();

                    // Un-map all the memory and just map the video memory and stack.
                    Debugger.Machine.Memory.Mappings.Clear();
                    Debugger.Machine.Memory.Map(0x7FFF0000, 0x7FFFFFFC, 0x00C00000); // Stack
                    Debugger.Machine.Memory.Map(0x80000000, 0x807FFFFC, 0x00100000); // Video memory

                    if (EditorLanguage == Language.ASM)
                    {
                        BuildASMProgram();
                    }
                    else if (EditorLanguage == Language.C)
                    {
                        BuildCxxProgram();
                    }

                    Run();
                }
                catch (ElfToolchain.GCCCompileError ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BuildASMProgram()
        {
            // Set the machine to little-endian mode
            Debugger.Machine.Memory.IsBigEndian = false;

            // Map the text and data segment
            Debugger.Machine.Memory.Map(0x00400000, 0x1000FFFC, 0x00800000); // Text segment
            Debugger.Machine.Memory.Map(0x10010000, 0x6FFFFFFC, 0x00810000); // Data segment

            Assembler asm = new Assembler();
                
            MemoryStream ms = new MemoryStream();
            StreamWriter w = new StreamWriter(ms);
            w.Write(textBoxProgram.Text);
            w.Flush();

            ms.Position = 0;

            asm.Read(ms);

            var writer = new DefaultAssemblyWriter(Debugger.Machine, 0x00400000, 0x10010000);

            asm.Write(writer);
        }

        private void BuildCxxProgram()
        {
            const string path = @"..\..\..\gcc-bin";
            const string prefix = "mips-sde-elf";
            var program = string.Format("p-{0}.elf", Guid.NewGuid());

            loadedProgram = program;

            // Save the program
            System.IO.File.WriteAllText("editor.c", textBoxProgram.Text);

            // Create a program
            var toolchain = new ElfToolchain(path, prefix);
            var lib = new[] { "syscalls.c", "video.c", "math.c" };
            var sources = new[] { "editor.c" };
            var bigEndian = false;
            toolchain.ExecuteTool("gcc", string.Format("-mips1 -E{2} -o {0} {1}", program, string.Join(" ", sources.Union(lib)), bigEndian ? "B" : "L"));
            var mips = Debugger.Machine;

            // Load the program
            var elf = mips.LoadElf(program, 0x00800000);

            Debugger = new Architecture.Debugger(mips, elf);

            UpdateDebugInfo();
        }
        #endregion

        #region Build Action Buttons
        private void toolStripButtonCompile_Click(object sender, EventArgs e)
        {
            BuildCode();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButtonLanguage.Text = "Language: C";
            EditorLanguage = Language.C;
        }

        private void mIPSAssemblerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripSplitButtonLanguage.Text = "Language: ASM";
            EditorLanguage = Language.ASM;
        }
        #endregion
    }
}
