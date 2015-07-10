namespace MCSong.Gui
{
    partial class BackupManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupManager));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.liBackups = new System.Windows.Forms.ListBox();
            this.liMaps = new System.Windows.Forms.ListBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.txtTime);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.btnCreate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.liBackups);
            this.panel1.Controls.Add(this.liMaps);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 179);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(131, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Backups:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Levels:";
            // 
            // liBackups
            // 
            this.liBackups.FormattingEnabled = true;
            this.liBackups.Location = new System.Drawing.Point(122, 29);
            this.liBackups.Name = "liBackups";
            this.liBackups.ScrollAlwaysVisible = true;
            this.liBackups.Size = new System.Drawing.Size(113, 147);
            this.liBackups.TabIndex = 11;
            this.liBackups.SelectedIndexChanged += new System.EventHandler(this.liBackups_SelectedIndexChanged);
            // 
            // liMaps
            // 
            this.liMaps.FormattingEnabled = true;
            this.liMaps.Location = new System.Drawing.Point(3, 29);
            this.liMaps.Name = "liMaps";
            this.liMaps.ScrollAlwaysVisible = true;
            this.liMaps.Size = new System.Drawing.Size(113, 147);
            this.liMaps.TabIndex = 10;
            this.liMaps.SelectedIndexChanged += new System.EventHandler(this.liMaps_SelectedIndexChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(250, 29);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(103, 23);
            this.btnCreate.TabIndex = 14;
            this.btnCreate.Text = "Create Backup";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(250, 58);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(103, 23);
            this.btnRestore.TabIndex = 15;
            this.btnRestore.Text = "Restore Backup";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(250, 87);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(103, 23);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete Backup";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(240, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(122, 10);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            // 
            // txtTime
            // 
            this.txtTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtTime.Location = new System.Drawing.Point(250, 143);
            this.txtTime.Name = "txtTime";
            this.txtTime.ReadOnly = true;
            this.txtTime.Size = new System.Drawing.Size(103, 20);
            this.txtTime.TabIndex = 18;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(247, 127);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(73, 13);
            this.lblTime.TabIndex = 19;
            this.lblTime.Text = "Backup Time:";
            // 
            // BackupManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 179);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(382, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(382, 217);
            this.Name = "BackupManager";
            this.Text = "Backup Manager";
            this.Load += new System.EventHandler(this.BackupManager_Load);
            this.Resize += new System.EventHandler(this.BackupManager_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox liBackups;
        private System.Windows.Forms.ListBox liMaps;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnCreate;
    }
}