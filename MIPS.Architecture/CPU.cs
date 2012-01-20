using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class CPU
    {
        /// <summary>
        /// Gets the names of the registers in the Register File
        /// </summary>
        protected static readonly string[] RegisterNames = { "0", "v0", "at", "v1", "a0", "a1", "a2", "a3", "t0", "t1", "t2", "t3", "t4", "t5",
                                                            "t6", "t7", "s0", "s1", "s2", "s3", "s4", "s5", "s6", "s7", "t8", "t9", "k0", "k1", 
                                                            "gp", "sp", "fp", "ra" };

        /// <summary>
        /// Gets the Register File
        /// </summary>
        public uint[] RF { get; protected set; }

        /// <summary>
        /// Gets or sets the machine this CPU is a part of.
        /// </summary>
        public Machine Machine { get; protected set; }

        /// <summary>
        /// Creates a new CPU. This constructor is only called inside the Machine constructor.
        /// </summary>
        internal CPU(Machine machine)
        {
            RF = new uint[32];
            Machine = machine;
        }
    }
}
