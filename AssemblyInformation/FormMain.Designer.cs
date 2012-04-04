namespace AssemblyInformation
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lblCompilation = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dependencyTreeView = new System.Windows.Forms.TreeView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.referenceListListBox = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.referringAssembliesListtBox = new System.Windows.Forms.ListBox();
            this.referringAssemblyBrowseFolderButton = new System.Windows.Forms.Button();
            this.referringAssemblyFolderSearchButton = new System.Windows.Forms.Button();
            this.referringAssemblyFolderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.targetProcessorTextBox = new System.Windows.Forms.TextBox();
            this.assemblyKindTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReferences = new System.Windows.Forms.Label();
            this.txtEditAndContinue = new System.Windows.Forms.TextBox();
            this.txtSequencing = new System.Windows.Forms.TextBox();
            this.txtOptimized = new System.Windows.Forms.TextBox();
            this.txtTrackingEnabled = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideGACAssembliesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAssemblyFullNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DebuggableFlagsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.frameWorkVersion = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCompilation
            // 
            this.lblCompilation.AutoSize = true;
            this.lblCompilation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblCompilation.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblCompilation.Location = new System.Drawing.Point(10, 37);
            this.lblCompilation.Name = "lblCompilation";
            this.lblCompilation.Size = new System.Drawing.Size(76, 13);
            this.lblCompilation.TabIndex = 0;
            this.lblCompilation.Text = "Compilation:";
            this.lblCompilation.MouseEnter += new System.EventHandler(this.LblCompilationMouseEnter);
            this.lblCompilation.MouseLeave += new System.EventHandler(this.LblCompilationMouseLeave);
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblFullName.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblFullName.Location = new System.Drawing.Point(10, 126);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(67, 13);
            this.lblFullName.TabIndex = 9;
            this.lblFullName.Text = "Full Name:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.frameWorkVersion);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.targetProcessorTextBox);
            this.panel1.Controls.Add(this.assemblyKindTextBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblReferences);
            this.panel1.Controls.Add(this.txtEditAndContinue);
            this.panel1.Controls.Add(this.txtSequencing);
            this.panel1.Controls.Add(this.txtOptimized);
            this.panel1.Controls.Add(this.txtTrackingEnabled);
            this.panel1.Controls.Add(this.txtFullName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblCompilation);
            this.panel1.Controls.Add(this.lblFullName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 408);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(131, 171);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(673, 237);
            this.tabControl1.TabIndex = 12;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dependencyTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(665, 211);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Direct References";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dependencyTreeView
            // 
            this.dependencyTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dependencyTreeView.Location = new System.Drawing.Point(-3, 0);
            this.dependencyTreeView.Name = "dependencyTreeView";
            this.dependencyTreeView.Size = new System.Drawing.Size(669, 215);
            this.dependencyTreeView.TabIndex = 11;
            this.dependencyTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DependencyTreeViewBeforeExpand);
            this.dependencyTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DependencyTreeViewMouseDoubleClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.referenceListListBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(665, 211);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "All Direct & Indirect References";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // referenceListListBox
            // 
            this.referenceListListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referenceListListBox.FormattingEnabled = true;
            this.referenceListListBox.Location = new System.Drawing.Point(0, 0);
            this.referenceListListBox.Name = "referenceListListBox";
            this.referenceListListBox.Size = new System.Drawing.Size(663, 212);
            this.referenceListListBox.TabIndex = 0;
            this.referenceListListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AssemblyListBoxMouseDoubleClick);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.referringAssembliesListtBox);
            this.tabPage3.Controls.Add(this.referringAssemblyBrowseFolderButton);
            this.tabPage3.Controls.Add(this.referringAssemblyFolderSearchButton);
            this.tabPage3.Controls.Add(this.referringAssemblyFolderTextBox);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(665, 211);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Referring Assemblies";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // referringAssembliesListtBox
            // 
            this.referringAssembliesListtBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referringAssembliesListtBox.FormattingEnabled = true;
            this.referringAssembliesListtBox.Location = new System.Drawing.Point(7, 47);
            this.referringAssembliesListtBox.Name = "referringAssembliesListtBox";
            this.referringAssembliesListtBox.Size = new System.Drawing.Size(656, 160);
            this.referringAssembliesListtBox.TabIndex = 13;
            this.referringAssembliesListtBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AssemblyListBoxMouseDoubleClick);
            // 
            // referringAssemblyBrowseFolderButton
            // 
            this.referringAssemblyBrowseFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.referringAssemblyBrowseFolderButton.Location = new System.Drawing.Point(571, 21);
            this.referringAssemblyBrowseFolderButton.Name = "referringAssemblyBrowseFolderButton";
            this.referringAssemblyBrowseFolderButton.Size = new System.Drawing.Size(25, 23);
            this.referringAssemblyBrowseFolderButton.TabIndex = 2;
            this.referringAssemblyBrowseFolderButton.Text = "...";
            this.referringAssemblyBrowseFolderButton.UseVisualStyleBackColor = true;
            this.referringAssemblyBrowseFolderButton.Click += new System.EventHandler(this.referringAssemblyBrowseFolderButton_Click);
            // 
            // referringAssemblyFolderSearchButton
            // 
            this.referringAssemblyFolderSearchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.referringAssemblyFolderSearchButton.Location = new System.Drawing.Point(602, 21);
            this.referringAssemblyFolderSearchButton.Name = "referringAssemblyFolderSearchButton";
            this.referringAssemblyFolderSearchButton.Size = new System.Drawing.Size(57, 23);
            this.referringAssemblyFolderSearchButton.TabIndex = 3;
            this.referringAssemblyFolderSearchButton.Text = "Find";
            this.referringAssemblyFolderSearchButton.UseVisualStyleBackColor = true;
            this.referringAssemblyFolderSearchButton.Click += new System.EventHandler(this.referringAssemblyFolderSearchButton_Click);
            // 
            // referringAssemblyFolderTextBox
            // 
            this.referringAssemblyFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.referringAssemblyFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.referringAssemblyFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.referringAssemblyFolderTextBox.Location = new System.Drawing.Point(7, 21);
            this.referringAssemblyFolderTextBox.Name = "referringAssemblyFolderTextBox";
            this.referringAssemblyFolderTextBox.Size = new System.Drawing.Size(558, 20);
            this.referringAssemblyFolderTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(313, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select the directory in which you want to find referring assemblies";
            // 
            // targetProcessorTextBox
            // 
            this.targetProcessorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetProcessorTextBox.Location = new System.Drawing.Point(132, 105);
            this.targetProcessorTextBox.Name = "targetProcessorTextBox";
            this.targetProcessorTextBox.ReadOnly = true;
            this.targetProcessorTextBox.Size = new System.Drawing.Size(673, 20);
            this.targetProcessorTextBox.TabIndex = 8;
            // 
            // assemblyKindTextBox
            // 
            this.assemblyKindTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.assemblyKindTextBox.Location = new System.Drawing.Point(131, 63);
            this.assemblyKindTextBox.Multiline = true;
            this.assemblyKindTextBox.Name = "assemblyKindTextBox";
            this.assemblyKindTextBox.ReadOnly = true;
            this.assemblyKindTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.assemblyKindTextBox.Size = new System.Drawing.Size(673, 38);
            this.assemblyKindTextBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Target Processor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Assembly Kind";
            // 
            // lblReferences
            // 
            this.lblReferences.AutoSize = true;
            this.lblReferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblReferences.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblReferences.Location = new System.Drawing.Point(10, 168);
            this.lblReferences.Name = "lblReferences";
            this.lblReferences.Size = new System.Drawing.Size(76, 13);
            this.lblReferences.TabIndex = 11;
            this.lblReferences.Text = "References:";
            // 
            // txtEditAndContinue
            // 
            this.txtEditAndContinue.ForeColor = System.Drawing.SystemColors.Info;
            this.txtEditAndContinue.Location = new System.Drawing.Point(349, 34);
            this.txtEditAndContinue.Name = "txtEditAndContinue";
            this.txtEditAndContinue.ReadOnly = true;
            this.txtEditAndContinue.Size = new System.Drawing.Size(136, 20);
            this.txtEditAndContinue.TabIndex = 4;
            this.txtEditAndContinue.Text = "Edit and Continue Disabled";
            // 
            // txtSequencing
            // 
            this.txtSequencing.ForeColor = System.Drawing.SystemColors.Info;
            this.txtSequencing.Location = new System.Drawing.Point(254, 34);
            this.txtSequencing.Name = "txtSequencing";
            this.txtSequencing.ReadOnly = true;
            this.txtSequencing.Size = new System.Drawing.Size(94, 20);
            this.txtSequencing.TabIndex = 3;
            this.txtSequencing.Text = "MSIL Sequencing";
            // 
            // txtOptimized
            // 
            this.txtOptimized.ForeColor = System.Drawing.SystemColors.Info;
            this.txtOptimized.Location = new System.Drawing.Point(179, 34);
            this.txtOptimized.Name = "txtOptimized";
            this.txtOptimized.ReadOnly = true;
            this.txtOptimized.Size = new System.Drawing.Size(74, 20);
            this.txtOptimized.TabIndex = 2;
            this.txtOptimized.Text = "Not Optimized";
            // 
            // txtTrackingEnabled
            // 
            this.txtTrackingEnabled.ForeColor = System.Drawing.SystemColors.Info;
            this.txtTrackingEnabled.Location = new System.Drawing.Point(131, 34);
            this.txtTrackingEnabled.Name = "txtTrackingEnabled";
            this.txtTrackingEnabled.ReadOnly = true;
            this.txtTrackingEnabled.Size = new System.Drawing.Size(47, 20);
            this.txtTrackingEnabled.TabIndex = 1;
            this.txtTrackingEnabled.Text = "Release";
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.Location = new System.Drawing.Point(131, 128);
            this.txtFullName.Multiline = true;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(673, 36);
            this.txtFullName.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.aboutToolStripMenuItem.Text = "Options";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideGACAssembliesToolStripMenuItem,
            this.showAssemblyFullNameToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // hideGACAssembliesToolStripMenuItem
            // 
            this.hideGACAssembliesToolStripMenuItem.Name = "hideGACAssembliesToolStripMenuItem";
            this.hideGACAssembliesToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.hideGACAssembliesToolStripMenuItem.Text = "Hide GAC Assemblies";
            this.hideGACAssembliesToolStripMenuItem.Click += new System.EventHandler(this.hideGACAssembliesToolStripMenuItem_Click);
            // 
            // showAssemblyFullNameToolStripMenuItem
            // 
            this.showAssemblyFullNameToolStripMenuItem.Checked = true;
            this.showAssemblyFullNameToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAssemblyFullNameToolStripMenuItem.Name = "showAssemblyFullNameToolStripMenuItem";
            this.showAssemblyFullNameToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.showAssemblyFullNameToolStripMenuItem.Text = "Show Assembly Full Name";
            this.showAssemblyFullNameToolStripMenuItem.Click += new System.EventHandler(this.showAssemblyFullNameToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(491, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = ".Net Framework";
            this.label4.MouseEnter += new System.EventHandler(this.LblCompilationMouseEnter);
            this.label4.MouseLeave += new System.EventHandler(this.LblCompilationMouseLeave);
            // 
            // frameWorkVersion
            // 
            this.frameWorkVersion.Location = new System.Drawing.Point(593, 34);
            this.frameWorkVersion.Name = "frameWorkVersion";
            this.frameWorkVersion.ReadOnly = true;
            this.frameWorkVersion.Size = new System.Drawing.Size(100, 20);
            this.frameWorkVersion.TabIndex = 13;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 408);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(154, 110);
            this.Name = "FormMain";
            this.Text = "Assembly Information";
            this.Load += new System.EventHandler(this.FormMainLoad);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCompilation;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtTrackingEnabled;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblReferences;
        private System.Windows.Forms.TextBox txtOptimized;
        private System.Windows.Forms.ToolTip DebuggableFlagsToolTip;
        private System.Windows.Forms.TextBox txtSequencing;
        private System.Windows.Forms.TextBox txtEditAndContinue;
        private System.Windows.Forms.TextBox targetProcessorTextBox;
        private System.Windows.Forms.TextBox assemblyKindTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView dependencyTreeView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button referringAssemblyFolderSearchButton;
        private System.Windows.Forms.TextBox referringAssemblyFolderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button referringAssemblyBrowseFolderButton;
        private System.Windows.Forms.ListBox referringAssembliesListtBox;
        private System.Windows.Forms.ListBox referenceListListBox;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideGACAssembliesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAssemblyFullNameToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox frameWorkVersion;
    }
}