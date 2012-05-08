using System;
using System.Collections.Generic;
using System.Linq;
using MIPS.Architecture;
using MIPS.Debugger;
using System.Windows.Forms;

namespace MIPS.Simulator
{
    static class Program
    {
        const string path = @"..\..\..\gcc-bin";
        const string prefix = "mips-sde-elf";
        const string program = "program.elf";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create a new MIPS machine with 32MB of RAM.
            Machine mips = new Machine(32);

            Application.EnableVisualStyles();
            Application.Run(new DebugForm(mips, null));
        }
    }
}
