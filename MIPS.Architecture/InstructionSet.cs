using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MIPS.Architecture
{
    public class InstructionSet
    {
        /// <summary>
        /// Gets the names of the registers in the CPU's Register File
        /// </summary>
        public static readonly string[] RegisterNames = { "zero", "at", "v0", "v1", "a0", "a1", "a2", "a3", "t0", "t1", "t2", "t3", "t4", "t5",
                                                          "t6", "t7", "s0", "s1", "s2", "s3", "s4", "s5", "s6", "s7", "t8", "t9", "k0", "k1", 
                                                          "gp", "sp", "fp", "ra" };

        #region Instructions
        public static Dictionary<string, InstructionDefinition> Instructions = new[] {
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
                                                                    new InstructionDefinition(
                                                                       "j", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Immediate
                                                                       }, 
                                                                       OpCode.j
                                                                    ),
                                                                    new InstructionDefinition(
                                                                       "jal", 
                                                                       new InstructionArgumentType[] { 
                                                                           InstructionArgumentType.Immediate
                                                                       }, 
                                                                       OpCode.jal
                                                                    ),
                                                               }.ToDictionary(id => id.Name);
        #endregion

        #region Macro Instructions
        public static Dictionary<string, MacroInstructionDefinition> MacroInstructions = new[] {
            // move
            new MacroInstructionDefinition(
                    "move", 2,
                    (asm, args) => {
                        var Rd = (int)InstructionSet.GetRegister(args[0]);
                        var Rs = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("addu", Rd, (int)Register.zero, Rs);
                    }
                ),
            // negate
            new MacroInstructionDefinition(
                    "neg", 2,
                    (asm, args) => {
                        var Rd = (int)InstructionSet.GetRegister(args[0]);
                        var Rs = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("sub", Rd, (int)Register.zero, Rs);
                    }
                ),
            // bitwise not
            new MacroInstructionDefinition(
                    "not", 2,
                    (asm, args) => {
                        var Rd = (int)InstructionSet.GetRegister(args[0]);
                        var Rs = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("nor", Rd, Rs, (int)Register.zero);
                    }
                ),
            // absolute value
            new MacroInstructionDefinition(
                    "abs", 2,
                    (asm, args) => {
                        Symbol skip = new Symbol();
                        var Rd = (int)InstructionSet.GetRegister(args[0]);
                        var Rs = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("addu", Rd, (int)Register.zero, Rs);
                        asm.EmitInstruction("bgez", skip, SymbolReferenceType.Immediate, Rs, 0);
                        asm.EmitInstruction("sub", Rd, (int)Register.zero, Rs);
                        asm.MarkLabel(skip);
                    }
                ),
            // load immediate
            new MacroInstructionDefinition(
                    "li", 2,
                    (asm, args) => {
                        var Rt = (int)InstructionSet.GetRegister(args[0]);
                        var upper = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateUpper);
                        asm.EmitInstruction("lui", (int)Register.at, upper);
                        var lower = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateLower);
                        asm.EmitInstruction("ori", Rt, (int)Register.at, lower);
                    }
                ),
            // load address
            new MacroInstructionDefinition(
                    "la", 2,
                    (asm, args) => {
                        var Rt = (int)InstructionSet.GetRegister(args[0]);
                        var upper = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateUpper);
                        asm.EmitInstruction("lui", (int)Register.at, upper);
                        var lower = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateLower);
                        asm.EmitInstruction("ori", Rt, (int)Register.at, lower);
                    }
                ),
            // load word at label
            new MacroInstructionDefinition(
                    "lw", 2,
                    (asm, args) => {
                        var Rt = (int)InstructionSet.GetRegister(args[0]);
                        var upper = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateUpper);
                        asm.EmitInstruction("lui", (int)Register.at, upper);
                        var lower = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateLower);
                        asm.EmitIndexedInstruction("ori", Register.at, Rt, lower);
                    }
                ),
            // store word at label
            new MacroInstructionDefinition(
                    "sw", 2,
                    (asm, args) => {
                        var Rt = (int)InstructionSet.GetRegister(args[0]);
                        var upper = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateUpper);
                        asm.EmitInstruction("lui", (int)Register.at, upper);
                        var lower = asm.GetImmediate(args[1], SymbolReferenceType.ImmediateLower);
                        asm.EmitIndexedInstruction("sw", Register.at, Rt, lower);
                    }
                ),
            // branch on less than
            new MacroInstructionDefinition(
                    "blt", 3,
                    (asm, args) => {
                        var Rs = (int)InstructionSet.GetRegister(args[0]);
                        var Rt = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("slt", (int)Register.at, Rs, Rt);
                        var label = asm.GetImmediate(args[2], SymbolReferenceType.Immediate);
                        asm.EmitInstruction("bne", (int)Register.at, (int)Register.zero, label);
                    }
                ),
            // branch on less than or equal
            new MacroInstructionDefinition(
                    "ble", 3,
                    (asm, args) => {
                        var Rs = (int)InstructionSet.GetRegister(args[0]);
                        var Rt = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("slt", (int)Register.at, Rt, Rs);
                        var label = asm.GetImmediate(args[2], SymbolReferenceType.Immediate);
                        asm.EmitInstruction("beq", (int)Register.at, (int)Register.zero, label);
                    }
                ),
            // branch on greater than
            new MacroInstructionDefinition(
                    "bgt", 3,
                    (asm, args) => {
                        var Rs = (int)InstructionSet.GetRegister(args[0]);
                        var Rt = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("slt", (int)Register.at, Rt, Rs);
                        var label = asm.GetImmediate(args[2], SymbolReferenceType.Immediate);
                        asm.EmitInstruction("bne", (int)Register.at, (int)Register.zero, label);
                    }
                ),
            // branch on greater than or equal
            new MacroInstructionDefinition(
                    "bge", 3,
                    (asm, args) => {
                        var Rs = (int)InstructionSet.GetRegister(args[0]);
                        var Rt = (int)InstructionSet.GetRegister(args[1]);
                        asm.EmitInstruction("slt", (int)Register.at, Rs, Rt);
                        var label = asm.GetImmediate(args[2], SymbolReferenceType.Immediate);
                        asm.EmitInstruction("beq", (int)Register.at, (int)Register.zero, label);
                    }
                ),
        }.ToDictionary(mid => mid.Name);
        #endregion

        public static Dictionary<FunctionCode, InstructionDefinition> InstructionsByFunctionCode { get; private set; }

        public static Dictionary<OpCode, InstructionDefinition> InstructionsByOpCode { get; private set; }

        public static Dictionary<BranchCode, InstructionDefinition> InstructionsByBranchCode { get; private set; }

        static InstructionSet()
        {
            InstructionsByFunctionCode = Instructions.Values.Where(i => i.OpCode == OpCode.Register).ToDictionary(i => i.FunctionCode);
            InstructionsByOpCode = Instructions.Values.Where(i => i.OpCode != OpCode.Register && i.OpCode != OpCode.Branch).ToDictionary(i => i.OpCode);
            InstructionsByBranchCode = Instructions.Values.Where(i => i.OpCode == OpCode.Branch).ToDictionary(i => i.BranchCode);
        }

        public static int GetRegister(string register)
        {
            Regex regex = new Regex(@"^\$(\w+)$");
            var match = regex.Match(register);

            if (!match.Success)
                throw new ArgumentException("Register expected.");

            var registerName = match.Groups[1].Value;

            int index;
            index = Array.IndexOf<string>(InstructionSet.RegisterNames, registerName);
            if (index == -1)
                throw new ArgumentException("Invalid register name.");

            return index;
        }
    }
}
