using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MIPS.Architecture
{
    public class Section
    {
        public string Name { get; set; }
        public AssemblyContext Context { get; set; }
        public Stream Stream { get; set; }
        public int Offset { get; set; }

        public Section(string name)
        {
            Name = name;
            Context = new AssemblyContext { Section = name };
            Stream = new MemoryStream();
            Offset = 0;
        }
    }
}
