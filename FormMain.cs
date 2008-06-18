using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Att.AssemblyInformation
{
    public partial class FormMain : Form
    {
        public FormMain(Assembly assemb)
        {
            InitializeComponent();
            bool? isDebug;

            try
            {
                isDebug = IsAssemblyDebugBuild(assemb);
            }
            catch
            {
                isDebug = null;
            }

            if (isDebug != null)
            {
                txtDebugCompilation.Text = (isDebug == true) ? "Debug" : "Release";

                if (isDebug == true)
                {
                    txtDebugCompilation.BackColor = Color.Red;
                }
                else
                {
                    txtDebugCompilation.BackColor = Color.Green;
                }
            }
            else
            {
                txtDebugCompilation.Text = "Unknown";
            }

            txtFullName.Text = assemb.FullName;

            FillAssemblyReferences(assemb);
        }

        private void FillAssemblyReferences(Assembly assemb)
        {
            AssemblyName[] names = assemb.GetReferencedAssemblies();
            int count = 1;
            foreach (AssemblyName name in names)
            {
                StringBuilder buffer = new StringBuilder(16);
                foreach (byte b in name.GetPublicKeyToken())
                    buffer.AppendFormat("{0:X}", b);

                if (buffer.Length == 0)
                    buffer.Append("null");

                string fullName = name.Name + ", Version=" + name.Version + ", PublicKeyToken=" + buffer.ToString().ToLower();
                
                if (count == names.Length)
                    txtReferences.Text += fullName;
                else
                    txtReferences.Text += fullName + Environment.NewLine;

                count++;
            }
        }

        private static bool IsAssemblyDebugBuild(Assembly assemb)
        {
            foreach (object att in assemb.GetCustomAttributes(false))
            {
                if (att is System.Diagnostics.DebuggableAttribute)
                {
                    return ((System.Diagnostics.DebuggableAttribute)att).IsJITTrackingEnabled;
                }
            }

            return false;
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtFullName.Text);
            
        }
    }
}
