using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class Machine
    {
        /// <summary>
        /// Gets or sets an array of 32-bit integers representing the machine's memory.
        /// </summary>
        public MMU Memory { get; protected set; }

        public CPU CPU { get; protected set; }

        public Machine(int memoryInMb)
        {
            CPU = new CPU(this);
            Memory = new MMU(memoryInMb);
        }

        public void Run()
        {
            CPU.Start();
        }
    }
}
