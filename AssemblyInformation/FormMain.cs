using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace AssemblyInformation
{
    public partial class FormMain : Form
    {
        private const string Loading = "Loading";

        private readonly Assembly _mAssembly;

        static readonly Dictionary<string, Form> AssemblyFormMap = new Dictionary<string, Form>();

        private readonly AssemblyInformationLoader assemblyInformation;

	    private List<Binary> recursiveDependencies;

	    private List<Binary> directDependencies;

        public FormMain(Assembly assemb)
        {
            InitializeComponent();
            _mAssembly = assemb;
            assemblyInformation = new AssemblyInformationLoader(assemb);
            referringAssemblyFolderTextBox.Text = Path.GetDirectoryName(_mAssembly.Location);
            AssemblyFormMap[assemb.FullName] = this;
            FormClosing += FormMainFormClosing;
        }

        private void FormMainLoad(object sender, EventArgs e) 
        {
            string debuggableFlagsToolTipText;

            assemblyKindTextBox.Text = assemblyInformation.AssemblyKind;
            targetProcessorTextBox.Text = assemblyInformation.TargetProcessor;

            if (assemblyInformation.DebuggingFlags != null) 
            {
                debuggableFlagsToolTipText = string.Format(@"Debugging Flags: {0}", assemblyInformation.DebuggingFlags);
            }
            else 
            {
                debuggableFlagsToolTipText = @"Debugging Flags: NONE";
            }
            DebuggableFlagsToolTip.Tag = debuggableFlagsToolTipText;

            // Display values
            if (assemblyInformation.JitTrackingEnabled) 
            {
                txtTrackingEnabled.Text = "Debug";
                txtTrackingEnabled.BackColor = Color.Red;
            }
            else 
            {
                txtTrackingEnabled.Text = "Release";
                txtTrackingEnabled.BackColor = Color.Green;
            }

            if (assemblyInformation.JitOptimized) 
            {
                txtOptimized.Text = "Optimized";
                txtOptimized.BackColor = Color.Green;
            }
            else 
            {
                txtOptimized.Text = "Not Optimized";
                txtOptimized.BackColor = Color.Red;
            }

            if (assemblyInformation.IgnoreSymbolStoreSequencePoints) 
            {
                txtSequencing.Text = "MSIL Sequencing";
                txtSequencing.BackColor = Color.Green;
            }
            else 
            {
                txtSequencing.Text = "PDB Sequencing";
                txtSequencing.BackColor = assemblyInformation.JitTrackingEnabled ? Color.Red : Color.Orange;
            }

            if (assemblyInformation.EditAndContinueEnabled) 
            {
                txtEditAndContinue.Text = "Edit and Continue Enabled";
                txtEditAndContinue.BackColor = Color.Red;
            }
            else 
            {
                txtEditAndContinue.Text = "Edit and Continue Disabled";
                txtEditAndContinue.BackColor = Color.Green;
            }

		    frameWorkVersion.Text = assemblyInformation.FrameWorkVersion;
            txtFullName.Text = assemblyInformation.AssemblyFullName;

            DependencyWalker dependencyWalker = new DependencyWalker();
            List<string> errors;
            directDependencies = dependencyWalker.FindDependencies(_mAssembly, false, out errors).ToList();

            FillAssemblyReferences(directDependencies);
        }

        void FormMainFormClosing(object sender, FormClosingEventArgs e)
        {
            AssemblyFormMap.Remove(_mAssembly.FullName);
        }

		private void FillAssemblyReferences(IEnumerable<Binary> dependencies, TreeNode treeNode = null)
        {
			foreach (var binary in dependencies)
            {
                if(hideGACAssembliesToolStripMenuItem.Checked && binary.IsSystemBinary) continue;
			    string assemblyName = showAssemblyFullNameToolStripMenuItem.Checked
			                              ? binary.FullName
			                              : binary.DisplayName;
                TreeNode node = new TreeNode(assemblyName);
			    node.Tag = binary;

                if (treeNode == null)
                {
                    dependencyTreeView.Nodes.Add(node);
                }
                else
                {
                    if (!FindNodeParent(treeNode, assemblyName))
                    {
                        treeNode.Nodes.Add(node);
                    }
                    else
                    {
                        Trace.WriteLine(String.Format("{0} is already a parent of {1}", assemblyName, treeNode.Name));
                    }
                }
                if (AssemblyInformationLoader.SystemAssemblies.Where(p => assemblyName.StartsWith(p)).Count() == 0)
                {
                    node.Nodes.Add(new TreeNode(Loading));
                }
            }
        }

        private void AboutToolStripMenuItem1Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void LblCompilationMouseEnter(object sender, EventArgs e)
        {
            DebuggableFlagsToolTip.Active = true;  // workaround for MS bug
            DebuggableFlagsToolTip.Show((string)DebuggableFlagsToolTip.Tag, lblCompilation);
        }

        private void LblCompilationMouseLeave(object sender, EventArgs e)
        {
            DebuggableFlagsToolTip.Active = false;
        }

        static bool FindNodeParent(TreeNode node, string parentName)
        {
            if (null == node) return false;
            if (null == parentName) return false;

            while(node != null)
            {
                if (node.Text.Equals(parentName))
                    return true;
                node = node.Parent;
            }

            return false;
        }

        private void DependencyTreeViewMouseDoubleClick(object sender, MouseEventArgs e)
        {
            var node = dependencyTreeView.SelectedNode;
            if (null != node)
            {
			    Binary binary = node.Tag as Binary;
			    if (null != binary)
			    {
                    LoadAssemblyInformationForAssembly(binary.FullName);
			    }
			}
		}

        private static string LoadAssemblyInformationForAssembly(string assemblyName) 
        {
                int retryCount = 0;
                while (retryCount < 2)
                {
                    retryCount++;
                    try
                    {
                        Assembly assembly = null;
                        if (!File.Exists(assemblyName))
                        {
                            assembly = Assembly.Load(assemblyName);
                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo(assemblyName);
                            assembly = Assembly.LoadFile(fileInfo.FullName);
                        }

                        if(null != assembly)
                        {
                            if (!AssemblyFormMap.ContainsKey(assembly.FullName))
                            {
                                var form = new FormMain(assembly);
                                AssemblyFormMap[assembly.FullName] = form;
                                form.Show();
                            }
                            else
                            {
                                Form form = AssemblyFormMap[assembly.FullName];
                                form.BringToFront();
                            }
                        }
                    }
                    catch (FileNotFoundException)
                    {
                        string[] parts = assemblyName.Split(',');
                        if (parts.Length > 0)
                        {
                            string name = parts[0].Trim() + ".dll";
                            assemblyName = name;
                        }
                    }
                    catch (Exception) { }
                }
            return assemblyName;
        }

        private void DependencyTreeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if(null != e && null != e.Node && e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == Loading)
            {
                e.Node.Nodes.Clear();
			    Binary binary = e.Node.Tag as Binary;
                if(binary == null) return;

                if (AssemblyInformationLoader.SystemAssemblies.Where(p => binary.FullName.StartsWith(p)).Count() > 0)
                {
                    e.Node.Nodes.Clear();
                    return;
                }

                DependencyWalker dependencyWalker = new DependencyWalker();
                List<string> errors;
                var referredAssemblies =
                    dependencyWalker.FindDependencies(new AssemblyName(binary.FullName), false,
                                                      out errors);

                FillAssemblyReferences(referredAssemblies, e.Node);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) 
        {
			if(tabControl1.SelectedIndex ==1 && referenceListListBox.Items.Count == 0)
			{
				FillRecursiveDependency();
			}
		}

	    private void FillRecursiveDependency()
	    {
            if (null == recursiveDependencies)
            {
                DependencyWalker dependencyWalker = new DependencyWalker();
                List<string> errors;
                System.Windows.Forms.Cursor existingCursor = Cursor;
                try
                {
                    this.Cursor = Cursors.WaitCursor;
                    recursiveDependencies = dependencyWalker.FindDependencies(_mAssembly, true, out errors).ToList();
                }
                finally
                {
                    this.Cursor = existingCursor;
                }

                referenceListListBox.Items.Clear();
                foreach (var dependency in recursiveDependencies)
                {
                    if (hideGACAssembliesToolStripMenuItem.Checked && dependency.IsSystemBinary)
                        continue;
                    referenceListListBox.Items.Add(showAssemblyFullNameToolStripMenuItem.Checked?
                        dependency.FullName:
                        dependency.DisplayName);
                }
				if(errors.Count >0)
				{
                    referenceListListBox.Items.Add("");
                    referenceListListBox.Items.Add("-----------------ERRORS--------------");
					foreach (string error in errors)
					{
                        referenceListListBox.Items.Add(error);
                    }
                }
            }
            else
            {
                referenceListListBox.Items.Clear();
                foreach (var dependency in recursiveDependencies) 
                {
                    if (hideGACAssembliesToolStripMenuItem.Checked && dependency.IsSystemBinary)
                        continue;
                    referenceListListBox.Items.Add(showAssemblyFullNameToolStripMenuItem.Checked ?
                        dependency.FullName :
                        dependency.DisplayName);
                }
            }
	    }

	    private void referringAssemblyFolderSearchButton_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrWhiteSpace(referringAssemblyFolderTextBox.Text) &&
                Directory.Exists(referringAssemblyFolderTextBox.Text))
			{
                FindReferringAssembliesForm frm = new FindReferringAssembliesForm();
                frm.DirectoryPath = referringAssemblyFolderTextBox.Text;
                frm.TestAssembly = _mAssembly;
                frm.Recursive = true;
                if (frm.ShowDialog() == DialogResult.OK) 
                {
                    var binaries = frm.ReferringAssemblies;
                    if (null == binaries) return;
                    referringAssembliesListtBox.Items.Clear();
                    foreach (var binary in binaries)
                    {
                        referringAssembliesListtBox.Items.Add(binary);
					}
				}
			}
		}

        private void referringAssemblyBrowseFolderButton_Click(object sender, EventArgs e) 
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            if (dlg.ShowDialog() == DialogResult.OK) 
            {
                referringAssemblyFolderTextBox.Text = dlg.SelectedPath;
            }
        }

        private void AssemblyListBoxMouseDoubleClick(object sender, MouseEventArgs e) 
        {
            ListBox listBox = sender as ListBox;
            if (null == listBox || null == listBox.SelectedItem) return;

            var selectedAssembly = listBox.SelectedItem.ToString();
            if (!selectedAssembly.Contains("ERROR")) 
            {
                LoadAssemblyInformationForAssembly(selectedAssembly);
            }
        }

        private void hideGACAssembliesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hideGACAssembliesToolStripMenuItem.Checked = !hideGACAssembliesToolStripMenuItem.Checked;
            dependencyTreeView.Nodes.Clear();
            FillAssemblyReferences(directDependencies);
            if(recursiveDependencies != null)
                FillRecursiveDependency();
        }

        private void showAssemblyFullNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showAssemblyFullNameToolStripMenuItem.Checked =
                !showAssemblyFullNameToolStripMenuItem.Checked;

            dependencyTreeView.Nodes.Clear();
            FillAssemblyReferences(directDependencies);
            if (recursiveDependencies != null)
                FillRecursiveDependency();
        }
    }

    class AssemblyNameComparer: IComparer<AssemblyName>
    {
        #region IComparer<AssemblyName> Members

        public int Compare(AssemblyName x, AssemblyName y)
        {
            return String.Compare(x.FullName, y.FullName, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
    
}
