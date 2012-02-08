﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class InstructionSet
    {
        /// <summary>
        /// Gets the names of the registers in the CPU's Register File
        /// </summary>
        public static readonly string[] RegisterNames = { "0", "v0", "at", "v1", "a0", "a1", "a2", "a3", "t0", "t1", "t2", "t3", "t4", "t5",
                                                          "t6", "t7", "s0", "s1", "s2", "s3", "s4", "s5", "s6", "s7", "t8", "t9", "k0", "k1", 
                                                          "gp", "sp", "fp", "ra" };

        
    }
}