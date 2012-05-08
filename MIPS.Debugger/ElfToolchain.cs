using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MIPS.Debugger
{
    class ElfToolchain
    {
        public class GCCCompileError : Exception
        {
            public GCCCompileError(string message) : base(message) { }
        }

        public string Prefix { get; set; }

        public ElfToolchain(string path, string prefix)
        {
            var dir = new DirectoryInfo(path);

            Environment.SetEnvironmentVariable("PATH", string.Format("{0};{1}", dir.FullName, Environment.GetEnvironmentVariable("PATH")), EnvironmentVariableTarget.Process);
            Prefix = prefix;
        }

        public void ExecuteTool(string name, string parameters)
        {
            var psi = new ProcessStartInfo(Prefix + "-" + name, parameters);

            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            var p = Process.Start(psi);

            //Console.Write(p.StandardOutput.ReadToEnd());
            var error = p.StandardError.ReadToEnd();
            if (!string.IsNullOrEmpty(error))
            {
                throw new GCCCompileError(error);
            }
        }
    }
}
