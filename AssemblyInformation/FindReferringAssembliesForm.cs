using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace AssemblyInformation
{
    public partial class FindReferringAssembliesForm : Form
    {
        private bool cancel;

        public FindReferringAssembliesForm()
        {
            this.InitializeComponent();
            messageLabel.Text = "";
        }

        public Assembly TestAssembly { get; set; }

        public string DirectoryPath { get; set; }

        public bool Recursive { get; set; }

        public IEnumerable<string> ReferringAssemblies { get; set; }

        private void FindReferringAssembliesForm_Load(object sender, EventArgs e)
        {
            (new Thread(this.FindThread)).Start();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.cancel = true;
            cancelButton.Enabled = false;
        }

        private void FindThread()
        {
            DependencyWalker dw = null;
            try
            {
                dw = new DependencyWalker();
                dw.ReferringAssemblyStatusChanged += this.UpdateStatus;
                this.ReferringAssemblies = dw.FindReferringAssemblies(this.TestAssembly, this.DirectoryPath, this.Recursive);
                this.UpdateStatus(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = "", Progress = -3 });
            }
            catch (Exception)
            {
                this.UpdateStatus(this, new ReferringAssemblyStatusChangeEventArgs { StatusText = Resource.FailedToListBinaries, Progress = -2 });
            }
            finally
            {
                if (null != dw)
                {
                    dw.ReferringAssemblyStatusChanged -= this.UpdateStatus;
                }
            }
        }

        private void UpdateStatus(object sender, ReferringAssemblyStatusChangeEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<ReferringAssemblyStatusChangeEventArgs>(this.UpdateStatus), sender, e);
                return;
            }

            messageLabel.Text = e.StatusText;

            if (e.Progress >= 0)
            {
                progressBar1.Value = e.Progress;
            }

            if (e.Progress == -1)
            {
            }
            else if (e.Progress == -2)
            {
                this.DialogResult = DialogResult.Cancel;
                MessageBox.Show(Resource.AppName, e.StatusText, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            else if (e.Progress == -3)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            e.Cancel = this.cancel;
        }
    }
}
