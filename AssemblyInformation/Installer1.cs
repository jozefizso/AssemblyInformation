using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace Att.AssemblyInformation
{
    //[RunInstaller(true)]
    public partial class Installer1 : Installer
    {
        public Installer1()
        {
            InitializeComponent();
        }

        public override void Install(IDictionary stateSaver)
        {
            /*
            try
            {
                string fullPath = Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
                //if(Environment.Is)

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            */
            
            //try
            //{
            //    string fullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //    Registry.ClassesRoot.CreateSubKey(@"dllfile\shell\AssemblyInformation").SetValue("", "Assembly Information", RegistryValueKind.Unknown);
            //    Registry.ClassesRoot.CreateSubKey(@"dllfile\shell\AssemblyInformation\command").SetValue("", "\"" + fullPath + "\\Att.AssemblyInformation.exe\" \"%1\"", RegistryValueKind.Unknown);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            //try
            //{
            //    Registry.ClassesRoot.DeleteSubKey(@"dllfile\shell\AssemblyInformation\command", false);
            //    Registry.ClassesRoot.DeleteSubKey(@"dllfile\shell\AssemblyInformation", false);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            base.Uninstall(savedState);
        }

    }
}
