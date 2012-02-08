using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class CPU
    {
        /// <summary>
        /// Gets the Register File
        /// </summary>
        public uint[] RF { get; protected set; }

        /// <summary>
        /// The Instruction Register; gets or sets the current instruction. The address is IR*4.
        /// </summary>
        public int IR { get; set; }

        /// <summary>
        /// Gets or sets the high word of an arithmetic operation result.
        /// </summary>
        public uint High { get; set; }

        /// <summary>
        /// Gets or sets the low word of an arithmetic operation result.
        /// </summary>
        public uint Low { get; set; }

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

        internal void Start()
        {
            while (true)
            {
                Step();
            }
        }

        private void Step()
        {
            // Fetch the next instruction
            var ins = new Instruction(Machine.Memory[IR++]);

            if (ins.OpCode == OpCode.Register)
            {
                ExecuteRegisterInstruction(ins);
            }
            else if (ins.OpCode == OpCode.j || ins.OpCode == OpCode.jal)
            {
                throw new NotImplementedException();
            }
            else
            {
                ExecuteImmediateInstruction(ins);
            }
        }

        private void ExecuteRegisterInstruction(Instruction ins)
        {
            // Register format instructions have Op-Code "0" and a Function Code.
            switch (ins.FunctionCode)
            {
                // Shift Left Logical
                case FunctionCode.sll:
                    RF[(int)ins.Rd] = RF[(int)ins.Rt] << ins.sa;
                    break;
                // Shift Right Logical
                case FunctionCode.srl:
                    RF[(int)ins.Rd] = RF[(int)ins.Rt] >> ins.sa;
                    break;
                // Shift Right Arithmetic
                case FunctionCode.sra:
                    RF[(int)ins.Rd] = unchecked((uint)((int)RF[(int)ins.Rt] >> ins.sa));
                    break;
                // Shift Left Logical Variable
                case FunctionCode.sllv:
                    RF[(int)ins.Rd] = RF[(int)ins.Rt] << (int)RF[(int)ins.Rs];
                    break;
                // Shift Right Logical Variable
                case FunctionCode.srlv:
                    RF[(int)ins.Rd] = RF[(int)ins.Rt] >> (int)RF[(int)ins.Rs];
                    break;
                // Shift Right Arithmetic Variable
                case FunctionCode.srav:
                    RF[(int)ins.Rd] = unchecked((uint)((int)RF[(int)ins.Rt] >> (int)RF[(int)ins.Rs]));
                    break;
                // Jump Register
                case FunctionCode.jr:
                    throw new NotImplementedException();
                // Jump and Link Register
                case FunctionCode.jalr:
                    throw new NotImplementedException();
                // System Call
                case FunctionCode.syscall:
                    // TODO: For now, all syscalls are simulated here.
                    Syscall();
                    break;
                // Move From High
                case FunctionCode.mfhi:
                    RF[(int)ins.Rd] = High;
                    break;
                // Move to High
                case FunctionCode.mthi:
                    High = RF[(int)ins.Rs];
                    break;
                // Move From Low
                case FunctionCode.mflo:
                    RF[(int)ins.Rd] = Low;
                    break;
                // Move To Low
                case FunctionCode.mtlo:
                    Low = RF[(int)ins.Rs];
                    break;
                // Multiply
                case FunctionCode.mult:
                    {
                        long result = (long)RF[(int)ins.Rs] * (long)RF[(int)ins.Rt];
                        Low = (uint)result;
                        High = (uint)(result >> 32);
                        break;
                    }
                // Multiply Unsigned
                case FunctionCode.multu:
                    {
                        ulong result = (ulong)RF[(int)ins.Rs] * (ulong)RF[(int)ins.Rt];
                        Low = (uint)result;
                        High = (uint)(result >> 32);
                        break;
                    }
                // Divide
                case FunctionCode.div:
                    {
                        long result;
                        Low = (uint)Math.DivRem((long)unchecked((uint)RF[(int)ins.Rs]), unchecked((int)(long)RF[(int)ins.Rs]), out result);
                        High = (uint)result;
                        break;
                    }
                // Divide Unsigned
                case FunctionCode.divu:
                    {
                        long result;
                        Low = (uint)Math.DivRem((long)RF[(int)ins.Rs], (long)RF[(int)ins.Rs], out result);
                        High = (uint)result;
                        break;
                    }
                // Add
                case FunctionCode.add:
                    RF[(int)ins.Rd] = unchecked((uint)((int)RF[(int)ins.Rs] + (int)RF[(int)ins.Rt]));
                    // TODO: Generate exception in case of overflow.
                    break;
                // Add Unsigned
                case FunctionCode.addu:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] + RF[(int)ins.Rt];
                    break;
                // Subtract
                case FunctionCode.sub:
                    RF[(int)ins.Rd] = unchecked((uint)((int)RF[(int)ins.Rs] - (int)RF[(int)ins.Rt]));
                    break;
                // Subtract Unsigned
                case FunctionCode.subu:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] - RF[(int)ins.Rt];
                    break;
                // And
                case FunctionCode.and:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] & RF[(int)ins.Rt];
                    break;
                // Or
                case FunctionCode.or:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] | RF[(int)ins.Rt];
                    break;
                // Exclusive Or
                case FunctionCode.xor:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] ^ RF[(int)ins.Rt];
                    break;
                // Nor
                case FunctionCode.nor:
                    RF[(int)ins.Rd] = ~(RF[(int)ins.Rs] | RF[(int)ins.Rt]);
                    break;
                // Set on Less Than
                case FunctionCode.slt:
                    throw new NotImplementedException();
                // Set on Less Than Unsigned
                case FunctionCode.sltu:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        private void ExecuteImmediateInstruction(Instruction ins)
        {
            switch (ins.OpCode)
            {
                // The default Op-Code that deals with registers and is defined by a function code.
                case OpCode.Register:
                    throw new NotImplementedException();
                // Special branches with a branch code.
                case OpCode.Branch:
                    switch (ins.BranchCode)
                    {
                        // Branch if Greater Than or Equal to Zero
                        case BranchCode.bgez:
                            if ((int)RF[(int)ins.Rs] >= 0)
                                IR += unchecked((int)ins.SignExtendedImmediate);
                            break;
                        // Branch if Greater Than or Equal to Zero, and Link
                        case BranchCode.bgezal:
                            throw new NotImplementedException();
                        // Branch if Less than Zero
                        case BranchCode.bltz:
                            throw new NotImplementedException();
                        // Branch if Less than Zero, and Link
                        case BranchCode.bltzal:
                            throw new NotImplementedException();
                        default:
                            // TODO: Invalid expression instruction?
                            throw new NotImplementedException();
                    }
                    break;
                // The Jump instruction
                case OpCode.j:
                    throw new NotImplementedException();
                // The Jump and Link (call procedure) instruction
                case OpCode.jal:
                    throw new NotImplementedException();
                // Branch if Equal
                case OpCode.beq:
                    throw new NotImplementedException();
                // Branch if Not Equal
                case OpCode.bne:
                    if (RF[(int)ins.Rs] != RF[(int)ins.Rt])
                        IR += unchecked((int)ins.SignExtendedImmediate);
                    break;
                // Branch if Less Than or Equal to Zero
                case OpCode.blez:
                    if ((int)RF[(int)ins.Rs] <= 0)
                        IR += unchecked((int)ins.SignExtendedImmediate);
                    break;
                // Branch if Greater than Zero
                case OpCode.bgtz:
                    throw new NotImplementedException();
                // Add Immediate
                case OpCode.addi:
                    // TODO: Overflow exception
                    RF[(int)ins.Rt] = RF[(int)ins.Rs] + ins.SignExtendedImmediate;
                    break;
                // Add Immediate Unsigned
                case OpCode.addiu:
                    RF[(int)ins.Rt] = RF[(int)ins.Rs] + ins.SignExtendedImmediate;
                    break;
                // Set on Less Than Immediate
                case OpCode.slti:
                    throw new NotImplementedException();
                // Set on Less Than Immediate Unsigned
                case OpCode.sltiu:
                    throw new NotImplementedException();
                // And Immediate
                case OpCode.andi:
                    throw new NotImplementedException();
                // Or Immediate
                case OpCode.ori:
                    RF[(int)ins.Rt] = RF[(int)ins.Rs] | ins.Immediate;
                    break;
                // Xor Immediate
                case OpCode.xori:
                    throw new NotImplementedException();
                // Load Upper Immediate
                case OpCode.lui:
                    RF[(int)ins.Rt] = (uint)ins.Immediate << 16;
                    break;
                // Misc. System Instructions
                case OpCode.System:
                    throw new NotImplementedException();
                // Load Byte
                case OpCode.lb:
                    throw new NotImplementedException();
                // Load Halfword
                case OpCode.lh:
                    throw new NotImplementedException();
                // Load Word Left
                case OpCode.lwl:
                    throw new NotImplementedException();
                // Load Word
                case OpCode.lw:
                    throw new NotImplementedException();
                // Load Byte Unsigned
                case OpCode.lbu:
                    throw new NotImplementedException();
                // Load Halfword Unsigned
                case OpCode.lhu:
                    throw new NotImplementedException();
                // Load Word Right
                case OpCode.lwr:
                    throw new NotImplementedException();
                // Store Byte
                case OpCode.sb:
                    throw new NotImplementedException();
                // Store Halfword
                case OpCode.sh:
                    throw new NotImplementedException();
                // Store Word Left
                case OpCode.swl:
                    throw new NotImplementedException();
                // Store Word
                case OpCode.sw:
                    throw new NotImplementedException();
                // Store Word Right
                case OpCode.swr:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        private void Syscall()
        {
            switch (RF[(int)Register.v0])
            {
                // Print Integer, a0 = number to be printed
                case 1:
                    Console.Write((int)RF[(int)Register.a0]);
                    break;
                // Print Float, a0 = number to be printed
                case 2:
                    throw new NotImplementedException();
                // Print Double, a0 = number to be printed
                case 3:
                    throw new NotImplementedException();
                // Print String, a0 = address of string in memory
                case 4:
                    int addr = (int)RF[(int)Register.a0];
                    int offset = addr % 4;
                    addr >>= 2;
                    while (true)
                    {
                        uint word = Machine.Memory[addr];
                        byte b = 0;
                        while (offset < 4)
                        {
                            b = (byte)(word >> (offset * 8));
                            if (b == 0)
                                break;
                            if (b == 10)
                                Console.WriteLine();
                            else
                                Console.Write((char)b);
                            offset++;
                        }
                        offset = 0;
                        if (b == 0)
                            break;
                        addr++;
                    }
                    break;
                // Read Integer, Number returned in v0
                case 5:
                    RF[(int)Register.v0] = (uint)int.Parse(Console.ReadLine());
                    break;
                // Read Float, Number returned in f0
                case 6:
                    throw new NotImplementedException();
                // Read Double, Number returned in f0
                case 7:
                    throw new NotImplementedException();
                // Read String, a0 = address of input buffer in memory, a1 = length of buffer (n)
                case 8:
                    throw new NotImplementedException();
                // Sbrk, a0 = amount, address returned in v0
                case 9:
                    throw new NotImplementedException();
                // Exit
                case 10:
                    Console.Write("\r\nPress any key to continue...");
                    Console.ReadKey(true);
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
