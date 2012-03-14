using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace MIPS.Architecture
{
    public class Assembler
    {
        public List<Symbol> Symbols = new List<Symbol>();

        public List<SymbolReference> References = new List<SymbolReference>();

        public AssemblyContext CurrentContext { get; set; }

        public List<Section> Sections = new List<Section>();

        public List<string> Errors = new List<string>();

        public Assembler()
        {
            Sections.AddRange(new[] {
                new Section(".data"),
                new Section(".text")
            });

            SetContext(".text");
        }

        public Section CurrentSection
        {
            get
            {
                return Sections.Where(s => s.Name == CurrentContext.Section).Single();
            }
        }

        public Section GetSection(string name)
        {
            return Sections.Where(s => s.Name == name).Single();
        }

        public Symbol MarkLabel(string name)
        {
            var symbol = new Symbol { 
                Name = name, 
                Offset = (int)CurrentSection.Offset, 
                Section = CurrentSection.Name
            };

            Symbols.Add(symbol);

            return symbol;
        }

        public Symbol MarkLabel()
        {
            var symbol = new Symbol
            {
                Offset = (int)CurrentSection.Offset,
                Section = CurrentSection.Name
            };

            Symbols.Add(symbol);

            return symbol;
        }


        public void MarkLabel(Symbol s)
        {
            s.Offset = (int)CurrentSection.Offset;
            s.Section = CurrentSection.Name;

            Symbols.Add(s);
        }

        public void SetContext(string context)
        {
            CurrentContext = GetSection(context).Context;
        }

        public Symbol DefineExternal(string name)
        {
            var symbol = new Symbol
            {
                Name = name,
                IsExternal = true
            };

            Symbols.Add(symbol);

            return symbol;
        }

        public void Read(Stream stream)
        {
            using (StreamReader sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    ParseLine(sr.ReadLine());
                    currentLine++;
                }
            }
        }

        int currentLine = 0;

        private void ParseLine(string line)
        {
            var snippet = line.Split(new[] {'#'}, 2)[0].Trim(); // Get rid of any comments and trim.

            // Do we have anything meaningful?
            if (string.IsNullOrEmpty(snippet))
                return;

            // Get the label of the snippet.
            if (snippet.Contains(':'))
            {
                var labelName = snippet.Remove(snippet.IndexOf(':')).Trim();
                MarkLabel(labelName);

                snippet = snippet.Substring(snippet.IndexOf(':') + 1).Trim();
            }

            if (snippet.StartsWith("."))
            {
                // Process assembler directives
                ProcessAssemblerDirective(snippet);
            }
            else
            {
                // Process normal instructions - get the instruction and the arguments.
                ProcessInstruction(snippet);
            }
        }

        private void ProcessInstruction(string snippet)
        {
            var parts = snippet.Split(new[] { ' ', '\t' }, 2, StringSplitOptions.RemoveEmptyEntries);
            var name = parts[0].Trim();
            var arg = parts.Length == 1 ? string.Empty : parts[1].Trim();

            var args = arg.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToArray();

            InstructionDefinition def;
            MacroInstructionDefinition mdef;

            if (InstructionSet.Instructions.TryGetValue(name, out def))
            {
                if (def.Arguments.Length != args.Count())
                    throw new ArgumentException("Instruction argument count is invalid.");

                // Create the instruction
                Instruction ins = new Instruction();

                ins.OpCode = def.OpCode;

                if (ins.OpCode == OpCode.Register)
                    ins.FunctionCode = def.FunctionCode;
                else if (ins.OpCode == OpCode.Branch)
                    ins.BranchCode = def.BranchCode;

                // Set the instruction's arguments
                for (int i = 0; i < def.Arguments.Length; i++)
                {
                    switch (def.Arguments[i])
                    {
                        case InstructionArgumentType.Rd:
                            ins.Rd = InstructionSet.GetRegister(args[i]);
                            break;
                        case InstructionArgumentType.Rs:
                            ins.Rs = InstructionSet.GetRegister(args[i]);
                            break;
                        case InstructionArgumentType.Rt:
                            ins.Rt = InstructionSet.GetRegister(args[i]);
                            break;
                        case InstructionArgumentType.Sa:
                            ins.sa = byte.Parse(args[i]);
                            break;
                        case InstructionArgumentType.ImmediateRs:
                            // Match arguments of type "immediate ($register)"
                            Regex regex = new Regex(@"^(\w+)\s*(\$\w+)$");
                            var match = regex.Match(args[i]);

                            if (!match.Success)
                                throw new ArgumentException("Invalid argument.");

                            ins.Rs = InstructionSet.GetRegister(match.Groups[2].Value);
                            // GetImmediate() also takes care of symbol references.
                            ins.Immediate = (ushort)GetImmediate(match.Groups[1].Value, SymbolReferenceType.Immediate);

                            break;
                        case InstructionArgumentType.Immediate:
                        case InstructionArgumentType.Label:
                            // GetImmediate() also takes care of symbol references.
                            ins.Immediate = (ushort)GetImmediate(args[i], SymbolReferenceType.Immediate);
                            break;
                    }
                }

                // Now that the instruction's arguments are encoded, let's emit it into the proper section.
                EmitInstruction(ins);
            }
            else if (InstructionSet.MacroInstructions.TryGetValue(name, out mdef))
            {
                // Call the macro instruction's delegate to process it.
                mdef.Delegate(this, args);
            }
            else
            {
                throw new Exception("Invalid instruction.");
            }
        }

        public void EmitInstruction(Instruction ins)
        {
            CurrentSection.Stream.Write(BitConverter.GetBytes(ins.Encode()), 0, 4);
            CurrentSection.Offset += 4;
        }

        public void EmitInstruction(string name, Symbol symbol, SymbolReferenceType type, params int[] args)
        {
            MarkSymbolReference(symbol, type);

            var def = InstructionSet.Instructions[name];

            var ins = Instruction.FromDefinition(def, args);

            CurrentSection.Stream.Write(BitConverter.GetBytes(ins.Encode()), 0, 4);
            CurrentSection.Offset += 4;
        }

        public void EmitInstruction(string name, string symbol, SymbolReferenceType type, params int[] args)
        {
            MarkSymbolReference(symbol, type);

            var def = InstructionSet.Instructions[name];

            var ins = Instruction.FromDefinition(def, args);

            CurrentSection.Stream.Write(BitConverter.GetBytes(ins.Encode()), 0, 4);
            CurrentSection.Offset += 4;
        }

        public void EmitIndexedInstruction(string name, Register rs, params int[] args)
        {
            var def = InstructionSet.Instructions[name];

            var ins = Instruction.FromDefinition(def, rs, args);

            CurrentSection.Stream.Write(BitConverter.GetBytes(ins.Encode()), 0, 4);
            CurrentSection.Offset += 4;
        }

        public void EmitInstruction(string name, params int[] args)
        {
            var def = InstructionSet.Instructions[name];

            var ins = Instruction.FromDefinition(def, args);

            CurrentSection.Stream.Write(BitConverter.GetBytes(ins.Encode()), 0, 4);
            CurrentSection.Offset += 4;
        }

        public int GetImmediate(string arg, SymbolReferenceType type)
        {
            int val = 0;
            if (arg.StartsWith("0x"))
            {
                val = ushort.Parse(arg.Substring(2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            else if (!int.TryParse(arg, out val))
            {
                // Add a symbol reference
                MarkSymbolReference(arg, type);
            }

            if (type == SymbolReferenceType.ImmediateLower)
                return (int)((ushort)val);
            else if (type == SymbolReferenceType.ImmediateUpper)
                return (int)((ushort)(val >> 16));
            else
                return val;
        }

        public void MarkSymbolReference(string symbol, SymbolReferenceType type)
        {
            References.Add(new SymbolReference(this) { Name = symbol, Type = type });
        }

        public void MarkSymbolReference(Symbol symbol, SymbolReferenceType type)
        {
            References.Add(new SymbolReference(this) { Symbol = symbol, Type = type });
        }

        private void ProcessAssemblerDirective(string directive)
        {
            // If this is just the name of a section, i.e. .text or .data, then set the context.
            if (Sections.Where(s => s.Name == directive).Any())
                SetContext(directive);
            else
            {
                var parts = directive.Split(new[] {' '}, 2);
                var name = parts[0];

                switch (name)
                {
                    case ".asciiz":
                        var str = parts[1].Trim();
                        if (!(str.StartsWith("\"") && str.EndsWith("\"")))
                            throw new ArgumentException("String expected.");

                        str = str.Substring(1, str.Length - 2).Replace(@"\\", "\\").Replace(@"\r", "\r").Replace(@"\n", "\n");

                        CurrentSection.Stream.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                        CurrentSection.Stream.Write(new byte[] { 0 }, 0, 1);
                        CurrentSection.Offset += str.Length + 1;

                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        void Error(string error)
        {
            Errors.Add(string.Format("Error at line {0}: {1}", currentLine, error));
        }

        public void Write(AssemblyWriter writer)
        {
            writer.Write(this);
        }
    }
}
