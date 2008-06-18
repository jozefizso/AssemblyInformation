using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace Att.AssemblyInformation
{
    class Program
    {
        private const string UsageString = @"Checks to see if an assembly is a debug build.
        The arguement should be the path of the assembly  to check";
        public static Assembly assemb;

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string filePath = args.GetValue(0).ToString();

                try
                {
                    assemb = Assembly.LoadFile(Path.GetFullPath(filePath));

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(assemb));
                }
                catch
                {
                }
            }
        }
    }
}
