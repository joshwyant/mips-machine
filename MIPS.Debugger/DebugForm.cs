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
        Machine Machine;
        public DebugForm(Machine machine)
        {
            InitializeComponent();

            Machine = machine;
            Machine.CPU.CPUStep += new EventHandler(CPU_CPUStep);
            Machine.CPU.BreakpointHit += new EventHandler(CPU_CPUStep);
            Machine.CPU.Paused += new EventHandler(CPU_CPUStep);
        }

        void CPU_CPUStep(object sender, EventArgs e)
        {
            toolStripButtonResume.Enabled = true;
            toolStripButtonNext.Enabled = true;
            toolStripButtonPause.Enabled = false;

            for (int i = 0; i < 32; i++)
            {
                unsafe
                {
                    if (listViewRegisters.Items.Count == 32)
                    {
                        var str = "0x" + Machine.CPU.RF[i].ToString("X8");

                        if (listViewRegisters.Items[i].SubItems[1].Text == str)
                            listViewRegisters.Items[i].ForeColor = Color.Black;
                        else
                            listViewRegisters.Items[i].ForeColor = Color.Red;

                        listViewRegisters.Items[i].SubItems[1].Text = str;
                    }
                    else
                    {
                        listViewRegisters.Items.Add(new ListViewItem(new[] { "$" + Architecture.InstructionSet.RegisterNames[i], "0x" + Machine.CPU.RF[i].ToString("X8") }));
                    }
                }
            }

            label2.Text = "";
            labelDisassembly.Text += Machine.CPU.IR.ToString() + "\r\n";
        }

        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            toolStripButtonResume.Enabled = false;
            toolStripButtonNext.Enabled = false;
            toolStripButtonPause.Enabled = true;

            Machine.CPU.Resume();
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            toolStripButtonPause.Enabled = false;
            toolStripButtonResume.Enabled = true;

            Machine.CPU.Pause();
        }

        private void toolStripButtonNext_Click(object sender, EventArgs e)
        {
            toolStripButtonNext.Enabled = false;

            Machine.CPU.NextStep();
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {
            toolStripButtonRun.Enabled = false;
            toolStripButtonSingleStep.Enabled = false;
            toolStripButtonPause.Enabled = true;

            BuildProgram();

            Machine.Run();
        }

        private void BuildProgram()
        {
            if (!string.IsNullOrWhiteSpace(textBoxProgram.Text))
            {
                Machine.CPU.Stop();

                Assembler asm = new Assembler();
                
                MemoryStream ms = new MemoryStream();
                StreamWriter w = new StreamWriter(ms);
                w.Write(textBoxProgram.Text);
                w.Flush();

                ms.Position = 0;

                asm.Read(ms);

                var writer = new DefaultAssemblyWriter(Machine, 0x00400000, 0x10010000);

                asm.Write(writer);
            }
        }

        private void toolStripButtonSingleStep_Click(object sender, EventArgs e)
        {
            toolStripButtonRun.Enabled = false;
            toolStripButtonSingleStep.Enabled = false;
            toolStripButtonResume.Enabled = true;

            Machine.CPU.SingleStep = true;
            Machine.Run();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxProgram.Text = System.IO.File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void textBoxProgram_TextChanged(object sender, EventArgs e)
        {
            toolStripButtonRun.Enabled = true;
        }
    }
}
