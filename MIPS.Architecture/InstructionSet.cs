using System;
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

        public static Dictionary<string, InstructionDefinition> Instructions { get; private set; }

        public static Dictionary<FunctionCode, InstructionDefinition> InstructionsByFunctionCode { get; private set; }

        public static Dictionary<OpCode, InstructionDefinition> InstructionsByOpCode { get; private set; }

        public static Dictionary<BranchCode, InstructionDefinition> InstructionsByBranchCode { get; private set; }

        #region Instructions
        private static InstructionDefinition[] _Instructions = {
                                                                   new InstructionDefinition(
                                                                       "add", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.add
                                                                    ),
                                                                   new InstructionDefinition(
                                                                       "addu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.addu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "and", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.and
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "div", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.div
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "divu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.divu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "jalr", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs
                                                                       }, 
                                                                       FunctionCode.jalr
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "jr", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs
                                                                       }, 
                                                                       FunctionCode.jr
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "mfhi", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd
                                                                       }, 
                                                                       FunctionCode.mfhi
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "mflo", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd
                                                                       }, 
                                                                       FunctionCode.mflo
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "mthi", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                       }, 
                                                                       FunctionCode.mthi
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "mtlo", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                       }, 
                                                                       FunctionCode.mtlo
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "mult", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.mult
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "multu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.multu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "nor", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.nor
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "or", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.or
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sll", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Sa 
                                                                       }, 
                                                                       FunctionCode.sll
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sllv", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs 
                                                                       }, 
                                                                       FunctionCode.sllv
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "slt", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.slt
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sltu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.sltu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sra", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Sa 
                                                                       }, 
                                                                       FunctionCode.sra
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "srav", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs 
                                                                       }, 
                                                                       FunctionCode.srav
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "srl", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Sa 
                                                                       }, 
                                                                       FunctionCode.srl
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "srlv", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs 
                                                                       }, 
                                                                       FunctionCode.srlv
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sub", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.sub
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "subu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.subu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "syscall", 
                                                                       new InstructionArgumentType[] { 
                                                                       }, 
                                                                       FunctionCode.syscall
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "xor", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rd, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt 
                                                                       }, 
                                                                       FunctionCode.xor
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "addi", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.addi
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "addiu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.addiu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "andi", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.andi
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "beq", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Label 
                                                                       }, 
                                                                       OpCode.beq
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "bgtz", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Label 
                                                                       }, 
                                                                       OpCode.bgtz
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "blez", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Label 
                                                                       }, 
                                                                       OpCode.blez
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "bne", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Label 
                                                                       }, 
                                                                       OpCode.bne
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "lb", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt,
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.lb
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "lbu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.lbu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "lh", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.lh
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "lhu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.lhu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "lui", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Immediate
                                                                       }, 
                                                                       OpCode.lui
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "lw", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.lw
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "ori", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.ori
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sb", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.sb
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "slti", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.slti
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sltiu", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.sltiu
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sh", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.sh
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "sw", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.ImmediateRs
                                                                       }, 
                                                                       OpCode.sw
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "xori", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rt, 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Immediate 
                                                                       }, 
                                                                       OpCode.xori
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "bgez", 
                                                                       new InstructionArgumentType[] {
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Label 
                                                                       }, 
                                                                       BranchCode.bgez
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "bltz", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Rs, 
                                                                           InstructionArgumentType.Label 
                                                                       }, 
                                                                       BranchCode.bltz
                                                                    ),
                                                               };
        #endregion

        static InstructionSet()
        {
            Instructions = _Instructions.ToDictionary(i => i.Name);
            InstructionsByFunctionCode = _Instructions.Where(i => i.OpCode == OpCode.Register).ToDictionary(i => i.FunctionCode);
            InstructionsByOpCode = _Instructions.Where(i => i.OpCode != OpCode.Register && i.OpCode != OpCode.Branch).ToDictionary(i => i.OpCode);
            InstructionsByBranchCode = _Instructions.Where(i => i.OpCode == OpCode.Branch).ToDictionary(i => i.BranchCode);
        }
    }
}
