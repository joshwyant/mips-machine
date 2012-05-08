using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MIPS.Architecture
{
    public class Debugger
    {
        public Machine Machine;
        public ELFSharp.ELF<uint> Elf;
        public Dictionary<string, uint> MethodSymbols = new Dictionary<string,uint>();

        public Debugger(Machine machine, ELFSharp.IELF elf)
        {
            Machine = machine;
            Elf = elf as ELFSharp.ELF<uint>;

            if (elf != null)
            {
                var symtab = elf.GetSection(".symtab") as ELFSharp.SymbolTable<uint>;

                MethodSymbols = symtab.Entries.Where(e => e.Type == ELFSharp.SymbolType.Function).ToDictionary(e => e.Name, e => e.Value);
            }
        }

        public unsafe string ExecutingFunction
        {
            get
            {
                var current = getSymbolAtAddress((uint)Machine.CPU.PC);
                var parent = getSymbolAtAddress(Machine.CPU.RF[(int)MIPS.Architecture.Register.ra]);

                if (string.IsNullOrEmpty(parent))
                    return current;

                return string.Format("{0} at {1}", current, parent);
            }
        }

        string getSymbolAtAddress(uint addr)
        {
            if (!MethodSymbols.Any(t => (long)t.Value <= addr))
                return null;

            return MethodSymbols.OrderBy(e => e.Value).Last(t => (long)t.Value <= addr).Key;
        }

        public void BreakAt(string symbolName)
        {
            Machine.CPU.BreakPoints.Add((IntPtr)MethodSymbols[symbolName]);
        }

        public void RemoveBreakPoint(string symbolName)
        {
            var ptr = (IntPtr)MethodSymbols[symbolName];

            if (Machine.CPU.BreakPoints.Contains(ptr))
                Machine.CPU.BreakPoints.RemoveAll(intptr => intptr == ptr);
        }
    }
}
