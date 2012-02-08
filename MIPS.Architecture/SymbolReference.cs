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
        public ImmediateType Type { get; set; }
    }
}
