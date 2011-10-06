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
        The argument should be the path of the assembly  to check";
        public static Assembly assemb;

        [STAThread]
        static void Main(string[] args)
        {
            Application.ThreadException += ApplicationThreadException;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainTypeResolve;
            if (args.Length == 1)
            {
                string filePath = args.GetValue(0).ToString();

                try
                {
                    string assemblyullName = Path.GetFullPath(filePath);
                    //required to change directory for loading referenced assemblies
                    Environment.CurrentDirectory = Path.GetDirectoryName(filePath);
                    assemb = Assembly.LoadFile(assemblyullName);

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new FormMain(assemb));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format(@"Failed to load DLL.  Error was ""{0}""", ex.Message), "Error", MessageBoxButtons.OK);
                }
            }
        }

        static Assembly CurrentDomainTypeResolve(object sender, ResolveEventArgs args)
        {
            if (null != args && !String.IsNullOrEmpty(args.Name))
            {
                string[] parts = args.Name.Split(',');
                if (parts.Length > 0)
                {
                    string name = parts[0] + ".dll";
                    if (File.Exists(name))
                    {
                        try
                        {
                            return Assembly.LoadFile((new FileInfo(name)).FullName);
                        }
                        catch (ArgumentException) { }
                        catch (IOException) { }
                        catch (BadImageFormatException) { }
                    }
                }
            }
            return null;
        }

        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show(string.Format(@"Failed to load DLL.  Error was ""{0}""", e.Exception.Message), "Error", MessageBoxButtons.OK);
            Application.Exit();
        }
    }
}
