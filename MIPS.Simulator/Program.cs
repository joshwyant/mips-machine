using System;
using System.Collections.Generic;
using System.Linq;
using MIPS.Architecture;
using MIPS.Debugger;
using System.Windows.Forms;

namespace MIPS.Simulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Console.WriteLine("Hypothetical Machine Simulator based on MIPS, written by Josh Wyant");

            Machine mips = new Machine(32); // Create a new MIPS machine with 32MB of RAM.

            // Map the data and text segments
            mips.Memory.Map(0x00400000, 0x1000FFFC, 0x00800000); // Text segment
            mips.Memory.Map(0x10010000, 0xFFFFFFFC, 0x00810000); // Data segment
            mips.Memory.Map(0x80000000, 0x007FFFFC, 0x00100000); // Video memory

            // Fill the text segment
            // Example: mips.Memory[0] = new Instruction(OpCode.ori, Register.t5, Register.t2, Register.sp).Encode();
            var addr = 0x00400000 >> 2;
            mips.Memory[addr++] = 0x24020004;  // addiu $2,$0,4   
            mips.Memory[addr++] = 0x3c011001;  // lui $1,4097         
            mips.Memory[addr++] = 0x34240000;  // ori $4,$1,0
            mips.Memory[addr++] = 0x0000000c;  // syscall             
            mips.Memory[addr++] = 0x24020005;  // addiu $2,$0,5       
            mips.Memory[addr++] = 0x0000000c;  // syscall             
            mips.Memory[addr++] = 0x1840000c;  // blez $2,12          
            mips.Memory[addr++] = 0x24080000;  // addiu $8,$0,0       
            mips.Memory[addr++] = 0x01024020;  // add $8,$8,$2        
            mips.Memory[addr++] = 0x2042ffff;  // addi $2,$2,-1       
            mips.Memory[addr++] = 0x1440fffd;  // bne $2,$0,-3        
            mips.Memory[addr++] = 0x24020004;  // addiu $2,$0,4       
            mips.Memory[addr++] = 0x3c011001;  // lui $1,4097         
            mips.Memory[addr++] = 0x34240021;  // ori $4, $1, $33
            mips.Memory[addr++] = 0x0000000c;  // syscall             
            mips.Memory[addr++] = 0x24020001;  // addiu $2,$0,1       
            mips.Memory[addr++] = 0x00082021;  // addu $4,$0,$8       
            mips.Memory[addr++] = 0x0000000c;  // syscall             
            mips.Memory[addr++] = 0x0401ffed;  // bgez $0,-19         
            mips.Memory[addr++] = 0x24020004;  // addiu $2,$0,4       
            mips.Memory[addr++] = 0x3c011001;  // lui $1,4097         
            mips.Memory[addr++] = 0x34240049;  // ori $4,$1,73
            mips.Memory[addr++] = 0x0000000c;  // syscall             
            mips.Memory[addr++] = 0x2402000a;  // addiu $2,$0,10      
            mips.Memory[addr++] = 0x0000000c;  // syscall             
       
            // Fill the data segment
            addr = 0x10010000 >> 2;
            mips.Memory[addr++] = 0x6c50200a;
            mips.Memory[addr++] = 0x65736165;
            mips.Memory[addr++] = 0x706e6920;
            mips.Memory[addr++] = 0x61207475;
            mips.Memory[addr++] = 0x6c617620;
            mips.Memory[addr++] = 0x66206575;
            mips.Memory[addr++] = 0x4e20726f;
            mips.Memory[addr++] = 0x20203d20;
            mips.Memory[addr++] = 0x65685400;
            mips.Memory[addr++] = 0x6d757320;
            mips.Memory[addr++] = 0x20666f20;
            mips.Memory[addr++] = 0x20656874;
            mips.Memory[addr++] = 0x65746e69;
            mips.Memory[addr++] = 0x73726567;
            mips.Memory[addr++] = 0x6f726620;
            mips.Memory[addr++] = 0x2031206d;
            mips.Memory[addr++] = 0x4e206f74;
            mips.Memory[addr++] = 0x20736920;
            mips.Memory[addr++] = 0x2a200a00;
            mips.Memory[addr++] = 0x202a2a2a;
            mips.Memory[addr++] = 0x6f696441;
            mips.Memory[addr++] = 0x6d412073;
            mips.Memory[addr++] = 0x206f6769;
            mips.Memory[addr++] = 0x6148202d;
            mips.Memory[addr++] = 0x61206576;
            mips.Memory[addr++] = 0x6f6f6720;
            mips.Memory[addr++] = 0x61642064;
            mips.Memory[addr++] = 0x2a2a2079;
            mips.Memory[addr++] = 0x00002a2a;

            // Set the Program Counter
            unsafe
            {
                mips.CPU.PC = (uint*)0x00400000;
            }

            Application.EnableVisualStyles();
            Application.Run(new DebugForm(mips));
        }
    }
}
