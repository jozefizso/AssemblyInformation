namespace Att.AssemblyInformation
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
            this.dependencyTreeView = new System.Windows.Forms.TreeView();
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
            this.DebuggableFlagsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
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
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "Full Name:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dependencyTreeView);
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
            this.panel1.Controls.Add(this.lblCompilation);
            this.panel1.Controls.Add(this.lblFullName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(816, 408);
            this.panel1.TabIndex = 2;
            // 
            // dependencyTreeView
            // 
            this.dependencyTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dependencyTreeView.Location = new System.Drawing.Point(132, 168);
            this.dependencyTreeView.Name = "dependencyTreeView";
            this.dependencyTreeView.Size = new System.Drawing.Size(672, 237);
            this.dependencyTreeView.TabIndex = 11;
            this.dependencyTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.DependencyTreeViewBeforeExpand);
            this.dependencyTreeView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DependencyTreeViewMouseDoubleClick);
            // 
            // targetProcessorTextBox
            // 
            this.targetProcessorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetProcessorTextBox.Location = new System.Drawing.Point(132, 105);
            this.targetProcessorTextBox.Name = "targetProcessorTextBox";
            this.targetProcessorTextBox.ReadOnly = true;
            this.targetProcessorTextBox.Size = new System.Drawing.Size(673, 20);
            this.targetProcessorTextBox.TabIndex = 10;
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
            this.assemblyKindTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(10, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Target Processor";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 7;
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
            this.lblReferences.TabIndex = 4;
            this.lblReferences.Text = "References:";
            // 
            // txtEditAndContinue
            // 
            this.txtEditAndContinue.ForeColor = System.Drawing.SystemColors.Info;
            this.txtEditAndContinue.Location = new System.Drawing.Point(349, 34);
            this.txtEditAndContinue.Name = "txtEditAndContinue";
            this.txtEditAndContinue.ReadOnly = true;
            this.txtEditAndContinue.Size = new System.Drawing.Size(136, 20);
            this.txtEditAndContinue.TabIndex = 3;
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
            this.txtOptimized.TabIndex = 3;
            this.txtOptimized.Text = "Not Optimized";
            // 
            // txtTrackingEnabled
            // 
            this.txtTrackingEnabled.ForeColor = System.Drawing.SystemColors.Info;
            this.txtTrackingEnabled.Location = new System.Drawing.Point(131, 34);
            this.txtTrackingEnabled.Name = "txtTrackingEnabled";
            this.txtTrackingEnabled.ReadOnly = true;
            this.txtTrackingEnabled.Size = new System.Drawing.Size(47, 20);
            this.txtTrackingEnabled.TabIndex = 3;
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
            this.txtFullName.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(816, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "Options";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.AboutToolStripMenuItem1Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
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
    }
}