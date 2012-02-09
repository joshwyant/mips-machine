using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class MMU
    {
        public List<Tuple<int, int, int>> Mappings;
        public unsafe uint* RawMemory;

        /// <summary>
        /// Gets the word at the address index * 4.
        /// </summary>
        /// <param name="index">The nth 32-bit word in memory</param>
        public uint this[int index]
        {
            get
            {
                unsafe
                {
                    return RawMemory[Mappings.Where(m => m.Item1 <= index && m.Item2 >= index).Select(m => index - m.Item1 + m.Item3).Single()];
                }
            }
            set
            {
                unsafe
                {
                    RawMemory[Mappings.Where(m => m.Item1 <= index && m.Item2 >= index).Select(m => index - m.Item1 + m.Item3).Single()] = value;
                }
            }
        }

        public MMU(int memoryInMB)
        {
            unsafe
            {
                RawMemory = (uint*)System.Runtime.InteropServices.Marshal.AllocHGlobal(memoryInMB << 20);
            }
            Mappings = new List<Tuple<int, int, int>>();
        }

        public void Map(uint startVirtualAddress, uint lastVirtualAddress, uint startPhysicalAddress)
        {
            Mappings.Add(new Tuple<int,int,int>((int)(startVirtualAddress >> 2), (int)(lastVirtualAddress >> 2), (int)(startPhysicalAddress >> 2)));
        }
    }
}
