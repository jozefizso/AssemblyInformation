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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lblIsDebugCompilation = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtReferences = new System.Windows.Forms.RichTextBox();
            this.lblReferences = new System.Windows.Forms.Label();
            this.txtDebugCompilation = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIsDebugCompilation
            // 
            this.lblIsDebugCompilation.AutoSize = true;
            this.lblIsDebugCompilation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblIsDebugCompilation.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblIsDebugCompilation.Location = new System.Drawing.Point(12, 40);
            this.lblIsDebugCompilation.Name = "lblIsDebugCompilation";
            this.lblIsDebugCompilation.Size = new System.Drawing.Size(76, 13);
            this.lblIsDebugCompilation.TabIndex = 0;
            this.lblIsDebugCompilation.Text = "Compilation:";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblFullName.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblFullName.Location = new System.Drawing.Point(12, 65);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(67, 13);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "Full Name:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCopy);
            this.panel1.Controls.Add(this.txtReferences);
            this.panel1.Controls.Add(this.lblReferences);
            this.panel1.Controls.Add(this.txtDebugCompilation);
            this.panel1.Controls.Add(this.txtFullName);
            this.panel1.Controls.Add(this.lblIsDebugCompilation);
            this.panel1.Controls.Add(this.lblFullName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 257);
            this.panel1.TabIndex = 2;
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(438, 33);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(99, 23);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.Text = "Copy to clipboard";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtReferences
            // 
            this.txtReferences.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReferences.Location = new System.Drawing.Point(94, 133);
            this.txtReferences.Name = "txtReferences";
            this.txtReferences.ReadOnly = true;
            this.txtReferences.Size = new System.Drawing.Size(443, 86);
            this.txtReferences.TabIndex = 5;
            this.txtReferences.Text = "";
            // 
            // lblReferences
            // 
            this.lblReferences.AutoSize = true;
            this.lblReferences.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.lblReferences.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lblReferences.Location = new System.Drawing.Point(12, 133);
            this.lblReferences.Name = "lblReferences";
            this.lblReferences.Size = new System.Drawing.Size(76, 13);
            this.lblReferences.TabIndex = 4;
            this.lblReferences.Text = "References:";
            // 
            // txtDebugCompilation
            // 
            this.txtDebugCompilation.ForeColor = System.Drawing.SystemColors.Info;
            this.txtDebugCompilation.Location = new System.Drawing.Point(94, 33);
            this.txtDebugCompilation.Name = "txtDebugCompilation";
            this.txtDebugCompilation.ReadOnly = true;
            this.txtDebugCompilation.Size = new System.Drawing.Size(59, 20);
            this.txtDebugCompilation.TabIndex = 3;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(94, 63);
            this.txtFullName.Multiline = true;
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.ReadOnly = true;
            this.txtFullName.Size = new System.Drawing.Size(443, 50);
            this.txtFullName.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 235);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(549, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(549, 24);
            this.menuStrip1.TabIndex = 4;
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
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 257);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(557, 281);
            this.MinimumSize = new System.Drawing.Size(557, 281);
            this.Name = "FormMain";
            this.Text = "Assembly Information";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIsDebugCompilation;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtDebugCompilation;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblReferences;
        private System.Windows.Forms.RichTextBox txtReferences;
        private System.Windows.Forms.Button btnCopy;
    }
}