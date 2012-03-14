using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class DefaultAssemblyWriter : AssemblyWriter
    {
        public Machine Machine { get; set; }
        public uint TextSection { get; set; }
        public uint DataSection { get; set; }

        Assembler asm;

        public DefaultAssemblyWriter(Machine machine, uint textSection, uint dataSection)
        {
            Machine = machine;
            TextSection = textSection;
            DataSection = dataSection;
        }

        public override void Write(Assembler asm)
        {
            this.asm = asm;

            // Write the text and data sections to memory.
            WriteSection(asm.GetSection(".text"), TextSection);
            WriteSection(asm.GetSection(".data"), DataSection);

            // Resolve references.
            ResolveReferences();

            // Start executing at the entry point.
            unsafe
            {
                var main = asm.Symbols.Where(s => s.IsGlobal && s.Section == ".text" && s.Name == "main").SingleOrDefault();
                Machine.CPU.PC = (uint*)(TextSection + (main != null ? main.Offset : 0));
            }
        }

        private void ResolveReferences()
        {
            foreach (var reference in asm.References)
            {
                // First, find the symbol and calculate its location in memory.
                var symbol = reference.Symbol;
                
                if (symbol == null)
                    symbol = asm.Symbols.Where(s => s.Name == reference.Name).SingleOrDefault();

                if (symbol == null)
                    throw new Exception("Unresolved reference.");

                if (symbol.IsExternal)
                    throw new NotImplementedException();

                uint symbolOffset = 0;
                uint referenceOffset = 0;

                // There are only 2 secions: .text and .data.
                switch (symbol.Section)
                {
                    case ".text":
                        symbolOffset = TextSection;
                        break;
                    case ".data":
                        symbolOffset = DataSection;
                        break;
                }
                symbolOffset += (uint)symbol.Offset;

                switch (reference.Section)
                {
                    case ".text":
                        referenceOffset = TextSection;
                        break;
                    case ".data":
                        referenceOffset = DataSection;
                        break;
                }
                referenceOffset += (uint)reference.Offset;

                // Now, insert the referenced symbol.
                // First, determine the value we're inserting.
                uint val = 0;
                switch (reference.Type)
                {
                    case SymbolReferenceType.Immediate:
                    case SymbolReferenceType.ImmediateLower:
                    case SymbolReferenceType.Jump:
                        val = symbolOffset;
                        break;
                    case SymbolReferenceType.ImmediateUpper:
                        val = symbolOffset >> 16;
                        break;
                }

                // Next, determine how to write this to memory.
                if (reference.Type == SymbolReferenceType.Jump)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    Machine.Memory[(int)referenceOffset >> 2] = Machine.Memory[(int)referenceOffset >> 2] | (ushort)val;
                }
            }
        }

        void WriteSection(Section s, uint address)
        {
            // Make sure the buffer is a multiple of 4 bytes.
            byte[] buffer = new byte[((s.Stream.Length + 3) / 4) * 4];

            // Read the section into the buffer.
            s.Stream.Position = 0;
            s.Stream.Read(buffer, 0, (int)s.Stream.Length);

            // Copy the section to memory.
            // TODO: Implement some sort of raw memory copy that takes into account memory mapping. This will do for now.
            for (int i = 0; i < buffer.Length / 4; i++)
            {
                Machine.Memory[(int)(address >> 2) + i] = BitConverter.ToUInt32(buffer, i * 4);
            }
        }
    }
}
