using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class InstructionDefinition
    {
        public string Name { get; set; }
        public InstructionArgumentType[] Arguments { get; set; }
        public OpCode OpCode { get; set; }
        public FunctionCode FunctionCode { get; set; }
        public BranchCode BranchCode { get; set; }

        public InstructionDefinition(string name, InstructionArgumentType[] args, FunctionCode functionCode)
        {
            Name = name;
            Arguments = args;
            OpCode = OpCode.Register;
            FunctionCode = functionCode;
        }

        public InstructionDefinition(string name, InstructionArgumentType[] args, OpCode opCode)
        {
            Name = name;
            Arguments = args;
            OpCode = opCode;
        }

        public InstructionDefinition(string name, InstructionArgumentType[] args, BranchCode branchCode)
        {
            Name = name;
            Arguments = args;
            OpCode = OpCode.Branch;
            BranchCode = branchCode;
        }

        /// <summary>
        /// Formats an instruction argument based on the argument type.
        /// </summary>
        internal static string GetArg(InstructionArgumentType arg, Instruction ins)
        {
            switch (arg)
            {
                case InstructionArgumentType.Rs:
                    return "$" + InstructionSet.RegisterNames[ins.Rs];
                case InstructionArgumentType.Rt:
                    return "$" + InstructionSet.RegisterNames[ins.Rt];
                case InstructionArgumentType.Rd:
                    return "$" + InstructionSet.RegisterNames[ins.Rd];
                case InstructionArgumentType.Immediate:
                case InstructionArgumentType.Label:
                    if (ins.OpCode == OpCode.j || ins.OpCode == OpCode.jal)
                        return "0x" + (ins.Target << 2).ToString("X");
                    else
                        return "0x" + ins.Immediate.ToString("X");
                case InstructionArgumentType.ImmediateRs:
                    return string.Format("{1}(${0})", InstructionSet.RegisterNames[ins.Rs], ins.Immediate);
                case InstructionArgumentType.Sa:
                    return ins.sa.ToString();
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Gets the instruction definition for an instruction.
        /// </summary>
        public static InstructionDefinition ForInstruction(Instruction ins)
        {
            if (ins.OpCode == OpCode.Register)
            {
                return InstructionSet.InstructionsByFunctionCode[ins.FunctionCode];
            }
            else
            {
                if (ins.OpCode != OpCode.Branch)
                    return InstructionSet.InstructionsByOpCode[ins.OpCode];
                else
                    return InstructionSet.InstructionsByBranchCode[ins.BranchCode];
            }
        }

        /// <summary>
        /// Gets the string representation of an instruction.
        /// </summary>
        public static string GetString(Instruction ins)
        {
            if (ins.OriginalInstruction.HasValue && ins.OriginalInstruction.Value == 0)
            {
                return "null";
            }

            // Get the instruction definition for this instruction
            var def = InstructionDefinition.ForInstruction(ins);

            // Join all the arguments together, calling GetArg on each argument.
            var args = string.Join(", ", def.Arguments.Select(arg => GetArg(arg, ins)));

            // Format the instruction.
            return string.Format("{0} {1}", def.Name, args).Trim();
        }
    }
}
