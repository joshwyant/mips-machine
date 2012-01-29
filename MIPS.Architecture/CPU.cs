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
        /// The Instruction Register; gets the current instruction. The address is IR*4.
        /// </summary>
        public int IR { get; protected set; }

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
        }

        private void ExecuteRegisterInstruction(Instruction ins)
        {
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
                    throw new NotImplementedException();
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
                    // TODO: Generate exception in case of overflow.
                    break;
                // Subtract
                case FunctionCode.sub:
                    RF[(int)ins.Rd] = unchecked((uint)((int)RF[(int)ins.Rs] - (int)RF[(int)ins.Rt]));
                    // TODO: Generate exception in case of overflow.
                    break;
                // Subtract Unsigned
                case FunctionCode.subu:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] - RF[(int)ins.Rt];
                    // TODO: Generate exception in case of overflow.
                    break;
                // And
                case FunctionCode.and:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] & RF[(int)ins.Rt];
                    break;
                // Or
                case FunctionCode.or:
                    RF[(int)ins.Rd] = RF[(int)ins.Rs] | RF[(int)ins.Rt];
                    break;
                // Exclusive Or,
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
                    throw new NotImplementedException();
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
                    throw new NotImplementedException();
                // Branch if Less Than or Equal to Zero
                case OpCode.blez:
                    throw new NotImplementedException();
                // Branch if Greater than Zero
                case OpCode.bgtz:
                    throw new NotImplementedException();
                // Add Immediate
                case OpCode.addi:
                    throw new NotImplementedException();
                // Add Immediate Unsigned
                case OpCode.addiu:
                    throw new NotImplementedException();
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
                    throw new NotImplementedException();
                // Xor Immediate
                case OpCode.xori:
                    throw new NotImplementedException();
                // Load Upper Immediate
                case OpCode.lui:
                    throw new NotImplementedException();
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
    }
}
