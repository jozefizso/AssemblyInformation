using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Att.AssemblyInformation
{
    public partial class FormMain : Form
    {
        private const string Loading = "Loading";

        private readonly Assembly _mAssembly;

        static readonly Dictionary<string, Form> AssemblyFormMap = new Dictionary<string, Form>();

        static readonly Dictionary<PortableExecutableKinds, string> PortableExecutableKindsNames = new Dictionary<PortableExecutableKinds, string>();
        static readonly Dictionary<ImageFileMachine, string> ImageFileMachineNames = new Dictionary<ImageFileMachine, string>();
        static FormMain()
        {
            PortableExecutableKindsNames[PortableExecutableKinds.ILOnly] = "Contains only Microsoft intermediate language (MSIL), and is therefore neutral with respect to 32-bit or 64-bit platforms.";
            PortableExecutableKindsNames[PortableExecutableKinds.NotAPortableExecutableImage] = "Not in portable executable (PE) file format.";
            PortableExecutableKindsNames[PortableExecutableKinds.PE32Plus] = "Requires a 64-bit platform.";
            PortableExecutableKindsNames[PortableExecutableKinds.Required32Bit] = "Can be run on a 32-bit platform, or in the 32-bit Windows on Windows (WOW) environment on a 64-bit platform.";
            PortableExecutableKindsNames[PortableExecutableKinds.Unmanaged32Bit] = "Contains pure unmanaged code.";

            ImageFileMachineNames[ImageFileMachine.I386] = "Targets a 32-bit Intel processor.";
            ImageFileMachineNames[ImageFileMachine.IA64] = "Targets a 64-bit Intel processor.";
            ImageFileMachineNames[ImageFileMachine.AMD64] = "Targets a 64-bit AMD processor.";
        }

        public FormMain(Assembly assemb)
        {
            InitializeComponent();
            _mAssembly = assemb;

            AssemblyFormMap[assemb.FullName] = this;
            FormClosing += FormMainFormClosing;
        }

        void FormMainFormClosing(object sender, FormClosingEventArgs e)
        {
            AssemblyFormMap.Remove(_mAssembly.FullName);
        }

        private void FillAssemblyReferences(Assembly assemb)
        {
            AssemblyName[] names = assemb.GetReferencedAssemblies();
            foreach (AssemblyName name in names.OrderBy(p => p.FullName))
            {
                TreeNode dummyNode = new TreeNode(Loading);
                TreeNode node = new TreeNode(name.ToString());
                node.Nodes.Add(dummyNode);
                dependencyTreeView.Nodes.Add(node);
            }
        }

        private DebuggableAttribute GetDebuggableAttribute(Assembly assembly)
        {
            return assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().FirstOrDefault();
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

        private void BtnCopyClick(object sender, EventArgs e)
        {
            Clipboard.SetText(txtFullName.Text);
        }

        private void FormMainLoad(object sender, EventArgs e)
        {
            bool isJitTrackingEnabled;
            bool isJitOptimized;
            bool isIgnoreSymbolStoreSequencePoints;
            bool isEditAndContinueEnabled;
            string debuggableFlagsToolTipText;

            // Determine debugging configuration
            DebuggableAttribute debugAttribute = GetDebuggableAttribute(_mAssembly);

            
            var modules = _mAssembly.GetModules(false);
            if(modules.Length >0)
            {
                PortableExecutableKinds portableExecutableKinds;
                ImageFileMachine imageFileMachine;
                modules[0].GetPEKind(out portableExecutableKinds, out imageFileMachine);

                foreach (PortableExecutableKinds kind in Enum.GetValues(typeof(PortableExecutableKinds)))
                {
                    if((portableExecutableKinds & kind) == kind && kind != PortableExecutableKinds.NotAPortableExecutableImage)
                    {
                        if(!String.IsNullOrEmpty(assemblyKindTextBox.Text))
                        {
                            assemblyKindTextBox.Text += Environment.NewLine;
                        }
                        assemblyKindTextBox.Text += "- " + PortableExecutableKindsNames[kind];
                    }
                }
                //assemblyKindTextBox.Text = PortableExecutableKindsNames[portableExecutableKinds];
                targetProcessorTextBox.Text = ImageFileMachineNames[imageFileMachine];
            }
            

            if (debugAttribute != null)
            {
                isJitTrackingEnabled = debugAttribute.IsJITTrackingEnabled;
                isJitOptimized = !debugAttribute.IsJITOptimizerDisabled;
                isIgnoreSymbolStoreSequencePoints = (debugAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints) != DebuggableAttribute.DebuggingModes.None;
                isEditAndContinueEnabled = (debugAttribute.DebuggingFlags & DebuggableAttribute.DebuggingModes.EnableEditAndContinue) != DebuggableAttribute.DebuggingModes.None;

                debuggableFlagsToolTipText = string.Format(@"Debugging Flags: {0}", debugAttribute.DebuggingFlags);
            }
            else  // No DebuggableAttribute means IsJITTrackingEnabled=false, IsJITOptimizerDisabled=false, IgnoreSymbolStoreSequencePoints=false, EnableEditAndContinue=false
            {
                isJitTrackingEnabled = false;
                isJitOptimized = true;
                isIgnoreSymbolStoreSequencePoints = false;
                isEditAndContinueEnabled = false;

                debuggableFlagsToolTipText = @"Debugging Flags: NONE";
            }

            // Display values
            if (isJitTrackingEnabled)
            {
                txtTrackingEnabled.Text = "Debug";
                txtTrackingEnabled.BackColor = Color.Red;
            }
            else
            {
                txtTrackingEnabled.Text = "Release";
                txtTrackingEnabled.BackColor = Color.Green;
            }

            if (isJitOptimized)
            {
                txtOptimized.Text = "Optimized";
                txtOptimized.BackColor = Color.Green;
            }
            else
            {
                txtOptimized.Text = "Not Optimized";
                txtOptimized.BackColor = Color.Red;
            }

            if (isIgnoreSymbolStoreSequencePoints)
            {
                txtSequencing.Text = "MSIL Sequencing";
                txtSequencing.BackColor = Color.Green;
            }
            else
            {
                txtSequencing.Text = "PDB Sequencing";
                txtSequencing.BackColor = isJitTrackingEnabled ? Color.Red : Color.Orange;
            }

            if (isEditAndContinueEnabled)
            {
                txtEditAndContinue.Text = "Edit and Continue Enabled";
                txtEditAndContinue.BackColor = Color.Red;
            }
            else
            {
                txtEditAndContinue.Text = "Edit and Continue Disabled";
                txtEditAndContinue.BackColor = Color.Green;
            }

            txtFullName.Text = _mAssembly.FullName;

            FillAssemblyReferences(_mAssembly);

            DebuggableFlagsToolTip.Tag = debuggableFlagsToolTipText;
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
                string assemblyName = e.Node.Text;
                //object[] data = new object[3] {e.Node, assemblyName, null};
                //EventHandler handler = LoadAssemblyInfo;
                //handler.BeginInvoke(data, EventArgs.Empty, LoadedCallback, data);
                int retryCount = 0;
                bool loaded = false;
                while (retryCount < 2)
                {
                    retryCount++;
                    try
                    {
                        Assembly assembly = null;
                        if(!File.Exists(assemblyName))
                        {
                            assembly = Assembly.Load(assemblyName);
                        }
                        else
                        {
                            FileInfo fileInfo = new FileInfo(assemblyName);
                            assembly = Assembly.LoadFile(fileInfo.FullName);
                        }
                        var dependencies = assembly.GetReferencedAssemblies();
                        e.Node.Nodes.Clear();

                        foreach (var name in dependencies.OrderBy(p => p.FullName))
                        {
                            TreeNode dummyNode = new TreeNode(Loading);
                            TreeNode node = new TreeNode(name.ToString());
                            node.Nodes.Add(dummyNode);
                            e.Node.Nodes.Add(node);
                        }
                        loaded = true;
                    }
                    catch (FileNotFoundException)
                    {
                        string[] parts = assemblyName.Split(',');
                        if(parts.Length >0)
                        {
                            string name = parts[0].Trim() +".dll";
                            assemblyName = name;
                        }
                    }
                    catch (ArgumentException) { }
                    catch (IOException) { }
                    catch (BadImageFormatException) { }
                }
                if(!loaded)
                {
                    e.Node.Nodes[0].Text = "Failed to load dependencies.";
                }
            }
        }
    }
}
