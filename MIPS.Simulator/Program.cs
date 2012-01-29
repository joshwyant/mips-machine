using System;
using System.Collections.Generic;
using System.Linq;
using MIPS.Architecture;

namespace MIPS.Simulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Machine mips = new Machine(32); // Create a new MIPS machine with 32MB of RAM.

            // mips.Memory[0] = new Instruction(OpCode.ori, Register.t5, Register.t2, Register.sp).Encode();

            mips.Run();

            Console.WriteLine("Hypothetical Machine Simulator based on MIPS, written by Josh Wyant");

            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
