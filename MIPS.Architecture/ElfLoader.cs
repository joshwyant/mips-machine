using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ELFSharp;

namespace MIPS.Architecture
{
    public static class ElfLoader
    {
        /// <summary>
        /// Load an ELF file and begin executing it.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="filename"></param>
        /// <param name="offset">The physical offset in memory to begin loading.</param>
        public unsafe static IELF LoadElf(this Machine m, string filename, uint offset)
        {
            // Load the ELF
            var elf = ELFReader.Load(filename) as ELF<uint>;

            m.Memory.IsBigEndian = (elf.Endianess == Endianess.BigEndian);

            // Set the entry point
            m.CPU.PC = (uint*)elf.EntryPoint;

            // Load all the program headers
            foreach (var header in elf.ProgramHeaders.Where(ph => ph.Type == ProgramHeaderType.Load))
            {
                // Map the memory
                m.Memory.Map(header.Address, header.Address + header.Size - 4, offset);
                offset += header.Size;

                // Get the binary data for the section
                var data = header.GetContents();

                // Copy the data
                for (int i = 0; i < data.Length; i += 4)
                {
                    // Get the word from the program header
                    var word = BitConverter.ToUInt32(data, i);
                    // Store it at the correct index in memory
                    m.Memory[((int)header.Address + i) >> 2] = word;
                }
            }

            return elf;
        }
    }
}
