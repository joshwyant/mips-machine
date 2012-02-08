using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class Symbol
    {
        public string Name { get; set; }
        public string Section { get; set; }
        public int Offset { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsExternal { get; set; }
    }
}
