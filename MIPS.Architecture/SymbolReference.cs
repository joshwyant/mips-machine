using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class SymbolReference
    {
        public string Name { get; set; }
        public string Section { get; set; }
        public int Offset { get; set; }
        public SymbolReferenceType Type { get; set; }

        public SymbolReference(Assembler asm)
        {
            Section = asm.CurrentSection.Name;
            Offset = asm.CurrentSection.Offset;
        }
    }
}
