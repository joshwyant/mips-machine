using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class MacroInstructionDefinition
    {
        public delegate void MacroInstructionDelegate(Assembler asm, string[] args);

        public string Name { get; set; }
        public int ArgumentCount { get; set; }
        public MacroInstructionDelegate Delegate { get; set; }

        public MacroInstructionDefinition(string name, int argc, MacroInstructionDelegate d)
        {
            Name = name;
            ArgumentCount = argc;
            Delegate = d;
        }
    }
}
