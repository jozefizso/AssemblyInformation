using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using Att.AssemblyInformation;

namespace AssemblyInformation
{
    public partial class FormMain : Form
    {
        private const string Loading = "Loading";

        private readonly Assembly _mAssembly;

        static readonly Dictionary<string, Form> AssemblyFormMap = new Dictionary<string, Form>();

        private readonly AssemblyInformationLoader assemblyInformation;

        public FormMain(Assembly assemb)
        {
            InitializeComponent();
            _mAssembly = assemb;
            assemblyInformation = new AssemblyInformationLoader(assemb);

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

            txtFullName.Text = assemblyInformation.AssemblyFullName;

            DependencyWalker dependencyWalker = new DependencyWalker();
            List<string> errors;
            List<string> dependencies = dependencyWalker.FindDependencies(_mAssembly, false, out errors);

            FillAssemblyReferences(dependencies);
            
        }

        void FormMainFormClosing(object sender, FormClosingEventArgs e)
        {
            AssemblyFormMap.Remove(_mAssembly.FullName);
        }

        private void FillAssemblyReferences(IEnumerable<string> dependencies, TreeNode treeNode = null)
        {
            foreach (var assemblyName in dependencies)
            {
                TreeNode node = new TreeNode(assemblyName);

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
                string assemblyName = node.Text;
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
            }
        }

        private void DependencyTreeViewBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if(null != e && null != e.Node && e.Node.Nodes.Count == 1 && e.Node.Nodes[0].Text == Loading)
            {
                e.Node.Nodes.Clear();
                string assemblyName = e.Node.Text;

                if (AssemblyInformationLoader.SystemAssemblies.Where(p => assemblyName.StartsWith(p)).Count() > 0)
                {
                    e.Node.Nodes.Clear();
                    return;
                }

                DependencyWalker dependencyWalker = new DependencyWalker();
                List<string> errors;
                List<string> referredAssemblies =
                    dependencyWalker.FindDependencies(new AssemblyName(assemblyName), false,
                                                      out errors);

                FillAssemblyReferences(referredAssemblies, e.Node);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) 
        {
            if(tabControl1.SelectedIndex ==1 && String.IsNullOrWhiteSpace(referenceListTextBox.Text))
            {
                DependencyWalker dependencyWalker = new DependencyWalker();
                List<string> errors;
                List<string> dependencies = dependencyWalker.FindDependencies(_mAssembly, true, out errors);
                StringBuilder stringBuilder = new StringBuilder();

                foreach (string dependency in dependencies)
                {
                    stringBuilder.Append(dependency);
                    stringBuilder.Append(Environment.NewLine);
                }
				if(errors.Count >0)
				{
					stringBuilder.Append("-----------------ERRORS--------------");
					foreach (string error in errors)
					{
						stringBuilder.Append(error);
						stringBuilder.Append(Environment.NewLine);
					}
				}
                referenceListTextBox.Text = stringBuilder.ToString();
            }
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
