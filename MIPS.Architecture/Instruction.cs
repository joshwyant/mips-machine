using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public struct Instruction
    {
        /// <summary>
        /// The OpCode of the instruction.
        /// </summary>
        public OpCode OpCode;

        /// <summary>
        /// The first source operand register for register- or immediate-format instructions.
        /// </summary>
        public Register Rs;

        /// <summary>
        /// The second source operand register for register- or immediate-format instructions.
        /// </summary>
        public Register Rt;

        public BranchCode BranchCode { get { return (BranchCode)Rt; } set { Rt = (Register)value; } }

        /// <summary>
        /// The destination register for a register-based instruction.
        /// </summary>
        public Register Rd;

        /// <summary>
        /// The immediate word.
        /// </summary>
        public ushort Immediate;

        /// <summary>
        /// The shift amount for shift instructions.
        /// </summary>
        public byte sa;

        /// <summary>
        /// The function code for a register-based instuction.
        /// </summary>
        public FunctionCode FunctionCode;

        /// <summary>
        /// The target for jump instructions.
        /// </summary>
        public int Target;

        public uint Encode()
        {
            // Encode the OpCode in the instruction word.
            uint instruction = (uint)OpCode << 26;

            if (((int)OpCode & 0x3E) != 2)
            {
                // This is not a jump instruciton, so there are source registers.
                instruction |= ((uint)Rt << 16) | ((uint)Rs << 21);

                // If this is a register format instruction, there is a destination register and a function code.
                if (OpCode == OpCode.Register)
                {
                    instruction |= (uint)FunctionCode | ((uint)Rd << 11);

                    // If this is a shift instruction, there is also a shift amount field.
                    if ((int)OpCode < 4)
                    {
                        instruction |= (uint)sa << 6;
                    }
                }
                else
                {
                    // If this is not a register format insruction, there is also an immediate half-word.
                    instruction |= (uint)Immediate;
                }

            }
            else
            {
                // If this is a jump instruction (OpCode = 00001?b), encode the target
                instruction |= (uint)Target & 0x3FFFFFF;
            }

            return instruction;
        }

        public Instruction(uint word)
        {
            // Initialize the structure.
            Rs = 0;
            Rt = 0;
            Rd = 0;
            Immediate = 0;
            sa = 0;
            FunctionCode = 0;
            Target = 0;

            // Get the instruction's OpCode.
            OpCode = (OpCode)(word >> 26);

            if (((int)OpCode & 0x3E) != 2)
            {
                // This is not a jump instruciton, so there are source registers.
                Rt = (Register)((word >> 16) & 0x1F);
                Rs = (Register)((word >> 21) & 0x1F);

                // If this is a register format instruction, there is a destination register and a function code.
                if (OpCode == OpCode.Register)
                {
                    FunctionCode = (FunctionCode)(word & 0x3F);
                    Rd = (Register)((word >> 11) & 0x1F);

                    // If this is a shift instruction, there is also a shift amount field.
                    if ((int)OpCode < 4)
                    {
                        sa = (byte)((word >> 6) & 0x1F);
                    }
                }
                else
                {
                    // If this is not a register format insruction, there is also an immediate half-word.
                    Immediate = (ushort)word;
                }
            }
            else
            {
                // If this is a jump instruction (OpCode = 00001?b), encode the target
                Target = (int)word & 0x3FFFFFF;

                if ((word & 0x2000000) != 0)
                {
                    // If this is a negative branch instruction, so make the integer negative.
                    Target = unchecked((int)((uint)Target | 0xFC000000));
                }
            }
        }

        public Instruction(OpCode opCode, Register rs, Register rt, Register rd)
        {
            OpCode = opCode;
            Rs = rs;
            Rt = rt;
            Rd = rd;
            Immediate = 0;
            sa = 0;
            FunctionCode = 0;
            Target = 0;
        }

        public Instruction(OpCode opCode, Register rs, Register rd)
        {
            OpCode = opCode;
            Rs = rs;
            Rt = 0;
            Rd = rd;
            sa = 0;
            Immediate = 0;
            FunctionCode = 0;
            Target = 0;
        }

        public Instruction(FunctionCode shiftCode, Register rt, Register rd, int shiftAmount)
        {
            OpCode = OpCode.Register;
            Rs = 0;
            Rt = rt;
            Rd = rd;
            sa = (byte)shiftAmount;
            Immediate = 0;
            FunctionCode = shiftCode;
            Target = 0;
        }

        public Instruction(FunctionCode functionCode, Register rs, Register rt, ushort immediate)
        {
            OpCode = OpCode.Register;
            Rs = rs;
            Rt = rt;
            Rd = 0;
            sa = 0;
            Immediate = immediate;
            FunctionCode = functionCode;
            Target = 0;
        }

        public Instruction(FunctionCode functionCode, Register rs, ushort immediate)
        {
            OpCode = OpCode.Register;
            Rs = rs;
            Rt = 0;
            Rd = 0;
            sa = 0;
            Immediate = immediate;
            FunctionCode = functionCode;
            Target = 0;
        }

        public Instruction(FunctionCode functionCode, Register rs, Register rt)
        {
            OpCode = OpCode.Register;
            Rs = rs;
            Rt = rt;
            Rd = 0;
            Immediate = 0;
            sa = 0;
            FunctionCode = functionCode;
            Target = 0;
        }

        public Instruction(FunctionCode functionCode, Register rs)
        {
            OpCode = OpCode.Register;
            Rs = rs;
            Rt = 0;
            Rd = 0;
            Immediate = 0;
            sa = 0;
            FunctionCode = functionCode;
            Target = 0;
        }

        public Instruction(FunctionCode functionCode, ushort immediate)
        {
            OpCode = OpCode.Register;
            Rs = 0;
            Rt = 0;
            Rd = 0;
            Immediate = immediate;
            sa = 0;
            FunctionCode = functionCode;
            Target = 0;
        }

        public Instruction(OpCode jumpCode, int target)
        {
            OpCode = jumpCode;
            Rs = 0;
            Rt = 0;
            Rd = 0;
            Immediate = 0;
            sa = 0;
            FunctionCode = 0;
            Target = target;
        }
    }
}
