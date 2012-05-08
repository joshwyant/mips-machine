using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class MMU
    {
        private bool isBigEndian;

        public bool IsBigEndian
        {
            get { return isBigEndian; }
            set
            {
                isBigEndian = value;
                if (isBigEndian == BitConverter.IsLittleEndian)
                {
                    getShort = getShortByteSwapped;
                    getWord = getWordByteSwapped;
                }
                else
                {
                    getShort = getShortUnchanged;
                    getWord = getWordUnchanged;
                }
            }
        }

        public List<Tuple<uint, uint, uint>> Mappings;

        /// <summary>
        /// This is the physical memory, in little-endian.
        /// </summary>
        public unsafe uint* RawMemory;

        /// <summary>
        /// Gets the little-endian word at the virtual address index * 4.
        /// </summary>
        /// <param name="index">The nth 32-bit word in memory</param>
        public uint this[int index]
        {
            get
            {
                unsafe
                {
                    var i = (uint)index;
                    return RawMemory[Mappings.Where(m => m.Item1 <= i && m.Item2 >= i).Select(m => i - m.Item1 + m.Item3).Single()];
                }
            }
            set
            {
                unsafe
                {
                    var i = (uint)index;
                    RawMemory[Mappings.Where(m => m.Item1 <= i && m.Item2 >= i).Select(m => i - m.Item1 + m.Item3).Single()] = value;
                }
            }
        }

        public MMU(int memoryInMB)
        {
            unsafe
            {
                RawMemory = (uint*)System.Runtime.InteropServices.Marshal.AllocHGlobal(memoryInMB << 20);
            }
            Mappings = new List<Tuple<uint, uint, uint>>();
            IsBigEndian = false;
        }

        public void Map(uint startVirtualAddress, uint lastVirtualAddress, uint startPhysicalAddress)
        {
            Mappings.Add(new Tuple<uint,uint,uint>(startVirtualAddress >> 2, lastVirtualAddress >> 2, startPhysicalAddress >> 2));
        }

        #region Byte ordering and word fetching
        delegate ushort GetShortDelegate(ushort value);
        delegate uint GetWordDelegate(uint value);

        GetShortDelegate getShort;
        GetWordDelegate getWord;

        static ushort getShortByteSwapped(ushort value)
        {
            
            return (ushort)((value >> 8) | (value << 8));
        }

        static ushort getShortUnchanged(ushort value)
        {
            return value;
        }

        static uint getWordByteSwapped(uint value)
        {
            return (value >> 24) | ((value & 0x00FF0000) >> 8) | ((value & 0x0000FF00) << 8) | (value << 24);
        }

        static uint getWordUnchanged(uint value)
        {
            return value;
        }

        /// <summary>
        /// Gets a little endian word of a certain length at a virtual address
        /// </summary>
        uint getWordAtAddress(uint addr, int wordLength)
        {
            // Get the byte offset
            var offset = (int)(addr % 4);

            // Return word-aligned addresses faster
            if (offset == 0)
                return this[(int)(addr >> 2)];

            // Get the first part of the word
            var word = this[(int)(addr >> 2)] >> (offset * 8);

            // Do we have to fetch the next word?
            if ((offset + wordLength) > 4)
            {
                word |= this[(int)(addr >> 2) + 1] << ((4 - offset) * 8);
            }

            // Return our byte-aligned word.
            return word;
        }

        /// <summary>
        /// Sets a little endian word of a certain length at a virtual address
        /// </summary>
        void setWordAtAddress(uint addr, uint word, int wordLength)
        {
            // Get the byte offset, and the index
            var offset = (int)(addr % 4);
            var index = (int)(addr >> 2);

            // Set word-aligned addresses faster
            if (offset == 0 && wordLength == 4)
                this[index] = word;

            // The amount to shift each word.
            var shift = offset * 8;

            // Long word mask based on length of word
            var newWordMask = ~(~0UL << (wordLength * 8)); // 0xFF x (number of bytes in word)
            newWordMask <<= shift;

            // Mask for preserving surrounding bytes
            var preserveMask = ~newWordMask; // Opposite of new word mask

            // Create the new long
            var newLong = ((ulong)word << shift) & newWordMask;

            // Store the first word
            this[index] = (uint)((this[index] & preserveMask) | newLong);

            // Do we have to store a second word?
            if ((offset + wordLength) > 4)
            {
                newLong >>= 32;
                preserveMask >>= 32;
                newWordMask >>= 32;

                this[index + 1] = (uint)((this[index + 1] & preserveMask) | newLong);
            }
        }
        #endregion

        #region Get and Set methods
        public byte GetByte(uint addr)
        {
            return (byte)getWordAtAddress(addr, 1);
        }

        public uint GetWord(uint addr)
        {
            return getWord(getWordAtAddress(addr, 4));
        }

        public ushort GetShort(uint addr)
        {
            return getShort((ushort)getWordAtAddress(addr, 2));
        }

        public void SetByte(uint addr, byte value)
        {
            setWordAtAddress(addr, value, 1);
        }

        public void SetWord(uint addr, uint value)
        {
            setWordAtAddress(addr, getWord(value), 4);
        }

        public void SetShort(uint addr, ushort value)
        {
            setWordAtAddress(addr, getShort(value), 2);
        }
        #endregion
    }
}
