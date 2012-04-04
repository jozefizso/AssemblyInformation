using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace AssemblyInformation 
{
    public partial class FindReferringAssembliesForm : Form 
    {
        public Assembly TestAssembly { get; set; }
        public string DirectoryPath { get; set; }
        public bool Recursive { get; set; }

        private bool cancel = false;
        public IEnumerable<string> ReferringAssemblies { get; set; }
        public FindReferringAssembliesForm() 
        {
            InitializeComponent();
            messageLabel.Text = "";
        }

        private void FindReferringAssembliesForm_Load(object sender, EventArgs e) 
        {
            (new Thread(FindThread)).Start();
        }

        private void cancelButton_Click(object sender, EventArgs e) 
        {
            cancel = true;
            cancelButton.Enabled = false;
        }

        void FindThread() 
        {
            DependencyWalker dw = null;
            try 
            {
                dw = new DependencyWalker();
                dw.ReferringAssemblyStatusChanged += UpdateStatus;
                ReferringAssemblies = dw.FindReferringAssemblies(TestAssembly, DirectoryPath, Recursive);
                UpdateStatus(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = "", Progress = -3 });
            }
            catch(Exception ex)
            {
                UpdateStatus(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = Resource.FailedToListBinaries, Progress = -2 });
            }
            finally
            {
                if(null != dw)
                    dw.ReferringAssemblyStatusChanged -= UpdateStatus;
            }
        }
        void UpdateStatus(object sender, ReferringAssemblyStatusChangeEventArgs e) 
        {
            if (InvokeRequired) 
            {
                Invoke(new EventHandler<ReferringAssemblyStatusChangeEventArgs>(UpdateStatus), sender, e);
                return;
            }
            messageLabel.Text = e.StatusText;
            
            if (e.Progress >= 0) progressBar1.Value = e.Progress;

            if (e.Progress == -1) 
            {
                
            }
            else if (e.Progress == -2) 
            {
                DialogResult = DialogResult.Cancel;
                MessageBox.Show(Resource.AppName, e.StatusText, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }
            else if (e.Progress == -3) 
            {
                DialogResult = DialogResult.OK;
                Close();
                return;
            }
            e.Cancel = cancel;
        }
    }
}
