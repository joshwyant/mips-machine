using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MIPS.Architecture;

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
        }

        void CPU_CPUStep(object sender, EventArgs e)
        {
            listViewRegisters.Clear();

            for (int i = 0; i < 32; i++)
            {
                unsafe
                {
                    listViewRegisters.Items.Add(new ListViewItem(new[] { Architecture.InstructionSet.RegisterNames[i], Machine.CPU.RF[i].ToString("X") }));
                }
            }
        }

        private void toolStripButtonResume_Click(object sender, EventArgs e)
        {
            Machine.CPU.SingleStep = true;
            Machine.Run();
        }
    }
}
