using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MIPS.Architecture
{
    public class Assembler
    {
        public List<Symbol> Symbols = new List<Symbol>();

        public List<SymbolReference> References = new List<SymbolReference>();

        public AssemblyContext CurrentContext { get; set; }

        public List<Section> Sections = new List<Section>();

        public List<string> Errors = new List<string>();

        Section currentSection;

        public Assembler()
        {
            Sections.AddRange(new[] {
                new Section(".data"),
                new Section(".text")
            });

            SetContext(".text");
        }

        public Symbol MarkLabel(string name)
        {
            return new Symbol { 
                Name = name, 
                Offset = (int)Sections.Where(s => s.Name == CurrentContext.Section).Single().Stream.Position, 
                Section = CurrentContext.Section 
            };
        }

        public void SetContext(string context)
        {
            currentSection = Sections.Where(s => s.Name == context).Single();

            CurrentContext = currentSection.Context;
        }

        public Symbol DefineExternal(string name)
        {
            return new Symbol
            {
                Name = name,
                IsExternal = true
            };
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
            var meaningful = line.Split(new[] {'#'}, 2)[0].Trim(); // Get rid of any comments and trim.

            // Do we have anything meaningful?
            if (string.IsNullOrEmpty(meaningful))
                return;

            if (meaningful.StartsWith("."))
            {
                // Process assembler directives
                throw new NotImplementedException();
            }
            else
            {
                // Process normal instructions - get the instruction and the arguments.
                var split = meaningful.Split(new[] { ' ' }, 2);
                var instruction = split[0].Trim();
                var arg = split.Length == 1 ? string.Empty : split[1].Trim();

                var args = arg.Split(',').Select(s => s.Trim());
            }
        }

        void Error(string error)
        {
            Errors.Add(string.Format("Error at line {0}: {1}", currentLine, error));
        }
    }
}
