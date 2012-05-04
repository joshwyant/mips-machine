using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MIPS.Simulator
{
    class ElfToolchain
    {
        public string Prefix { get; set; }

        public ElfToolchain(string path, string prefix)
        {
            Environment.SetEnvironmentVariable("PATH", string.Format("{0};{1}", path, Environment.GetEnvironmentVariable("PATH")), EnvironmentVariableTarget.Process);
            Prefix = prefix;
        }

        public void ExecuteTool(string name, string parameters)
        {
            var psi = new ProcessStartInfo(Prefix + "-" + name, parameters);

            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
            psi.UseShellExecute = false;

            var p = Process.Start(psi);

            //Console.Write(p.StandardOutput.ReadToEnd());
            var error = p.StandardError.ReadToEnd();
            Console.Write(error);
        }
    }
}
