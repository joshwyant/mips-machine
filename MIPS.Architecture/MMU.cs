using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class MMU
    {
        public List<Tuple<int, int, int>> Mappings { get; set; }
        public uint[] RawMemory { get; set; }

        /// <summary>
        /// Gets the word at the address index * 4.
        /// </summary>
        /// <param name="index">The nth 32-bit word in memory</param>
        public uint this[int index]
        {
            get
            {
                return RawMemory[Mappings.Where(m => m.Item1 <= index && m.Item2 >= index).Select(m => index - m.Item1 + m.Item3).Single()];
            }
            set
            {
                RawMemory[Mappings.Where(m => m.Item1 <= index && m.Item2 >= index).Select(m => index - m.Item1 + m.Item3).Single()] = value;
            }
        }

        public MMU(int memoryInMB)
        {
            RawMemory = new uint[memoryInMB << 18];
            Mappings = new List<Tuple<int, int, int>>();
        }

        public void Map(uint startVirtualAddress, uint lastVirtualAddress, uint startPhysicalAddress)
        {
            Mappings.Add(new Tuple<int,int,int>((int)(startVirtualAddress >> 2), (int)(lastVirtualAddress >> 2), (int)(startPhysicalAddress >> 2)));
        }
    }
}
