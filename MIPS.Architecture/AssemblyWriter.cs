using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public abstract class AssemblyWriter
    {
        public abstract void Write(Assembler assembler);
    }
}
